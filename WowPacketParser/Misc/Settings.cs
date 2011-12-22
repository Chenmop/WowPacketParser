﻿using System;
using System.Configuration;
using System.Globalization;
using System.Collections.Generic;

namespace WowPacketParser.Misc
{
    public static class Settings
    {
        public static readonly Dictionary<string, int> intValues =
            new Dictionary<string, int>();
        public static readonly Dictionary<string, float> floatValues =
            new Dictionary<string, float>();
        public static readonly Dictionary<string, bool> boolValues =
            new Dictionary<string, bool>();
        public static readonly Dictionary<string, object> objectValues =
            new Dictionary<string, object>();
        public static readonly Dictionary<string, string> stringValues =
            new Dictionary<string, string>();
        public static readonly Dictionary<string, string[]> stringListValues =
            new Dictionary<string, string[]>();

        public static string GetString(string key, string defValue)
        {
            string aux;
            if (stringValues.TryGetValue(key, out aux))
                return aux;

            aux = ConfigurationManager.AppSettings[key];
            if (aux == null)
                aux = defValue;
            stringValues.Add(key, aux);
            return aux;
        }

        public static string[] GetStringList(string key, string[] defValue)
        {
            string[] aux;
            if (stringListValues.TryGetValue(key, out aux))
                return aux;

            var s = ConfigurationManager.AppSettings[key];
            if (s == null)
                aux = defValue;
            else
                aux = s.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            stringListValues.Add(key, aux);
            return aux;
        }

        public static bool GetBoolean(string key, bool defValue)
        {
            bool aux;
            if (boolValues.TryGetValue(key, out aux))
                return aux;

            var s = ConfigurationManager.AppSettings[key];
            if (s == null || !bool.TryParse(s, out aux))
                aux = defValue;

            boolValues.Add(key, aux);
            return aux;
        }

        public static int GetInt32(string key, int defValue)
        {
            int aux;
            if (intValues.TryGetValue(key, out aux))
                return aux;

            var s = ConfigurationManager.AppSettings[key];
            if (s == null || !int.TryParse(s, NumberStyles.Integer, CultureInfo.InvariantCulture, out aux))
                aux = defValue;

            intValues.Add(key, aux);
            return aux;
        }

        public static float GetFloat(string key, float defValue)
        {
            float aux;
            if (floatValues.TryGetValue(key, out aux))
                return aux;

            var s = ConfigurationManager.AppSettings[key];
            if (s == null || !float.TryParse(s, NumberStyles.Float, CultureInfo.InvariantCulture, out aux))
                aux = defValue;

            floatValues.Add(key, aux);
            return aux;
        }

        public static T GetEnum<T>(string key, T defValue)
        {
            return GetEnum<T>(key, defValue, true);
        }

        public static T GetEnum<T>(string key, T defValue, bool fromInt)
        {
            object aux;
            if (objectValues.TryGetValue(key, out aux))
                return (T)aux;

            var s = ConfigurationManager.AppSettings[key];
            if (s == null)
                aux = defValue;
            else
            {
                if (fromInt)
                {
                    int value;
                    if (!int.TryParse(s, NumberStyles.Integer, CultureInfo.InvariantCulture, out value))
                        aux = defValue;
                    else
                        aux = (object)value;
                }
                else
                {
                    // cant use Enum.TryParse as that is not supported in .NET 3.5
                    if (!Enum.IsDefined(typeof(T), s))
                        aux = defValue;
                    else
                        aux = Enum.Parse(typeof(T), s);
                }
            }

            objectValues.Add(key, aux);
            return (T)aux;
        }
    }
}
