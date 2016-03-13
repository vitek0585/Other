using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crypt.Crypt;

namespace Crypt
{
    class Program
    {
        static void Main(string[] args)
        {
            Server<HttpRequest> server = new Server<HttpRequest>(new HybridDecrypt());
            User<HttpRequest> user = new User<HttpRequest>(new HybridEncrypt());

            while (true)
            {
                var pk = server.GetPuplicKey();
                var request = user.CreateRequest(pk);
                var line = server.DataRead(request);
                Console.WriteLine("Result - {0}", line);

                Console.WriteLine(new string('*', 50));
            }
        }
    }
}
