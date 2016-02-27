using System;
using System.Data.Entity;
using System.Linq;

namespace _007_CodeFirst
{
    class Program
    {
        static void Main()
        {
            Database.SetInitializer(new ManInitializer());

            using (var db = new ManContext())
            {
                var mans = db.Men.ToList();
                foreach (var man in mans)
                {
                    Console.WriteLine("{0}.{1}", man.ManID, man.Name);
                }
            }
            Console.ReadLine();
        }
    }
}
