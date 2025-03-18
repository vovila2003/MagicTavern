using System;
using JetBrains.Annotations;
using Unity.Plastic.Newtonsoft.Json;

namespace Tavern.Utils
{
    public static class Serializer
    {
        public static string SerializeObject([CanBeNull] object value)
        {
            return JsonConvert.SerializeObject(value);
        }

        public static (T, bool) DeserializeObject<T>(string value)
        {
            T t;
            try
            {
                t = JsonConvert.DeserializeObject<T>(value);
            }
            catch (Exception)
            {
                t = default;
                return (t, false);
            }

            return (t, true);
        }
    }
}