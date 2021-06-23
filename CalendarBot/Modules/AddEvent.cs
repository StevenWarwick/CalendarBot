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
    class AddEvent
    {
        static string[] Scopes = { CalendarService.Scope.Calendar };
        static string ApplicationName = "Google Calendar API .NET ";

        public static string Addevent(string eventInfo)
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
                //Console.WriteLine("Credential file saved to: " + credPath);
            }

            // Create Google Calendar API service.
            var service = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            string[] eventInfo1 = eventInfo.Split('|');

            Event newEvent = new Event()
            {
                Summary = eventInfo1[0],
                Location = eventInfo1[1],
                Description = eventInfo1[2],
                Start = new EventDateTime()
                {
                    DateTime = DateTime.Parse(eventInfo1[3]),
                    TimeZone = "America/Chicago",
                },
                End = new EventDateTime()
                {
                    DateTime = DateTime.Parse(eventInfo1[4]),
                    TimeZone = "America/Chicago",
                },
                //Recurrence = new String[] { "RRULE:FREQ=DAILY;COUNT=2" },
                //Attendees = new EventAttendee[] {
                //    //new EventAttendee() { Email = "lpage@example.com" },
                //    //new EventAttendee() { Email = "sbrin@example.com" },
                //},
                //Reminders = new Event.RemindersData()
                //{
                //    UseDefault = false,
                //    Overrides = new EventReminder[] {
                //    new EventReminder() { Method = "email", Minutes = 24 * 60 },
                //    new EventReminder() { Method = "sms", Minutes = 10 },
                //}
                //}
            };

            String calendarId = "primary";
            EventsResource.InsertRequest request = service.Events.Insert(newEvent, calendarId);
            Event createdEvent = request.Execute();
            return "Event created: " + createdEvent.HtmlLink;

        }

    }
}
