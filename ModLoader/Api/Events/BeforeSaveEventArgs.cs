using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace spacechase0.MiniModLoader.Api.Events
{
    public class BeforeSaveEventArgs : EventArgs
    {
        /// <summary>
        /// The filename we are saving to.
        /// </summary>
        public string Filename { get; }

        internal BeforeSaveEventArgs( string filename )
        {
            Filename = filename;
        }
    }
}
