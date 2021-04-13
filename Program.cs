using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ZespolLib;

namespace ZespolTesting
{
    class Program
    {
        static void Main(string[] args)
        {
            KierownikZespolu adamKowalski =
                new KierownikZespolu(
                    "Adam", "Kowalski", 
                    "01.07.1990", 
                    "90070142412", Plcie.M, 
                    5);
            KierownikZespolu janNowak =
                new KierownikZespolu(
                    "Jan", "Nowak", 
                    "01.02.1970", 
                    "90070142412", 
                    Plcie.M, 12);

            Zespol GrupaIT = new Zespol("Grupa IT", adamKowalski);
            Zespol Grupa2 = new Zespol("Grupa 2", janNowak);
            
            var funkcje = new List<string>{ "sekretarz","programista","projektant","tester"};
            var dane = new List<Dane> 
            {
                new Dane
                {
                    Imie = "Witold", Nazwisko = "Adamski", DataUrodzenia = "22.10.1992", PESEL = "92102266738",
                    Plec = Plcie.M, Funkcja = "sekretarz"
                },
                new Dane
                {
                    Imie = "Jan", Nazwisko = "Janowski", DataUrodzenia = "15.03.1992", PESEL = "92031532652",
                    Plec = Plcie.M, Funkcja = "programista"
                },
                new Dane
                {
                    Imie = "Jan", Nazwisko = "But", DataUrodzenia = "16.05.1992", PESEL = "92051613915",
                    Plec = Plcie.M, Funkcja = "programista"
                },
                new Dane
                {
                    Imie = "Beata", Nazwisko = "Nowak", DataUrodzenia = "22.11.1993", PESEL = "93112225023",
                    Plec = Plcie.K, Funkcja = "projektant"
                },
                new Dane
                {
                    Imie = "Anna", Nazwisko = "Mysza", DataUrodzenia = "22.07.1991", PESEL = "91072235964",
                    Plec = Plcie.K, Funkcja = "projektant"
                }
            };

            Random rnd = new Random();
            for (int i = 0; i < 10; ++i)
            {
                dane.Add(new Dane
                    {Imie = Faker.Name.First(), Nazwisko = Faker.Name.Last(), 
                     DataUrodzenia = Faker.Identification.DateOfBirth().ToString("dd.MM.yyyy"), 
                     PESEL = GetRandPesel(rnd), Plec = rnd.Next(0, 10) % 2 == 0 ? Plcie.K : Plcie.M, 
                     Funkcja =  funkcje[rnd.Next(funkcje.Count)] });
            }
            
            var czlonkowie = new List<CzlonekZespolu>();
            foreach (var czlonek in dane)
            {
                czlonkowie.Add(new CzlonekZespolu(
                    czlonek.Imie, czlonek.Nazwisko, 
                    czlonek.DataUrodzenia, czlonek.PESEL, 
                    czlonek.Plec, czlonek.Funkcja, GrupaIT)
                );
            }

            dane.Clear();
            for (int i = 0; i < 10; ++i)
            {
                dane.Add(new Dane {Imie = Faker.Name.First(), Nazwisko = Faker.Name.Last(), DataUrodzenia = Faker.Identification.DateOfBirth().ToString("dd.MM.yyyy"), PESEL = GetRandPesel(rnd), Plec = (rnd.Next(0, 10) % 2 == 0 ? Plcie.K : Plcie.M), Funkcja =  funkcje[rnd.Next(funkcje.Count)] });
            }
            var czlonkowie2 = new List<CzlonekZespolu>();
            foreach (var czlonek in dane)
            {
                czlonkowie2.Add(new CzlonekZespolu(czlonek.Imie, czlonek.Nazwisko, czlonek.DataUrodzenia, czlonek.PESEL, czlonek.Plec, czlonek.Funkcja, Grupa2));
            }

            Console.WriteLine(GrupaIT.ToString());
            Console.WriteLine(Grupa2.ToString());
            var polaczonaGrupa = new Zespol("Grupa Polaczona", janNowak, GrupaIT, Grupa2);
            Console.WriteLine(polaczonaGrupa.ToString());
            
            Console.WriteLine("Czlonkowie o funkcji programista: ");
            foreach (CzlonekZespolu czlonek in polaczonaGrupa.WyszukajCzlonkow("programista"))
            {
                Console.WriteLine(czlonek.ToString());
            }
            
            Console.WriteLine("Ścieżka, {0}", Path.GetFullPath("../../../"));
            GrupaIT.ZapiszBin("../../../grupait.bin");
            GrupaIT.ZapiszXML("../../../grupait.xml");
            GrupaIT.ZapiszJSON("../../../grupait.json");
            GrupaIT.ZapiszYaml("../../../grupait.yml");
        }

        static string GetRandPesel(Random rnd)
        {
            string randomPESEL = "";
            for (int j = 0; j < 11; ++j)
            {
                randomPESEL += rnd.Next(0, 10).ToString();
            }

            return randomPESEL;
        }
        
        class Dane
        {
            public string Imie { get; set; }
            public string Nazwisko { get; set; }
            public string DataUrodzenia { get; set; }
            public string PESEL { get; set; }
            public Plcie Plec { get; set; }
            public string Funkcja { get; set; }
        }
    }
}
