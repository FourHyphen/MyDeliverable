using Newtonsoft.Json;

namespace MyUtility
{
    public static class Json
    {
        public static T CreateInstance<T>(string jsonFilePath) where T : class
        {
            string jsonStr = System.IO.File.ReadAllText(jsonFilePath);
            return JsonConvert.DeserializeObject<T>(jsonStr);
        }

        /// <summary>
        /// Jsonファイルを整形状態で新規作成する
        /// すでに存在する場合は上書きする
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="filePath"></param>
        public static void CreateFile<T>(T instance, string filePath)
        {
            string jsonData = JsonConvert.SerializeObject(instance, Formatting.Indented);
            System.IO.File.WriteAllText(filePath, jsonData);
        }

        public static T Read<T>(string jsonPath)
        {
            string str = System.IO.File.ReadAllText(jsonPath);
            return JsonConvert.DeserializeObject<T>(str);
        }
    }
}
