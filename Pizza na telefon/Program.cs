﻿using System;
using System.Threading;
using System.Collections.Generic;

namespace Pizza_na_telefon
{
    public static class GlobalData
    {
        public static List<int> MenuPositions = new List<int>();
        public static string KlientImie;
        public static string KlientNazwisko;
        public static string KlientAdres;
        public static string KlientUwagiDoZamowienia;
        public static int KlientMetodaPlatnosci;
    };
    public class Funtions
    {
        public void CustomConsoleWriteLine(string text, string  text_color = "white", bool is_centered = false)
        {
            switch (text_color)
            {
                case "red":
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case "green":
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case "Green":
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
            }
            
            if (is_centered)
            {
                Console.SetCursorPosition((Console.WindowWidth - text.Length) / 2, Console.CursorTop);
                Console.WriteLine(text);
            }
            else
            {
                Console.WriteLine(text);
            }

            Console.ResetColor();
        }

        public dynamic MenuData(int position, string return_data)
        {
            string pozycja = "";
            double cena = 0;

            switch (position) {
                case 1:
                    pozycja="Pizza Cipolleo";
                    cena = 69.420;
                    break;
                default:
                    Console.WriteLine("Brak pozycji w menu");
                    break;
            }

            switch (return_data)
            {
                case "price":
                    return cena;
                case "name":
                    return pozycja;
                default:
                    Console.WriteLine("Nie ma takiego typu zwracania danych");
                    break;
            }

            return 0;
        }
    }

    public class Pizzeria
    {
        public Pizzeria()
        {
            var functions = new Funtions();

            functions.CustomConsoleWriteLine("Witaj w naszej Pizzeri Italiano", "green", true);
            functions.CustomConsoleWriteLine("Zapraszamy do złożenia zamówienia", "", true);
            //Thread.Sleep(2500); //przerwa w wykonywaniu kodu na 2.5s
            functions.CustomConsoleWriteLine("\nOto menu dla Ciebie", "", true);

            this.Menu();

            Console.WriteLine("\n\nAby zamknąć program w dowolnej chwili kliknij klaiwsz 'esc'");
            if (Console.ReadKey().Key == ConsoleKey.Escape)
            {
                this.KoniecProgramu();
            }
        }

        public void Loader()
        {
            Console.Write("Ładowanie... //");
            for(int i=0; i<3; i++)
            {
                Console.Write("\\//\\");
                Thread.Sleep(850);
            }
        }
        public void Menu()
        {
            var functions = new Funtions();
            Console.WriteLine("\n\n");
            functions.CustomConsoleWriteLine("Napoje", "green", true); //środek
            Console.WriteLine("\n");
            functions.CustomConsoleWriteLine("1.Cola           \t7zł","", true);
            functions.CustomConsoleWriteLine("2.Fanta          \t7zł","",true);
            functions.CustomConsoleWriteLine("3.Sprite          \t7zł","",true);
            functions.CustomConsoleWriteLine("4.Woda gazowana  \t7zł","",true);
            functions.CustomConsoleWriteLine("5.Woda niegazowana\t7zł","",true);

            Console.WriteLine("\n\n");
            functions.CustomConsoleWriteLine("Pizza", "green", true); //środek
            Console.WriteLine("\n");
            functions.CustomConsoleWriteLine("6.Margherita \t\t18zł","", true);
            functions.CustomConsoleWriteLine("7.Neapolitana\t\t22zł","", true);
            functions.CustomConsoleWriteLine("8.Carbonara  \t\t25zł", "", true);
            functions.CustomConsoleWriteLine("9.Mexico      \t30zł", "", true);
            functions.CustomConsoleWriteLine("10.Rukola    \t\t35zł", "", true);
            functions.CustomConsoleWriteLine("11.Hawaii    \t\t15zł", "", true);

            bool koniec_wprowadzania = false;
            do
            {
                if (GlobalData.MenuPositions.Count < 1) {
                    functions.CustomConsoleWriteLine("\nCo zamawiasz (podaj numer pozycji)?" + GlobalData.MenuPositions.Count);
                }
                else
                {
                    functions.CustomConsoleWriteLine("\nCzy chcesz zamówić coś jeszcze? ('0' => koniec składania zamówienia)");
                }

                var value = int.Parse(Console.ReadLine());

                if (value == 0)
                {
                    koniec_wprowadzania = true;
                    this.Szczegoly_Zamowienia();
                    return;
                }
                else
                {
                    GlobalData.MenuPositions.Add(value);
                }

            } while (koniec_wprowadzania == false);
        }

        public void Szczegoly_Zamowienia()
        {
            var functions = new Funtions();
            Console.WriteLine("czyszczenie + przejscie do zamowienia' czyszczenie 3s, może jakiś loader?");
            //Thread.Sleep(3000);
            Console.Clear();
            functions.CustomConsoleWriteLine("Wprowadź swoje imie","Green", false);
            GlobalData.KlientImie = Console.ReadLine();
            functions.CustomConsoleWriteLine("Wprowadź swoje nazwisko","Green", false);
            GlobalData.KlientNazwisko = Console.ReadLine();
            functions.CustomConsoleWriteLine("Wprowadź miasto, ulice i numer domu/mieszkania","Green", false);
            GlobalData.KlientAdres = Console.ReadLine();
            functions.CustomConsoleWriteLine("Uwagi do zamówienia", "Green", false);
            GlobalData.KlientUwagiDoZamowienia = Console.ReadLine();
            functions.CustomConsoleWriteLine("Dostępne metody płatności:", "Green", false);
            Console.WriteLine("\n1.Karta płatnicza\n2.Płatność gotówką przy odbiorze");
            bool bledna_platnosc = false;
            do
            {
                Console.WriteLine("\nWybierz metode płatności");
                try
                {
                    GlobalData.KlientMetodaPlatnosci = int.Parse(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("To nie liczba byczq");
                }

                switch (GlobalData.KlientMetodaPlatnosci)
                {
                    case 1 :
                        Console.WriteLine("wybrana metoda płatności to płatność kartą");
                        bledna_platnosc = true;
                        break;
                    case 2:
                        Console.WriteLine("wybrana metoda płatności to płatność gotówką przy odbiorze");
                        bledna_platnosc = true;
                        break;
                    default:
                        Console.WriteLine("Wybrana metoda płatności nie istnieje");
                        break;
                }

            } while (bledna_platnosc == false);


            Console.WriteLine("Naciśnij dowolny klawisz aby przejśc do podsumowania");
            Console.ReadKey();
            this.Podsumowanie();
        }

        public void Podsumowanie()
        {
            Console.WriteLine("czyszczenie 3s, może jakiś loader?");
            Thread.Sleep(3000);
            Console.Clear();

            Console.WriteLine("Tutaj wypisujemy elementy zamówienia + dane osobowe. wszystko ładnie wyśietlone i wgl. Trzeba pomyśleć nad obiegktem elementów menu tak by można było je wywoływać po numerkach.");

            Console.WriteLine("Naciśnij dowolny klawisz aby przejśc do dalej");
            Console.ReadKey();
            this.ThankYouPage();
        }

        public void ThankYouPage()
        {
            Console.WriteLine("czyszczenie 3s, może jakiś loader?");
            Thread.Sleep(3000);
            Console.Clear();
            Console.WriteLine("Dziekujemy za złozenie zamowienia i wgl, moze damy tu jakies smieszne emotki? ");
            Console.WriteLine("𓁹‿𓁹");
        }
        public void KoniecProgramu()
        {
            Console.WriteLine("Koniec programu");
            return;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var functions = new Funtions();
            var pizzeria = new Pizzeria();
        }
    }
}
