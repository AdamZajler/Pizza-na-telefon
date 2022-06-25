﻿using System;
using System.Threading;
using System.Collections.Generic;
using System.Text.Json;
using System.IO;
//using System.Media;
//using System.ComponentModel;

namespace Product_na_telefon
{
    public class Product
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int[] IngredientsId { get; set; }
        public string CurrencySymbol { get; set; }
    }
    public class Ingredient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int[] TagsId { get; set; }
    }
    public static class GlobalData
    {
        public static List<Product> Menu = new List<Product>();
        public static List<int> Order = new List<int>();
        public static string KlientImie;
        public static string KlientNazwisko;
        public static string KlientAdres;
        public static string KlientUwagiDoZamowienia;
        public static int KlientMetodaPlatnosci;
        public static string AdminLogin = "kobe";
        public static string AdminPass = "technik2";

    };

    public static class Functions
    {
        public static string CustomConsoleWriteLine(string text, string text_color = "white", bool is_centered = false)
        {
            text_color = text_color.ToLower();

            switch (text_color)
            {
                case "red":
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case "green":
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
            return null;
        }
    }

    public class Admin
    {
        public static void Login()
        {
            bool loginPassed = false;
            do
            {
                Console.Clear();
                Console.Write("Podaj login: ");
                string login = Console.ReadLine();
                Console.Write("Podaj hasło: ");
                var pass = string.Empty;

                ConsoleKey key;
                do
                {
                    var keyInfo = Console.ReadKey(intercept: true);
                    key = keyInfo.Key;

                    if (key == ConsoleKey.Backspace && pass.Length > 0)
                    {
                        Console.Write("\b \b");
                        pass = pass[0..^1];
                    }
                    else if (!char.IsControl(keyInfo.KeyChar))
                    {
                        Console.Write("*");
                        pass += keyInfo.KeyChar;
                    }
                } while (key != ConsoleKey.Enter);

                if (login == GlobalData.AdminLogin && pass == GlobalData.AdminPass)
                {
                    Functions.CustomConsoleWriteLine("\n\n Poprawne hasło! Przekierowywanie...", "green", true);
                    Thread.Sleep(1000);
                    Panel();
                    return;
                }
                else
                {
                    Functions.CustomConsoleWriteLine("\n\nLogin i hasło nie są zgodne! Wpisz '0' aby wrócić do menu głównego lub dowolny inny klawisz aby spróbować ponownie!", "red", true);

                    var keyInfo = Console.ReadKey(intercept: true);
                    key = keyInfo.Key;

                    if (key == ConsoleKey.D0)
                    {
                        return;
                    }
                }
            } while (loginPassed == false);
        }

        public static void Panel()
        {
            int option = 0;
            do
            {
                Console.Clear();
                Console.WriteLine("Witaj w menu admina!");
                Console.WriteLine("Wybierz akcję: ");
                Console.WriteLine("1. Dodaj produkt");
                Console.WriteLine("2. Edytuj produkt");
                Console.WriteLine("3. Dodaj składnik");
                Console.WriteLine("4. Edytuj składnik");
                Console.WriteLine("-1. Wróć do menu głównego");
                try
                {
                    option = int.Parse(Console.ReadLine());
                }
                catch
                {

                }
            } while (option != -1);


        }
    }

    public class Pizzeria
    {
        public void WyborKategorii()
        {
            bool hide_menu = false;
            int selectedCategory = 0;
            do
            {
                Functions.CustomConsoleWriteLine("\n1. Napoje", "", false);
                Functions.CustomConsoleWriteLine("2. Pizze", "", false);
                Functions.CustomConsoleWriteLine("-1. Logowanie do panelu admina", "", false);
                Console.Write("\nTwój wybór: ");
                try
                {
                    selectedCategory = int.Parse(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("\nPodaj liczbe!");
                }
                
                if (selectedCategory == -1)
                {
                    Admin.Login();
                }
                else if (selectedCategory != 0)
                {
                    hide_menu = this.Menu(selectedCategory);
                }

                selectedCategory = 0;
            } while (hide_menu == false);

            return;
        }

        public bool CheckMenuPosition(int menuPosition)
        {
            bool isInMenu = false;

            foreach(Product single_product in GlobalData.Menu)
            {
                if(single_product.Id == menuPosition)
                {
                    isInMenu = true;
                }
            }

            return isInMenu;
        }

        public Pizzeria()
        {

            Functions.CustomConsoleWriteLine("Witaj w naszej Pizzerii Italiano! ඞ", "green", true);
            Functions.CustomConsoleWriteLine("Zapraszamy do złożenia zamówienia", "", true);
            //Thread.Sleep(2500); //przerwa w wykonywaniu kodu na 2.5s

            this.WyborKategorii();

            bool hide_menu = false;

            do
            {
                int value = -1;

                Console.Write("\nWybierz pozycje ('0' powrót do kategori)\n");

                while(value != 0)
                {
                    Console.Write("Wybrana pozycja: ");
                    try
                    {
                        value = int.Parse(Console.ReadLine());
                    }
                    catch
                    {
                        Console.WriteLine("Podaj liczbe!\n");
                        value = -1;
                    }

                    if (this.CheckMenuPosition(value) && value != -1)
                    {
                        GlobalData.Order.Add(value);
                    }
                    else if(value != -1)
                    {
                        Console.WriteLine("Podanej pozycji nie ma w menu!\n");
                    }

                }

            } while (hide_menu == false);
            
            /* Console.WriteLine("\n\nAby zamknąć program w dowolnej chwili kliknij klaiwsz 'esc'");
            if (Console.ReadKey().Key == ConsoleKey.Escape)
            {
                this.KoniecProgramu();
            } */
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
        public bool Menu(int selectedCategory)
        {
            bool menu_function_success = false;

            void displayCategory(int category)
            {
                Console.Write("\n");
                foreach (Product single_category in GlobalData.Menu)
                {
                    if(single_category.CategoryId == category)
                    {
                        Console.Write("{0}. {1} \n", single_category.Id, single_category.Name);
                    }
                }
            }

            foreach (Product single_category in GlobalData.Menu)
            {
                if (single_category.CategoryId == selectedCategory)
                {
                    displayCategory(selectedCategory);
                    menu_function_success = true;
                    break;
                }
            }
            
            if(menu_function_success == false)
            {
                Functions.CustomConsoleWriteLine("Nie ma takiej opcji w menu", "red", false);
            }

            return menu_function_success;


            /* Console.WriteLine("\n\n");
            Functions.CustomConsoleWriteLine("Napoje", "green", true); //środek
            Console.WriteLine("\n");
            Functions.CustomConsoleWriteLine("1.Cola           \t7zł","", true);
            Functions.CustomConsoleWriteLine("2.Fanta          \t7zł","",true);
            Functions.CustomConsoleWriteLine("3.Sprite          \t7zł","",true);
            Functions.CustomConsoleWriteLine("4.Woda gazowana  \t7zł","",true);
            Functions.CustomConsoleWriteLine("5.Woda niegazowana\t7zł","",true);

            Console.WriteLine("\n\n");
            Functions.CustomConsoleWriteLine("Pizza", "green", true); //środek
            Console.WriteLine("\n");
            Functions.CustomConsoleWriteLine("6.Margherita \t\t18zł","", true);
            Functions.CustomConsoleWriteLine("7.Neapolitana\t\t22zł","", true);
            Functions.CustomConsoleWriteLine("8.Carbonara  \t\t25zł", "", true);
            Functions.CustomConsoleWriteLine("9.Mexico      \t30zł", "", true);
            Functions.CustomConsoleWriteLine("10.Rukola    \t\t35zł", "", true);
            Functions.CustomConsoleWriteLine("11.Hawaii    \t\t15zł", "", true); */

            //bool koniec_wprowadzania = false;
            //do
            //{
            //    if (GlobalData.Menu.Count < 1) {
            //        Functions.CustomConsoleWriteLine("\nCo zamawiasz (podaj numer pozycji)?");
            //    }
            //    else
            //    {
            //        Functions.CustomConsoleWriteLine("\nCzy chcesz zamówić coś jeszcze? ('0' => koniec składania zamówienia)");
            //    }

            //    var value = int.Parse(Console.ReadLine());

            //    if (value == 0)
            //    {
            //        koniec_wprowadzania = true;
            //        this.Szczegoly_Zamowienia();
            //        return;
            //    }
            //    else
            //    {
            //        //GlobalData.Menu.Add(value);
            //    }

            //} while (koniec_wprowadzania == false);
        }

        public void Szczegoly_Zamowienia()
        {
            Console.WriteLine("czyszczenie + przejscie do zamowienia' czyszczenie 3s, może jakiś loader?");
            //Thread.Sleep(3000);
            Console.Clear();
            Functions.CustomConsoleWriteLine("Wprowadź swoje imie","Green", false);
            GlobalData.KlientImie = Console.ReadLine();
            Functions.CustomConsoleWriteLine("Wprowadź swoje nazwisko","Green", false);
            GlobalData.KlientNazwisko = Console.ReadLine();
            Functions.CustomConsoleWriteLine("Wprowadź miasto, ulice i numer domu/mieszkania","Green", false);
            GlobalData.KlientAdres = Console.ReadLine();
            Functions.CustomConsoleWriteLine("Uwagi do zamówienia", "Green", false);
            GlobalData.KlientUwagiDoZamowienia = Console.ReadLine();
            Functions.CustomConsoleWriteLine("Dostępne metody płatności:", "Green", false);
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
            //SoundPlayer player = new SoundPlayer();
            //player.SoundLocation = "../../../../src/data/menu.wav";
            //player.Play();

            //Console.Beep(800, 1000);
            string fileName = "../../../../src/data/menu.json";
            string menuJSON = File.ReadAllText(fileName);
            GlobalData.Menu = JsonSerializer.Deserialize<List<Product>>(menuJSON);
            Console.WriteLine("Załadowano {0} pozycji w menu", GlobalData.Menu.Count);
            //var functions = new Functions();
            var pizzeria = new Pizzeria();
        }
    }
}
