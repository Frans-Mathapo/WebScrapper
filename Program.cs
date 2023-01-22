using HtmlAgilityPack;
using System;
using Slack.Webhooks;
using Newtonsoft.Json;
using WebScrapper;

class Program
{
    static void Main(string[] args)
    {
        string apiToken = "xapp -1-A04HZ53LZM3-4704213407216-2ec42ee02b94c121cfef3c8c7ae86a575815cbed0695c592c44e889d5371e6d9";
        string channelId = "C04B5PSTB70";
        string requestUrl = $"https://slack.com/api/conversations.history?token={apiToken}&channel={channelId}";

        // Send the API request
        using (var httpClient = new HttpClient())
        {
            var response = httpClient.GetAsync(requestUrl).Result;
            var responseJson = response.Content.ReadAsStringAsync().Result;

            // Deserialize the JSON response
            var responseData = JsonConvert.DeserializeObject<SlackResponse>(responseJson);

            // Extract the messages from the response
            var messages = responseData.messages;

            if (responseData.messages == null || !responseData.messages.Any())
            {
                // Handle empty response
                Console.WriteLine("No messages found");
                return;
            }
            else
            {
                // Print the messages to the console
                foreach (var message in messages)
                {
                    // Get the sender's name from the API response
                    var sender = responseData.users.FirstOrDefault(x => x.id == message.user)?.name;

                    // Get the timestamp of the message
                    var timestamp = message.ts;
                    DateTime messageDate = DateTimeOffset.FromUnixTimeSeconds((long)Double.Parse(timestamp)).DateTime;
                    // Print the sender's name, message, and date
                    Console.WriteLine($"Sender: {sender}  Message: {message.text} Date: {messageDate.ToString("yyyy-MM-dd HH:mm:ss")}");
                }
            }
        }

    }
}