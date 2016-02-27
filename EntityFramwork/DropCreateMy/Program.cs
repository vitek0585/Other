using System;
using System.Linq;
using System.Data.Entity;
using CF.DataAccess;

//Использование Fluent API для настройки таблицы.
namespace _002_CF_CreateDB
{
    class Program
    {
        static void Main()
        {
            Database.SetInitializer(new DropCreateAttendeeDb());

            using (var db = new CodeContext())
            {
                var query = from attendees in db.Attendees
                                     select attendees;

                Console.WriteLine(query);
                Console.ReadKey();

                Console.WriteLine("Count - {0}", query.Count());
                Console.ReadKey();

                foreach (var item in query)
                {
                    Console.WriteLine("{0}.{1} <{2}>", item.AttendeeTrackingID, item.LastName, item.DateAdded);
                }
            }

            Console.ReadKey();
        }
    }
}
