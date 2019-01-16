using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace spacechase0.MiniModLoader.Api.Events
{
    public class AfterLoadEventArgs : EventArgs
    {
        /// <summary>
        /// The filename we loaded from.
        /// </summary>
        public string Filename { get; }

        internal AfterLoadEventArgs( string filename )
        {
            Filename = filename;
        }
    }
}
