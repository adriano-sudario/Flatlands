using Flatlands.Entities;
using Flatlands.Entities.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flatlands.Maps
{
    class GonzaloMap : Map
    {
        public GonzaloMap(List<Gunslinger> players) : base(players)
        {
            SetBlocks();
            SetGuns();
            SetBackgroundElements();
            SetFrontgroundElements();
        }

        private void SetGuns()
        {
            Guns = new List<Gun>()
            {
                new Gun(160, 365)
                {
                    Width = 16,
                    Height = 16
                },
                new Gun(610, 300)
                {
                    Width = 16,
                    Height = 16
                }
            };
        }

        private void SetBlocks()
        {
            Blocks = new List<Block>()
            {
                new Block()
                {
                    X = 0,
                    Y = 455,
                    Width = 800,
                    Height = 26
                },
                new Block()
                {
                    X = 0,
                    Y = 439,
                    Width = 186,
                    Height = 16
                },
                new Block()
                {
                    X = 0,
                    Y = 404,
                    Width = 170,
                    Height = 35
                },
                new Block()
                {
                    X = 0,
                    Y = 385,
                    Width = 176,
                    Height = 19
                },
                new Block()
                {
                    X = 0,
                    Y = 379,
                    Width = 54,
                    Height = 6
                },
                new Block()
                {
                    X = 0,
                    Y = 350,
                    Width = 54,
                    Height = 29
                },
                new Block()
                {
                    X = 0,
                    Y = 343,
                    Width = 64,
                    Height = 7
                },
                new Block()
                {
                    X = 0,
                    Y = 336,
                    Width = 68,
                    Height = 7
                },
                new Block()
                {
                    X = 0,
                    Y = 327,
                    Width = 118,
                    Height = 9
                },
                new Block()
                {
                    X = 0,
                    Y = 320,
                    Width = 129,
                    Height = 7
                },
                new Block()
                {
                    X = 163,
                    Y = 268,
                    Width = 66,
                    Height = 10
                },
                new Block()
                {
                    X = 266,
                    Y = 284,
                    Width = 66,
                    Height = 10
                },
                new Block()
                {
                    X = 385,
                    Y = 304,
                    Width = 66,
                    Height = 10
                },
                new Block()
                {
                    X = 503,
                    Y = 273,
                    Width = 66,
                    Height = 10
                },
                new Block()
                {
                    X = 672,
                    Y = 320,
                    Width = 128,
                    Height = 15
                },
                new Block()
                {
                    X = 698,
                    Y = 335,
                    Width = 102,
                    Height = 49
                },
                new Block()
                {
                    X = 624,
                    Y = 384,
                    Width = 176,
                    Height = 27
                },
                new Block()
                {
                    X = 620,
                    Y = 411,
                    Width = 180,
                    Height = 44
                },
                new Block(BlockType.Plank)
                {
                    X = 608,
                    Y = 320,
                    Width = 64,
                    Height = 10
                },
                new Block(BlockType.Plank)
                {
                    X = 480,
                    Y = 384,
                    Width = 144,
                    Height = 10
                },
                new Block(BlockType.Plank)
                {
                    X = 320,
                    Y = 384,
                    Width = 112,
                    Height = 9
                },
                new Block(BlockType.Plank)
                {
                    X = 176,
                    Y = 384,
                    Width = 80,
                    Height = 9
                }
            };
        }

        private void SetBackground()
        {

        }

        private void SetBackgroundElements()
        {
            BackgroundElements = new List<VisualEntity>();
        }

        private void SetFrontgroundElements()
        {
            FrontgroundElements = new List<VisualEntity>();
        }
    }
}
