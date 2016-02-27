using System;
using System.Data.Entity;

namespace _003_Migrations
{
    class Program
    {
        static void Main(string[] args)
        {
            //Database.SetInitializer(new DropCreateDatabaseIfModelChanges<BlogContext>());

            using (var db = new BlogContext())
            {
                db.Blogs.Add(new Blog { Name = "Another Blog " });
                db.SaveChanges();

                foreach (var blog in db.Blogs)
                {
                    Console.WriteLine(blog.Name);
                }
            }

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey(); 
        }
    }
}

//Команды для миграции 

//Install-Package EntityFramework  
//Enable-Migrations –EnableAutomaticMigrations
//Update-Database
//Add-Migration Name Migrations
//Update-Database –Verbose