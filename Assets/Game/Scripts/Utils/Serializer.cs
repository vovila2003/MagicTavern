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

        public static T DeserializeObject<T>(string value)
        {
            return JsonConvert.DeserializeObject<T>(value);
        }
    }
}