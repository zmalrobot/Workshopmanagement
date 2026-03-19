using Newtonsoft.Json;

namespace Workshopmanagement.Utility
{
    public static class ObjectComparerUtility
    {
        private static JsonSerializerSettings JsonSerializer { get; set; } = new JsonSerializerSettings()
        {
            DateFormatString = "yyyy-MM-dd HH:mm:ss"
        };

        #region ObjectsAreEqual
        /// <summary>
        /// Barba trucco per confrontare 2 oggetti senta star li a impazzire con le reflection
        /// Vantaggi: Veloce e semplice e generico
        /// Svantaggi: Non so quali proprietà sono cambiati
        /// Un giorno che non piove innesto anche quello completo
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj1"></param>
        /// <param name="obj2"></param>
        /// <returns></returns>
        public static bool ObjectsAreEqual<T>(T obj1, T obj2)
        {
            var obj1Serialized = Newtonsoft.Json.JsonConvert.SerializeObject(obj1, JsonSerializer);
            var obj2Serialized = Newtonsoft.Json.JsonConvert.SerializeObject(obj2, JsonSerializer);

            return obj1Serialized == obj2Serialized;
        }
        #endregion
    }
}

