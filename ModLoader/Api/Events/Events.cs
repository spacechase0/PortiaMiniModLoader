using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace spacechase0.MiniModLoader.Api.Events
{
    public static class Events
    {
        /// <summary>
        /// Triggers every frame.
        /// Sender: none
        /// </summary>
        public static EventHandler<OnUpdateEventArgs> OnUpdate;

        /// <summary>
        /// Triggers when the player leaves their bed.
        /// Sender: none <todo>player</todo>
        /// </summary>
        public static EventHandler PlayerLeavesBed;

        /// <summary>
        /// Triggers before the game is saved.
        /// </summary>
        public static EventHandler<BeforeSaveEventArgs> BeforeSave;

        /// <summary>
        /// Triggers after the game is saved.
        /// </summary>
        public static EventHandler<AfterSaveEventArgs> AfterSave;

        /// <summary>
        /// Triggers after the game is loaded.
        /// </summary>
        public static EventHandler<AfterLoadEventArgs> AfterLoad;
    }
}
