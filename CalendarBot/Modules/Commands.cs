using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord.Commands;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System.IO;
using System.Threading;
using System.Threading;
using CalendarBot.Modules;
using Discord;
using Discord.WebSocket;

namespace CalendarBot.Modules
{
    public class Commands : ModuleBase<SocketCommandContext>
    {

        [Command("test")]
        public async Task Ping()
        {
            await ReplyAsync("Connected");
        }

        [Command("CalTest")]
        public async Task CalStart()
        {
            await ReplyAsync(CalendarBot.Modules.CalendarEvents.Calender());
        }



        [Command("addEvent")]
        public async Task AddEvent([Remainder] string eventInfo)
        {
            await ReplyAsync(CalendarBot.Modules.AddEvent.Addevent(eventInfo));
        }

        [Command("deleteEvent")]
        public async Task DeleteEvent([Remainder] string EventInfo)
        {
            await ReplyAsync(CalendarBot.Modules.DeleteEvent.deleteEvent(EventInfo));
        }

        [Command("GetEventInfo")]
        public async Task GetEventInfo([Remainder] string eventInfo)
        {
            await ReplyAsync(CalendarBot.Modules.GetEventInfo.getEventInfo(eventInfo));
        }
    }
}