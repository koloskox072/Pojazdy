using MySql.Data;
using MySql.Data.MySqlClient;

namespace Pojazdy
{
    class Auto
    {
        public int ID, rocznik, przebieg, cena;
        public string marka, model, kolor;
        public Auto(int ID, string marka, string model, string kolor, int rocznik, int przebieg, int cena)
        {
            this.ID = ID;
            this.marka = marka;
            this.model = model;
            this.kolor = kolor;
            this.rocznik=rocznik;
            this.przebieg=przebieg;
            this.cena = cena;
        }
        public override string ToString()
        {
            return $"{ID}: {marka} {model} {kolor} {rocznik} {przebieg} {cena}";
        }
    }
    internal class Program
    {   
        static List<Auto> list = new List<Auto>();
        static int a = 0;
        static string co = "";
        static void next()
        {
            a++;
            Console.WriteLine(list[a]);
        }
        static void last()
        {
            a--;
            Console.WriteLine(list[a]);
        }
        static void Main(string[] args)
        {
            int i = 0;
            string adres = "server=localhost;user=root;database=firma_koper;port=3306;password=";
            string zapytanie1 = $"SELECT * FROM pojazd";
            
            MySqlConnection conn = new MySqlConnection(adres);
            MySqlCommand wynik = new MySqlCommand(zapytanie1, conn);
            conn.Open();
            MySqlDataReader rdr = wynik.ExecuteReader();
            while (rdr.Read()) 
            {
                /*i++;
                if(int.Parse(rdr[0].ToString())!=i)
                {
                    list.Add(new Auto(i, "0", "0", "0", 0, 0, 0));
                }
                else

                Sprawdzanie czy jest w liscie

                */
                list.Add(new Auto(int.Parse(rdr[0].ToString()), rdr[1].ToString(),
                    rdr[2].ToString(), rdr[3].ToString(), int.Parse(rdr[4].ToString()),
                    int.Parse(rdr[5].ToString()), int.Parse(rdr[6].ToString())));
            }
            Console.WriteLine(list[0]);
            while (true)
            {
                
                Console.Write("P - poprzedni Q - koniec N - następny: ");
                co = Console.ReadLine().ToUpper();
                if (co == "N")
                {
                    a++;
                    if (a >= 0 && a <= 100)
                    {
                        a--;
                        next();
                    }
                    else
                    {
                        Console.WriteLine("Probujesz wyjsc poza baze danych");
                        a--;
                    }
                }
                if (co == "Q")
                break;
                if (co == "P")
                {
                    a--;
                    if (a >= 0 && a <= 100)
                    {
                        a++;
                        last();
                    }
                    else
                    {
                        Console.WriteLine("Probujesz wyjsc poza baze danych");
                        a++;
                    }
                }
            }
            rdr.Close();
            conn.Close();
        }
    }
}