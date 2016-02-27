using System;

namespace DecorAK
{
    class Program
    {
        static void Main(string[] args)
        {
            AK ak=new AK();
            PodstvolAK podstvol=new PodstvolAK();
            Console.WriteLine("Цена подствола {0}",podstvol.Coast());
            Console.WriteLine("Цена автомата {0}", ak.Coast());
            Console.WriteLine("описание подствола {0}", podstvol.Desccription());
            Console.WriteLine("описание автомата {0}", ak.Desccription());
            PodstvolAK podstvolAk = new PodstvolAK(ak);
            Console.Write("Обычный Ак - "); ak.Shoot();
            Console.Write("Ак с подстволом - "); podstvolAk.Shoot();
            Console.WriteLine("описание автомата с подстволом  {0}", podstvolAk.Desccription());
            podstvolAk.ModeShoot(1);
            Console.Write("Ак с подстволом режим переключен - "); podstvolAk.Shoot();
            Console.Write("Ак с подстволом цена - {0}", podstvolAk.Coast()); 
            Console.ReadKey();
        }
    }
}
