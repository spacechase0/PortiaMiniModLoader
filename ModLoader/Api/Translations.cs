using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace spacechase0.MiniModLoader.Api
{
    /// <summary>
    /// Manages injected translation data.
    /// </summary>
    public static class Translations
    {
        internal static List<Translation> translations = new List<Translation>();
        private static int translationCounter = 1000000000;

        /// <summary>
        /// Register a translation
        /// </summary>
        /// <param name="trans">The translation to register.</param>
        /// <returns>The translation's new ID.</returns>
        public static int Register( Translation trans )
        {
            trans.Id = translationCounter++;
            translations.Add(trans);
            return trans.Id;
        }
    }
}
