using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EventPlanning.Models
{
    public class DbInitializer:DropCreateDatabaseAlways<EventContext>
    {
        protected override void Seed(EventContext db)
        {
            //db.Events.Add(new Event { NameEvent = "Google", Address = "Brest", Description = "Meetup" });
            //db.Events.Add(new Event { NameEvent = "Yandex", Address = "Minsk", Description = "Meetup" });
            //db.Events.Add(new Event { NameEvent = "Mail.ru", Address = "Vitebsk", Description = "Meetup" });

            //base.Seed(db);
        }
    }
}