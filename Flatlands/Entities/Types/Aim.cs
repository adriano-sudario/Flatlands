using Flatlands.Drawings;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flatlands.Entities.Types
{
    public class Aim
    {
        private readonly int trajectoryWidth = 800;

        private float angleRadian;
        private float angleDegrees;

        public float AngleRadian
        {
            get { return angleRadian; }
            set
            {
                angleRadian = value;
                angleDegrees = MathHelper.ToDegrees(value);
                UpdateTrajectory();
            }
        }

        public float AngleDegree
        {
            get { return angleDegrees; }
            set
            {
                angleDegrees = value;
                angleRadian = MathHelper.ToRadians(value);
                UpdateTrajectory();
            }
        }

        public Entity Target { get; private set; }

        private VisualEntity radius;
        private VisualEntity angleEntity;
        private VisualEntity trajectory;
        private VisualEntity target;

        private Vector2 startPoint;

        public Aim()
        {
            radius = new VisualEntity(new Sprite(color: Color.White)
            {
                 Source = new Rectangle(0, 192, 80, 80),
                 Origin = new Vector2(40, 40),
                 Width = 80,
                 Height = 80
            }, false)
            {
                Width = 80,
                Height = 80
            };

            angleEntity = new VisualEntity(
                new AnimatedSprite(
                    new List<Animation>() {
                        Animation.GetFromAtlas("BlinkingAngle", 0, 17, 16, 16, 2, 100)
                    }, color: Color.White)
                {
                    Name = "AngleEntity",
                    Origin = new Vector2(8, 8),
                    Width = 16,
                    Height = 16
                }, false)
            {
                Width = 16,
                Height = 16
            };

            trajectory = new VisualEntity(
                new AnimatedSprite(
                    new List<Animation>() {
                        new Animation()
                        {
                            Name = "TrajectoryAnimation",
                            Frames = new List<Animation.Frame>()
                            {
                                new Animation.Frame()
                                {
                                    Source = new Rectangle(64, 275, trajectoryWidth, 3),
                                    Duration = 100
                                },
                                new Animation.Frame()
                                {
                                    Source = new Rectangle(64, 282, trajectoryWidth, 3),
                                    Duration = 100
                                }
                            }
                        }
                    }, color: Color.White)
                {
                    Name = "Trajectory",
                    Origin = new Vector2(0, 1),
                    Width = trajectoryWidth,
                    Height = 3
                }, false)
            {
                Width = trajectoryWidth,
                Height = 3
            };

            target = new VisualEntity(
                new AnimatedSprite(
                    new List<Animation>() {
                        Animation.GetFromAtlas("BlinkingAngleEnd", 2, 17, 16, 16, 2, 100)
                    }, color: Color.White)
                {
                    Name = "AngleEndEntity",
                    Origin = new Vector2(8, 9),
                    Width = 16,
                    Height = 16
                }, false)
            {
                Width = 16,
                Height = 16
            };
        }

        public void Activate(Gunslinger gunslinger)
        {
            radius.X = gunslinger.X + (gunslinger.Width / 2);
            radius.Y = gunslinger.Y + (gunslinger.Height / 2);
            radius.IsVisible = true;
            radius.StartBlinking(maxAlpha: 0.5f);
            angleEntity.IsVisible = true;
            trajectory.IsVisible = true;
            startPoint = new Vector2(radius.X, radius.Y);
        }

        public void Deactivate()
        {
            radius.IsVisible = false;
            angleEntity.IsVisible = false;
            radius.StopBlinking();
        }

        private void UpdateTrajectory()
        {
            Vector2 position = new Vector2(
                startPoint.X + (float)((radius.OriginX - 3) * Math.Cos(AngleRadian * -1)),
                startPoint.Y + (float)((radius.OriginY - 3) * Math.Sin(AngleRadian * -1)));

            angleEntity.X = position.X;
            angleEntity.Y = position.Y;
            trajectory.X = position.X;
            trajectory.Y = position.Y;
            trajectory.Sprite.Rotation = AngleRadian * -1;
            target.IsVisible = false;
        }

        private void UpdateTrajectory(List<Block> blocks, List<Gunslinger> gunslingers)
        {
            Vector2 gunPoint = new Vector2(startPoint.X + (float)(16 * Math.Cos(AngleRadian * -1)),
                startPoint.Y + (float)(16 * Math.Sin(AngleRadian * -1)));

            Line trajectoryLine = new Line(gunPoint, 
                new Vector2(trajectory.X + (float)(trajectoryWidth * Math.Cos(AngleRadian * -1)),
                trajectory.Y + (float)(trajectoryWidth * Math.Sin(AngleRadian * -1))));

            Vector2? intersectionPoint = null;

            foreach (Block block in blocks)
                CheckForEntityIntersection(block, trajectoryLine, ref intersectionPoint);

            foreach (Gunslinger gunslinger in gunslingers.Where(g => !g.IsDead))
                CheckForEntityIntersection(gunslinger, trajectoryLine, ref intersectionPoint);

            if (intersectionPoint == null)
            {
                trajectory.Sprite.Width = trajectoryWidth;
                return;
            }

            int startToTrajectoryLength = (int)(trajectory.Position - trajectoryLine.Begin).Length();
            int startToIntersectionLength = (int)(intersectionPoint.Value - trajectoryLine.Begin).Length();
            int width = startToIntersectionLength - startToTrajectoryLength;
            target.IsVisible = true;
            target.X = gunPoint.X + (float)(startToIntersectionLength * Math.Cos(AngleRadian * -1));
            target.Y = gunPoint.Y + (float)(startToIntersectionLength * Math.Sin(AngleRadian * -1));
            trajectory.Sprite.Width = width < 0 ? 0 : width;
        }

        private void CheckForEntityIntersection(Entity entity, Line trajectoryLine, ref Vector2? intersectionPoint)
        {
            Vector2? intersectionFound = trajectoryLine.GetIntersectedPoint(entity, AngleDegree);

            if (intersectionFound != null)
            {
                if (intersectionPoint == null ||
                    (intersectionFound.Value - trajectoryLine.Begin).Length() <
                    (intersectionPoint.Value - trajectoryLine.Begin).Length())
                {
                    intersectionPoint = intersectionFound;
                    Target = entity;
                }
            }
        }

        public void Update(GameTime gameTime, List<Block> blocksOnEnvironment, 
            List<Gunslinger> gunslingersOnEnvironment)
        {
            radius.Update(gameTime);
            angleEntity.Update(gameTime);
            trajectory.Update(gameTime);
            target.Update(gameTime);
            UpdateTrajectory(blocksOnEnvironment, gunslingersOnEnvironment);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            radius.Draw(spriteBatch);
            trajectory.Draw(spriteBatch);
            angleEntity.Draw(spriteBatch);
            target.Draw(spriteBatch);
        }
    }
}
