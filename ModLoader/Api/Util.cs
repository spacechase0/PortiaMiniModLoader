using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace spacechase0.MiniModLoader.Api
{
    public static class Util
    {
        /// <summary>
        /// A shallow copy of an object (except simple arrays, which are cloned).
        /// </summary>
        /// <typeparam name="T">The type.</typeparam>
        /// <param name="oldItem">The object to clone.</param>
        /// <returns></returns>
        public static T Duplicate<T>(T oldItem) where T : new()
        {
            var newItem = new T();
            foreach (var field in typeof(T).GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
            {
                var obj = field.GetValue(oldItem);
                if (obj != null && obj.GetType().IsArray)
                {
                    obj = ((Array)obj).Clone();
                }
                field.SetValue(newItem, obj);
            }
            return newItem;
        }

        /// <summary>
        /// Invoke a parameterless event.
        /// </summary>
        /// <param name="name">The event name.</param>
        /// <param name="handlers">The event handlers.</param>
        /// <param name="sender">The event sender.</param>
        public static void InvokeEvent(string name, IEnumerable<Delegate> handlers, object sender)
        {
            if (handlers == null)
                return;

            var args = new EventArgs();
            foreach (EventHandler handler in handlers.Cast<EventHandler>())
            {
                try
                {
                    handler.Invoke(sender, args);
                }
                catch (Exception e)
                {
                    Log.Error($"Exception while handling event {name}:\n{e}");
                }
            }
        }

        /// <summary>
        /// Invoke an event.
        /// </summary>
        /// <typeparam name="T">The event argument type.</typeparam>
        /// <param name="name">The event name.</param>
        /// <param name="handlers">The event handlers.</param>
        /// <param name="sender">The event sender.</param>
        /// <param name="args">The event arguments.</param>
        public static void InvokeEvent<T>(string name, IEnumerable<Delegate> handlers, object sender, T args) where T : EventArgs
        {
            if (handlers == null)
                return;

            foreach (EventHandler<T> handler in handlers.Cast<EventHandler<T>>())
            {
                try
                {
                    handler.Invoke(sender, args);
                }
                catch (Exception e)
                {
                    Log.Error($"Exception while handling event {name}:\n{e}");
                }
            }
        }
    }
}
