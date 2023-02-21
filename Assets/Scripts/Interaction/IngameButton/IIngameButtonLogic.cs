using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kekw.Interaction
{
    /// <summary>
    /// Ingame button target script will implement this interface.
    /// </summary>
    interface IIngameButtonLogic
    {
        /// <summary>
        /// Action to trigger when button fires.
        /// </summary>
        public void TriggerAction();
    }
}
