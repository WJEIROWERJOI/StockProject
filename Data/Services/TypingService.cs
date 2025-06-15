using System.Collections;
using System.Xml;

namespace StockProject.Data.Services
{
    public class TypingService
    {
        private static readonly HttpClient client = new HttpClient();

        public static async Task<string> SearchWord(string word)
        {
            string apiKey = "4462B3F0F9B4655F5B449F2DA9E6056D";
            string url = $"https://krdict.korean.go.kr/api/search?key={apiKey}&q={word}";

            HttpResponseMessage response = await client.GetAsync(url);
            return await response.Content.ReadAsStringAsync();
        }

        public static List<string> GetWordDefinition(string xml)
        {
            // XmlDocument 객체 생성 및 XML 파일 로드
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xml);

            XmlNodeList itemNodes = xmlDoc.GetElementsByTagName("item");
            List<string> results = new();
            foreach (XmlNode item in itemNodes)
            {
                string word = item["word"]?.InnerText ?? "";
                string pos = item["pos"]?.InnerText ?? "";

                XmlNode senseNode = item["sense"];
                string definition = senseNode?["definition"]?.InnerText ?? "";
                string example = senseNode?["example"]?.InnerText ?? "";

                Console.WriteLine($"{pos} : {word} -> {definition}");
                results.Add(definition);
            }

            return results;
        }

        public string GenerateRandomWord()
        {
            Random random = new Random();

            int length = random.Next(2, 4);
            string str = string.Empty;

            for(int i = 0; i < length; i++)
            {
                int code = random.Next(0xAC00, 0xD7A4);
                str.Append((char)code);
            }

            return str;
        }

    }

}

