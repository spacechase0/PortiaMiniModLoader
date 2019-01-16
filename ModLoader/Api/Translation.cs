using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace spacechase0.MiniModLoader.Api
{
    /// <summary>
    /// Translation data.
    /// </summary>
    public class Translation
    {
        /// <summary>
        /// The translation ID.
        /// </summary>
        public int Id { get; internal set; }

        /// <summary>
        /// The voice ID.
        /// </summary>
        public int VoiceId { get; set; }

        /// <summary>
        /// The English version of this translation.
        /// </summary>
        public string English { get; set; }

        /// <summary>
        /// The Chinese version of this translation.
        /// </summary>
        public string Chinese { get; set; }

        /// <summary>
        /// The German version of this translation.
        /// </summary>
        public string German { get; set; }

        /// <summary>
        /// The French version of this translation.
        /// </summary>
        public string French { get; set; }

        /// <summary>
        /// The TChinese version of this translation. (Traditional?)
        /// (Not sure what that is, the field in the database is literally "T_Chinese")
        /// </summary>
        public string TChinese { get; set; }

        /// <summary>
        /// The Italian version of this translation.
        /// </summary>
        public string Italian { get; set; }

        /// <summary>
        /// The Spanish version of this translation.
        /// </summary>
        public string Spanish { get; set; }

        /// <summary>
        /// The Japanese version of this translation.
        /// </summary>
        public string Japanese { get; set; }

        /// <summary>
        /// The Russian version of this translation.
        /// </summary>
        public string Russian { get; set; }
    }
}
