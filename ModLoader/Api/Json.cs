using SimpleJSON;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace spacechase0.MiniModLoader.Api
{
    public static class Json
    {
        public static T FromString<T>(string json) where T : new()
        {
            var parsed = JSON.Parse(json).AsObject;
            return (T)NodeToObject(parsed, typeof(T));
        }

        public static string ToString<T>(T data)
        {
            return ObjectToJson(data).ToString();
        }

        private static object NodeToObject(JSONClass parsed, Type type)
        {
            object obj = type.GetConstructor(new Type[] { }).Invoke(new object[] { });
            foreach (var field in type.GetFields())
            {
                var fieldType = field.FieldType;
                var fieldVal = parsed[field.Name.ToLower()];
                object trueVal = null;
                if (fieldType.Equals(typeof(bool)))
                    trueVal = fieldVal.AsBool;
                else if (fieldType.Equals(typeof(float)))
                    trueVal = fieldVal.AsFloat;
                else if (fieldType.Equals(typeof(double)))
                    trueVal = fieldVal.AsDouble;
                else if (fieldType.Equals(typeof(int)))
                    trueVal = fieldVal.AsInt;
                else if (fieldType.Equals(typeof(long)))
                    trueVal = (long)fieldVal.AsInt;
                else if (fieldType.IsArray)
                    Log.Warn("TODO: Parse json arrays!");
                else if (fieldType.IsClass || fieldType.IsValueType)
                {
                    if (fieldType.IsSubclassOf(typeof(System.Collections.IDictionary)))
                        Log.Warn("TODO: Parse json dictionaries");
                    else
                        trueVal = NodeToObject(fieldVal.AsObject, fieldType);
                }
                field.SetValue(obj, trueVal);
            }
            foreach (var prop in type.GetProperties())
            {
                var propType = prop.PropertyType;
                var propVal = parsed[prop.Name.ToLower()];
                object trueVal = null;
                if (propType.Equals(typeof(bool)))
                    trueVal = propVal.AsBool;
                else if (propType.Equals(typeof(float)))
                    trueVal = propVal.AsFloat;
                else if (propType.Equals(typeof(double)))
                    trueVal = propVal.AsDouble;
                else if (propType.Equals(typeof(int)))
                    trueVal = propVal.AsInt;
                else if (propType.Equals(typeof(long)))
                    trueVal = (long)propVal.AsInt;
                else if (propType.Equals(typeof(string)))
                    trueVal = propVal.Value;
                else if (propType.IsArray)
                    Log.Warn("TODO: Parse json arrays!");
                else if (propType.IsClass || propType.IsValueType)
                {
                    if (propType.IsSubclassOf(typeof(System.Collections.IDictionary)))
                        Log.Warn("TODO: Parse json dictionaries");
                    else
                        trueVal = NodeToObject(propVal.AsObject, propType);
                }
                if (!(prop.GetSetMethod() is null))
                    prop.SetValue(obj, trueVal, null);
            }
            return obj;
        }


        private static object ObjectToJson<T>(T data)
        {
            var json = new JSONClass();
            foreach (var field in data.GetType().GetFields())
            {
                var fieldType = field.FieldType;
                var fieldVal = field.GetValue(data);
                var node = new JSONNode();
                if (fieldType.Equals(typeof(bool)))
                    node.AsBool = (bool)fieldVal;
                else if (fieldType.Equals(typeof(float)))
                    node.AsFloat = (float)fieldVal;
                else if (fieldType.Equals(typeof(double)))
                    node.AsDouble = (double)fieldVal;
                else if (fieldType.Equals(typeof(int)))
                    node.AsInt = (int)fieldVal;
                else if (fieldType.Equals(typeof(long)))
                    node.AsInt = (int)fieldVal;
                else if (fieldType.Equals(typeof(string)))
                    node.Value = (string)fieldVal;
                else if (fieldType.IsArray)
                    Log.Warn("TODO: JSON arrays");
                else if (fieldType.IsClass || fieldType.IsValueType)
                {
                    if (fieldType.IsSubclassOf(typeof(System.Collections.IDictionary)))
                        Log.Warn("TODO: JSON dictionaries");
                    else
                        Log.Warn("TODO: Json objects");
                }
                json.Add(field.Name, node);
            }
            foreach (var prop in data.GetType().GetProperties())
            {
                var propType = prop.PropertyType;
                var propVal = prop.GetValue(data, null);
                var node = new JSONNode();
                if (propType.Equals(typeof(bool)))
                    node.AsBool = (bool)propVal;
                else if (propType.Equals(typeof(float)))
                    node.AsFloat = (float)propVal;
                else if (propType.Equals(typeof(double)))
                    node.AsDouble = (double)propVal;
                else if (propType.Equals(typeof(int)))
                    node.AsInt = (int)propVal;
                else if (propType.Equals(typeof(long)))
                    node.AsInt = (int)propVal;
                else if (propType.Equals(typeof(string)))
                    node.Value = (string)propVal;
                else if (propType.IsArray)
                    Log.Warn("TODO: JSON arrays");
                else if (propType.IsClass || propType.IsValueType)
                {
                    if (propType.IsSubclassOf(typeof(System.Collections.IDictionary)))
                        Log.Warn("TODO: JSON dictionaries");
                    else
                        Log.Warn("TODO: Json objects");
                }
                json.Add(prop.Name, node);
            }
            return json;
        }
    }
}
