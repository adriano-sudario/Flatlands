using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Flatlands.Inputs.GameInput;

namespace Flatlands.Interfaces
{
    interface IControllable
    {
        AttachedTo InputAttached { get; set; }
        void AddCommands();
        void ResetCommands();
    }
}
