using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Text;

namespace MiddleWare.Slack
{
    public class SlackClient
    {
        private readonly string url = "https://hooks.slack.com/services/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";
        private readonly string channel = "#general";
        private readonly string username = "Application";

        public SlackClient(Exception ex)
        {
            var text = $"_*Exception in:  {DateTime.Now}*_ \n\n" +
                       $"*Message:*  \n" +
                       $"{ex.Message} \n" +
                       $"*Type:* {ex.GetType()} \n" +
                       $"*Source:* {ex.Source} \n" +
                       $"*StackTrace:* \n" +
                       $"{ex.StackTrace} \n" +
                       $"*TargetSite:*  \n" +
                       $"{ex.TargetSite} \n";
            PostMessage(text);
        }

        public void PostMessage(string text)
        {
            var json = new JObject
            {
                ["channel"] = channel,
                ["username"] = username,
                ["text"] = text,
            };
            PostMessage(json);
        }

        public void PostMessage(JObject json)
        {
            var serializedJson = JsonConvert.SerializeObject(json);
            var httpClient = new HttpClient();
            try
            {
                var content = new StringContent(serializedJson, Encoding.UTF8, "application/json");
                var result = httpClient.PostAsync(url, content).Result;
            }
            catch
            {
                Console.WriteLine("Ошибка соединения с интернетом");
            }
        }
    }
}
