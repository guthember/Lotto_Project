using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Lottos
{
    class Lotto
    {
        public int Ev { get; set; }
        public int Het { get; set; }
        public DateTime Huzas { get; set; }        
        public int[] Szamok { get; set; }

        public Lotto(string sor)
        {
            string[] m = sor.Split(';');
            Ev = int.Parse(m[0]);
            Het = int.Parse(m[1]);
            if (m[2] != "")
                Huzas = DateTime.Parse(m[2]);            
            Szamok = new int[5];
            for (int i = 0; i < Szamok.Length; i++)
            {
                Szamok[i] = int.Parse(m[11 + i]);
            }                           
        }

        public int Hossz
        {
            get
            {
                for (int i = 0; i < 4; i++)
                {
                    int x = 5 - i;
                    for (int j = 4; j >= (4 - i); j--)
                    {
                        if (Szamok[j] - Szamok[j - x + 1] == (x - 1))
                            return x;
                    }                    
                }
                return 1;               
            }
        }

        public int Osszeg
        {
            get { return Szamok.Sum(); }
        }

        public int Hasonlo
        {
            get
            {
                List<int> Jegyek = new List<int>();
                foreach (int item in Szamok)
                {
                    int tizes = item / 10;
                    if (item > 0)
                        Jegyek.Add(tizes);
                    Jegyek.Add(item - (tizes * 10));
                }
                int max = 0;
                for (int i = 0; i < 10; i++)
                {
                    int db = Jegyek.Count(n => n == i);
                    if (db > max)
                        max = db;
                }
                return max;
            }  
        }        
    }

    class Program
    {
        static void Main(string[] args)
        {
            List<Lotto> lottok = new List<Lotto>();
            foreach (string item in File.ReadAllLines("otos.csv"))
            {
                lottok.Add(new Lotto(item));
            }   

            Console.WriteLine("3 leghosszabb sorozatot tartalmazó számsor:");
            var hosszuk = lottok.OrderByDescending(n => n.Hossz).Take(3);
            StreamWriter sw = new StreamWriter("numbersLongest.txt");
            foreach (var item in hosszuk)
            {
                sw.WriteLine(item.Hossz);
                for (int i = 0; i < item.Szamok.Length; i++)
                {
                    sw.Write($"{item.Szamok[i]};");
                }                
                sw.WriteLine();
            }
            sw.Close();

            var kisOsszeguek = lottok.OrderBy(n => n.Osszeg).Take(3);
            Console.WriteLine("3 legkisebb összegű számsor:");
            sw = new StreamWriter("numbersLeast.txt");
            foreach (var item in kisOsszeguek)
            {
                sw.WriteLine(item.Osszeg);
                for (int i = 0; i < item.Szamok.Length; i++)
                {
                    sw.Write($"{item.Szamok[i]};");
                }
                sw.WriteLine();
            }
            sw.Close();
            Console.WriteLine("Leghasonlóbb számsorok:");
            var hasonlok = lottok.OrderByDescending(n => n.Hasonlo).Take(2);
            sw = new StreamWriter("numbersSimilar.txt");
            foreach (var item in hasonlok)
            {
                sw.WriteLine("{0};{1}", item.Ev, item.Het);                
                for (int i = 0; i < item.Szamok.Length; i++)
                {
                    sw.Write($"{item.Szamok[i]};");
                }
                sw.WriteLine();
            }
            sw.Close();
            Console.ReadKey();
        }
    }
}
