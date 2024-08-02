using Newtonsoft.Json;

namespace PizzeriaApp.Extensions
{
    public static class SessionExtensions
    {
        // Metodo per salvare un oggetto come JSON nella sessione
        public static void SetObjectAsJson(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        // Metodo per recuperare un oggetto JSON dalla sessione
        public static T GetObjectFromJson<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }
    }
}
