using Flatlands.Drawings;
using Flatlands.Entities;
using Flatlands.Entities.Types;
using Flatlands.Extensions;
using Flatlands.Inputs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flatlands.Maps
{
    public class Map
    {
        public List<Block> Blocks { get; set; }
        public List<VisualEntity> BackgroundElements { get; set; }
        public List<VisualEntity> FrontgroundElements { get; set; }
        public List<Gunslinger> Players { get; set; }
        public List<Gun> Guns { get; set; }

        public float Gravity { get; set; }

        public Map(List<Gunslinger> players)
        {
            Gravity = 0.3f;
            Players = players;
        }

        public void Update(GameTime gameTime)
        {
            foreach (VisualEntity bgElement in BackgroundElements)
                bgElement.Update(gameTime);

            foreach (VisualEntity fgElement in FrontgroundElements)
                fgElement.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Global.MapAtlas, new Rectangle(0, 0, 800, 480), Color.White);

            BackgroundElements.Draw(spriteBatch);
            FrontgroundElements.Draw(spriteBatch);
            Guns.Draw(spriteBatch);
            Players.Draw(spriteBatch);

            foreach (Gunslinger gunslinger in Players)
                if (gunslinger.IsAiming)
                    gunslinger.Aim.Draw(spriteBatch);
        }
    }
}
