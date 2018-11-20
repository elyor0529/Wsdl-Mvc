using Newtonsoft.Json;

namespace DemoMvc.Extensions
{
    public static class JsonExtension
    {
        public static string Encode<T>(this T t)
        {
            return JsonConvert.SerializeObject(t, Formatting.None, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
                PreserveReferencesHandling = PreserveReferencesHandling.Objects
            });
        }

        public static T Decode<T>(this string s)
        {
            return JsonConvert.DeserializeObject<T>(s, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
                PreserveReferencesHandling = PreserveReferencesHandling.Objects
            });
        }
    }
}
