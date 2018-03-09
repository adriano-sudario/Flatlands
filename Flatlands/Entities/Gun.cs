using Flatlands.Drawings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flatlands.Entities
{
    public class Gun : VisualEntity
    {
        private static AnimatedSprite GunSprite
        {
            get
            {
                Animation gunAnimation = Animation.GetFromAtlas("SpinningGun", 9, 12, 16, 16, 8, 100);
                return new AnimatedSprite(new List<Animation>() { gunAnimation }) { Name = "Gun" };
            }
        }

        public Gun(int x, int y) : base(GunSprite)
        {
            X = x;
            Y = y;
        }
    }
}
