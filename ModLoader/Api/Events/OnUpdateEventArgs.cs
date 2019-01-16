using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace spacechase0.MiniModLoader.Api.Events
{
    public class OnUpdateEventArgs : EventArgs
    {
        /// <summary>
        /// The delta time since last frame.
        /// </summary>
        public double DeltaTime { get; }

        internal OnUpdateEventArgs( double delta )
        {
            DeltaTime = delta;
        }
    }
}
