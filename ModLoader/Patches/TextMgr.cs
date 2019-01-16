using Harmony;
using spacechase0.MiniModLoader.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace spacechase0.MiniModLoader.Patches
{
    /// <summary>
    /// Injects our translations into the dictionary.
    /// </summary>
    [HarmonyPatch(typeof(TextMgr))]
    [HarmonyPatch("Load")]
    public static class TextHook
    {
        public static void Postfix(string str, object ___texts)
        {
            Log.Debug("Putting in translation stuff for language " + str);
            var dict = ___texts as System.Collections.IDictionary;
            foreach ( var trans in Translations.translations )
            {
                string translated = trans.English;
                switch ( str )
                {
                    case "Chinese":
                        if (trans.Chinese != null)
                            translated = trans.Chinese;
                        break;
                    case "German":
                        if (trans.German != null)
                            translated = trans.German;
                        break;
                    case "French":
                        if (trans.French != null)
                            translated = trans.French;
                        break;
                    case "T_Chinese":
                        if (trans.TChinese != null)
                            translated = trans.TChinese;
                        break;
                    case "Italian":
                        if (trans.Italian != null)
                            translated = trans.Italian;
                        break;
                    case "Spanish":
                        if (trans.Spanish != null)
                            translated = trans.Spanish;
                        break;
                    case "Japanese":
                        if (trans.Japanese != null)
                            translated = trans.Japanese;
                        break;
                    case "Russian":
                        if (trans.Russian != null)
                            translated = trans.Russian;
                        break;
                }
                dict.Add(trans.Id, MakeItem(translated, trans.VoiceId));
            }
        }

        private static ConstructorInfo itemConstructor = null;
        private static FieldInfo itemTextField = null;
        private static FieldInfo itemVoiceIdField = null;
        private static object MakeItem(string text, int voiceId)
        {
            if (itemConstructor is null)
            {
                var type = AccessTools.Inner(typeof(TextMgr), "Item");
                itemConstructor = type.GetConstructor(new Type[] { });
                itemTextField = type.GetField("text");
                itemVoiceIdField = type.GetField("voiceTextId");
            }

            var item = itemConstructor.Invoke(new object[] { });
            itemTextField.SetValue(item, text);
            itemVoiceIdField.SetValue(item, voiceId);
            return item;
        }
    }
}
