using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CalendarBot.Modules
{
    class GetEventInfo
    {
        static string[] Scopes = { CalendarService.Scope.Calendar };
        static string ApplicationName = "Google Calendar API .NET ";

        public static string getEventInfo(string eventInfo)
        {
            UserCredential credential;

            using (var stream =
                new FileStream("clientsecret.json", FileMode.Open, FileAccess.Read))
            {
                // The file token.json stores the user's access and refresh tokens, and is created
                // automatically when the authorization flow completes for the first time.
                string credPath = "token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
            }

            // Create Google Calendar API service.
            var service = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            EventsResource.ListRequest request = service.Events.List("primary");
            request.TimeMin = DateTime.Now;
            request.ShowDeleted = false;
            request.SingleEvents = true;
            request.MaxResults = 10;
            request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;

            // List events.
            Events events = request.Execute();
            //Console.WriteLine("Upcoming events:");
            if (events.Items != null && events.Items.Count > 0)
            {
                string s = "";
                foreach (var eventItem in events.Items)
                {

                    if (eventItem.Summary == eventInfo)
                    {
                        s += eventItem.Summary + " "+ eventItem.Description.ToString() + " " + eventItem.Start.DateTime.ToString() +  ": " + /*eventItem.HtmlLink +*/ eventItem.Id + "\n";
                    }
                    


                }
                return s;
            }
            else
            {
                return "No upcoming events found.";
            }
        }
    }
}
