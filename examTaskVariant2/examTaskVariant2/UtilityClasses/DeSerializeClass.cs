using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace examTaskVariant2.UtilityClasses
{
    public static class DeSerializeClass
    {
        public static T DeserializeObject<T>(string jsonStr)
        {
            return JsonConvert.DeserializeObject<T>(jsonStr);
        }

        public static string GetValueFromJsonString(string jsonStr, string key)
        {
            return JObject.Parse(jsonStr).SelectToken(key).ToString();
        }

        public static T GetObjectFromArrayJson<T>(string content, int index)
        {
            JArray jsonArray = JArray.Parse(content);
            var answer = jsonArray[index].ToObject<T>();

            return answer;
        }
    }
}
