using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace spacechase0.MiniModLoader.Api.Events
{
    public class AfterSaveEventArgs : EventArgs
    {
        /// <summary>
        /// The filename we saved to.
        /// </summary>
        public string Filename { get; }

        internal AfterSaveEventArgs( string filename )
        {
            Filename = filename;
        }
    }
}
