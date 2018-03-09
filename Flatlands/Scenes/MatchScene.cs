using Flatlands.Drawings;
using Flatlands.Entities;
using Flatlands.Entities.Types;
using Flatlands.Maps;
using Flatlands.Mechanics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flatlands.Scenes
{
    public class MatchScene : Scene
    {
        public Map Map;

        private Texture2D playerCollisionDebug;
        private Texture2D blockCollisionDebug;
        private Color lightRed;
        private Color lightBlue;

        public MatchScene()
        {
            playerCollisionDebug = new Texture2D(FlatlandsGame.SuperGraphics, 1, 1);
            lightRed = Color.Red;
            lightRed.A = 125;
            playerCollisionDebug.SetData(new Color[] { lightRed });

            blockCollisionDebug = new Texture2D(FlatlandsGame.SuperGraphics, 1, 1);
            lightBlue = Color.Blue;
            lightBlue.A = 125;
            blockCollisionDebug.SetData(new Color[] { lightBlue });
        }

        public void Update(GameTime gameTime)
        {
            Map.Update(gameTime);

            foreach (Gunslinger gunslinger in Map.Players)
            {
                gunslinger.Update(gameTime);

                if (gunslinger.Ground == null)
                {
                    gunslinger.GravityForceApplied -= Map.Gravity;
                    if (gunslinger.GravityForceApplied < gunslinger.JumpForce * -1)
                        gunslinger.GravityForceApplied = gunslinger.JumpForce * -1;
                }

                gunslinger.X = MathHelper.Clamp(gunslinger.X, 0, 
                    FlatlandsGame.ScreenWidth - gunslinger.BoundingBox.Width);

                if (gunslinger.IsAiming)
                    gunslinger.Aim.Update(gameTime, Map.Blocks, Map.Players);

                if (gunslinger.HasJustShot)
                {
                    if (gunslinger.Aim.Target?.GetType().BaseType == typeof(Gunslinger))
                        (gunslinger.Aim.Target as Gunslinger).Die();
                    gunslinger.HasJustShot = false;
                }
            }

            for (int i = Map.Guns.Count - 1; i >= 0; i--)
            {
                Map.Guns[i].Update(gameTime);

                bool removeGun = false;

                foreach (Gunslinger gunslinger in Map.Players)
                {
                    if (gunslinger.HasGun)
                        continue;

                    if (gunslinger.BoundingBox.Intersects(Map.Guns[i].BoundingBox))
                    {
                        gunslinger.HasGun = true;
                        removeGun = true;
                        break;
                    }
                }

                if (removeGun)
                    Map.Guns.Remove(Map.Guns[i]);
            }

            foreach (Gunslinger gunslinger in Map.Players)
            {
                if (gunslinger.Ground != null && !gunslinger.BottomBounds.Intersects(gunslinger.Ground.BoundingBox))
                    CheckForStepBelow(gunslinger);

                if (gunslinger.GroundPassingThrough != null &&
                    !gunslinger.BoundingBox.Intersects(gunslinger.GroundPassingThrough.BoundingBox))
                    gunslinger.GroundPassingThrough = null;

                foreach (Block block in Map.Blocks)
                {
                    if (block != gunslinger.GroundPassingThrough)
                    {
                        if (gunslinger.TopBounds.Intersects(block.BoundingBox) &&
                            block.Type == BlockType.Plank && gunslinger.CurrentAction == Gunslinger.Action.Jumping)
                        {
                            gunslinger.GroundPassingThrough = block;
                            continue;
                        }

                        if (gunslinger.BottomBounds.Intersects(block.BoundingBox) && block != gunslinger.Ground)
                            gunslinger.FixBottomPosition(block);
                        else if (gunslinger.TopBounds.Intersects(block.BoundingBox))
                            gunslinger.FixTopPosition(block);

                        if (gunslinger.Ground == null && 
                            ((gunslinger.RightBounds.Intersects(block.BoundingBox) ||
                            gunslinger.LeftBounds.Intersects(block.BoundingBox)) &&
                            (block.Type == BlockType.Plank)))
                        {
                            gunslinger.GroundPassingThrough = block;
                            continue;
                        }

                        if (gunslinger.RightBounds.Intersects(block.BoundingBox))
                            gunslinger.FixRightPosition(block);
                        else if (gunslinger.LeftBounds.Intersects(block.BoundingBox))
                            gunslinger.FixLeftPosition(block);
                    }
                }
            }
        }

        private void CheckForStepBelow(Gunslinger gunslinger)
        {
            gunslinger.Ground = null;
            Movement.Move(gunslinger, VerticalDirection.Down, gunslinger.StepHeight);
            foreach (Block block in Map.Blocks)
            {
                if (gunslinger.BottomBounds.Intersects(block.BoundingBox) && block != gunslinger.Ground)
                {
                    gunslinger.FixBottomPosition(block);
                    return;
                }
            }
            gunslinger.Y = gunslinger.PreviousY;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Map.Draw(spriteBatch);

            if (System.Diagnostics.Debugger.IsAttached)
            {
                foreach (Block b in Map.Blocks)
                {
                    spriteBatch.Draw(blockCollisionDebug, b.BoundingBox, Color.White);
                }
                foreach (Gunslinger p in Map.Players)
                {
                    spriteBatch.Draw(playerCollisionDebug, p.TopBounds, Color.White);
                    spriteBatch.Draw(playerCollisionDebug, p.BottomBounds, Color.White);
                    spriteBatch.Draw(playerCollisionDebug, p.LeftBounds, Color.White);
                    spriteBatch.Draw(playerCollisionDebug, p.RightBounds, Color.White);
                }
            }
        }
    }
}
