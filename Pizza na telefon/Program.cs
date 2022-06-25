using System;
using System.Threading;
using System.Collections.Generic;
using System.Text.Json;
using System.IO;
using System.Linq;
using System.Media;

namespace Product_na_telefon
{
    interface IProduct
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int[] IngredientsId { get; set; }
        public string CurrencySymbol { get; set; }
    }
    public class Product : IProduct
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
        public int IngredientId { get; set; }
        public string IngredientName { get; set; }
        public int[] IngredientPrice { get; set; }
    }
    public static class GlobalData
    {
        public static List<Product> Menu = new List<Product>();
        public static List<Product> Order = new List<Product>();
        public static List<Ingredient> Ingredients = new List<Ingredient>();
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
                case "orange":
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    break;
                case "yellow":
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case "blue":
                    Console.ForegroundColor = ConsoleColor.Blue;
                    break;
                case "purple":
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
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
        public static string CustomConsoleWrite(string text, string text_color = "white", bool is_centered = false)
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
                case "orange":
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    break;
                case "yellow":
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case "blue":
                    Console.ForegroundColor = ConsoleColor.Blue;
                    break;
                case "purple":
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
            }

            if (is_centered)
            {
                Console.SetCursorPosition((Console.WindowWidth - text.Length) / 2, Console.CursorTop);
                Console.Write(text);
            }
            else
            {
                Console.Write(text);
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
                Console.Write("Wybrana opcja: ");
                try
                {
                    option = int.Parse(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("Podaj liczbę!");
                    option = 0;
                }

                if(option != 0 )
                {
                    switch (option)
                    {
                        case -1:
                            return;
                        case 1:
                            AddProduct();
                            break;
                        case 2:
                            EditProduct();
                            break;
                        case 3:
                            AddIngredient();
                            break;
                        case 4:
                            EditIngredient();
                            break;
                        default:
                            Console.WriteLine("Nie ma takiej opcji!");
                            break;

                    }
                }
            } while (option != -1);

            return;
        }

        public static void AddProduct()
        {
            int option = 0, step = 0;
            bool success = false;

            Product newProduct = new Product();
            newProduct.Id = GlobalData.Menu.Max(i => i.Id) + 1;

            do
            {
                success = false;
                Console.Clear();
                Functions.CustomConsoleWriteLine("Dodawanie produktu o ID " + newProduct.Id, "", false);
                if (step > 0) Functions.CustomConsoleWriteLine("ID kategorii: " + newProduct.CategoryId, "", false);
                if (step > 1) Functions.CustomConsoleWriteLine("Nazwa: " + newProduct.Name, "", false);
                if (step > 2) Functions.CustomConsoleWriteLine("ID składników: " + newProduct.IngredientsId, "", false);
                if (step > 3) Functions.CustomConsoleWriteLine("Cena: " + newProduct.Price, "", false);

                if(step == 0)
                {
                    Console.WriteLine("\nWybierz kategorię produktu: ");
                    Console.WriteLine("1. Napoje");
                    Console.WriteLine("2. Pizze");
                    Console.WriteLine("-1. Anuluj dodawanie produktu");
                    Console.Write("Wybrana kategoria: ");
                    try
                    {
                        option = int.Parse(Console.ReadLine());
                    }
                    catch
                    {
                        Functions.CustomConsoleWriteLine("Podaj liczbę!", "red", false);
                        option = 0;
                        Console.ReadKey();
                    }

                    if (option == -1)
                    {
                        return;
                    }
                    else if (option != 0)
                    {
                        if (GlobalData.Menu.Any(i => i.CategoryId == option))
                        {
                            newProduct.CategoryId = option;
                            success = true;
                            option = 0;
                        }
                        else
                        {
                            Functions.CustomConsoleWriteLine("Nie ma takiej pozycji!", "red", false);
                            Thread.Sleep(1000);
                        }
                    }
                }

                if(step == 1)
                {
                    string name;
                    Console.Write("\nPodaj nazwę produktu (nazwa nie może się powtarzać!): ");
                    name = Console.ReadLine();

                    if (GlobalData.Menu.Any(i => i.Name.ToLower() == name.ToLower() && i.CategoryId == newProduct.CategoryId))
                    {
                        Functions.CustomConsoleWriteLine("Nazwa już istnieje w tej kategorii!", "red", false);
                        Thread.Sleep(1000);
                    }
                    else
                    {
                        newProduct.Name = name;
                        success = true;
                    }
                }

                if (success) step++;
            } while (step < 5);
        }

        public static void EditProduct()
        {

        }

        public static void AddIngredient()
        {

        }

        public static void EditIngredient()
        {

        }
    }
    public class Pizzeria
    {
        public void Loader()
        {
            Console.Write("Ładowanie... //");
            for(int i=0; i<3; i++)
            {
                Console.Write("\\//\\");
                Thread.Sleep(850);
            }
        }
        public void Greeting()
        {
            Functions.CustomConsoleWriteLine("Witaj w naszej Pizzerii Italiano! ", "green", true);
            Functions.CustomConsoleWriteLine("Zapraszamy do złożenia zamówienia\n", "", true);
            Functions.CustomConsoleWriteLine("Wpisz numer aby otworzyć kategorie menu", "red", true);

        }
        public string CheckCategoryString(string category_name)
        {
            string result = "0";
            category_name = category_name.ToLower();

            foreach (Product single_product in GlobalData.Menu)
            {
                switch (category_name)
                {
                    case "napoje":
                            result = "1";
                        break;
                    case "pizze":
                        result = "2";
                        break;
                    default:
                        result = "0";
                        break;
                }
            }

            return result;
        }
        public void DisplayOrder()
        {
            Console.Clear();
            Functions.CustomConsoleWriteLine("Twoje zamówienie: \n", "green", true);

            int i = 0;
            foreach (Product single_product in GlobalData.Order)
            {
                Console.WriteLine("{0}. {1} {2}{3}", ++i, single_product.Name, single_product.Price, single_product.CurrencySymbol);
            }

            Functions.CustomConsoleWriteLine("\nNaciśnij dowolny przycisk aby wyjść z przeglądu zamówienia");
            Console.ReadKey();
            Console.Clear();
            this.Greeting();
        }
        public int ChooseCategory()
        {
            bool hide_menu = false;
            int selectedCategory = 0;
            string selectedCategory_string = "0";
            do
            {
                Functions.CustomConsoleWriteLine("\n1. Napoje", "", false);
                Functions.CustomConsoleWriteLine("2. Pizze", "", false);

                if(GlobalData.Order.Count > 0)
                {
                    Functions.CustomConsoleWriteLine("\n9. Zobacz moje zamówienie", "", false);
                    Functions.CustomConsoleWriteLine("0. Przejdź do składania zamówienia", "green", false);
                }
                Functions.CustomConsoleWriteLine("-1. Logowanie do panelu admina", "", false);

                Console.Write("\nTwój wybór: ");
                try
                {
                    selectedCategory_string = Console.ReadLine();
                    selectedCategory = int.Parse(selectedCategory_string);
                }
                catch
                {
                    selectedCategory = int.Parse(CheckCategoryString(selectedCategory_string));
                }
                
                if (selectedCategory == -1)
                {
                    Admin.Login();
                }
                else if(selectedCategory_string == "0" && GlobalData.Order.Count > 0)
                {
                    hide_menu = true;
                    this.Szczegoly_Zamowienia();
                }
                else if (selectedCategory_string == "9" && GlobalData.Order.Count > 0)
                {
                    this.DisplayOrder();
                }
                else if (selectedCategory != 0)
                {
                    hide_menu = this.Menu(selectedCategory);
                }
                else if (selectedCategory == 0)
                {
                    Functions.CustomConsoleWriteLine("Nie ma takiej pozycji w menu!", "red", false);
                }
            } while (hide_menu == false);

            return selectedCategory;
        }
        public int CheckMenuPositionId(int providedPosition, List<Product> products)
        {
            var menuPosition = products.Find(product => product.Id == providedPosition);
            return menuPosition == null ? -1 : menuPosition.Id;
        }
        public int CheckMenuPositionString(string providedPosition, List<Product> products)
        {
            var menuPosition = products.Find(product => product.Name.ToLower() == providedPosition.ToLower());
            return menuPosition == null ? -1 : menuPosition.Id;
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
        }
        public Product GiveMenuObjById(int menuPositionId)
        {
            var menuPosition = GlobalData.Menu.Find(i => i.Id == menuPositionId);
            return menuPosition;
        }
        public Pizzeria()
        {
            this.Greeting();
            //this.Szczegoly_Zamowienia();

            int selected_category = this.ChooseCategory();

            string value_string;
            int value = -1;
            bool is_choosing_position = true;

            Console.Write("\nWybierz '0' aby powrócić do kategori" +
                          "\nWybierz '250' aby złożyć zamówienie" +
                          "\nWybierz '300' aby zobaczyć aktualne zamówienie");
            while(is_choosing_position != false)
            {
                var selected_category_products = GlobalData.Menu.Where(i => i.CategoryId == selected_category).ToList();

                Console.Write("\n");
                Console.Write("Twój wybór: ");
                value_string = Console.ReadLine();

                try
                {
                    value = this.CheckMenuPositionId(int.Parse(value_string), selected_category_products);
                }catch
                {
                    value = this.CheckMenuPositionString(value_string, selected_category_products);
                }

                if(value != -1)
                {
                    GlobalData.Order.Add(GiveMenuObjById(value));
                }
                else if(value_string == "0")
                {
                    Console.Clear();
                    this.Greeting();
                    selected_category = this.ChooseCategory();
                }
                else if(value_string == "250")
                {
                    Console.WriteLine("Trwa przechodzenie do szczegółów zamównienia (3s)");
                    Thread.Sleep(3000);
                    this.Szczegoly_Zamowienia();
                }
                else if (value_string == "300")
                {
                    Console.WriteLine("Trwa przechodzenie do okna przeglądania zamównienia (3s)");
                    Thread.Sleep(3000);
                    this.DisplayOrder();
                }
                else
                {
                    Functions.CustomConsoleWriteLine("Nie ma takiej pozycji w menu", "red", false);
                }
            }
        }

        public void Szczegoly_Zamowienia()
        {
            Console.WriteLine("czyszczenie + przejscie do zamowienia' czyszczenie 3s, może jakiś loader?");
            Console.Clear();

            do 
            {
                Functions.CustomConsoleWriteLine("Wprowadź swoje imie", "Green", false);
                GlobalData.KlientImie = Console.ReadLine();
                if (GlobalData.KlientImie == "") Console.WriteLine("Brak danych.\nSpróbuj ponownie.");
            }while(GlobalData.KlientImie=="");
            do
            {
                Functions.CustomConsoleWriteLine("Wprowadź swoje nazwisko", "Green", false);
                GlobalData.KlientNazwisko = Console.ReadLine();
                if (GlobalData.KlientNazwisko == "") Console.WriteLine("Brak danych.\nSpróbuj ponownie.");
            } while (GlobalData.KlientNazwisko == "");
            do
            {
                Functions.CustomConsoleWriteLine("Wprowadź miasto, ulice i numer domu/mieszkania", "Green", false);
                GlobalData.KlientAdres = Console.ReadLine();
                if (GlobalData.KlientAdres == "") Console.WriteLine("Brak danych.\nSpróbuj ponownie.");
            } while (GlobalData.KlientAdres == "");

            Functions.CustomConsoleWriteLine("Uwagi do zamówienia", "Green", false);
            GlobalData.KlientUwagiDoZamowienia = Console.ReadLine();

            Functions.CustomConsoleWriteLine("Dostępne metody płatności:", "Green", false);
            Console.WriteLine("\n1.Karta płatnicza" +
                              "\n2.Płatność gotówką przy odbiorze" +
                              "\n3.Płatność BLIK");
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
                    Console.WriteLine("Podałeś błędne dane (płatność ~280)");
                }
                string amogus;
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
                    case 3:
                        Console.WriteLine("wybrana metoda płatności to BLIK" +
                                        "\nPodaj kod BLIK:");
                        amogus = Convert.ToString(Console.ReadLine());
                        if (amogus.Length == 6) { bledna_platnosc = true; break; }
                        else { Console.WriteLine("Podałeś błędny kod"); break; }
                    default:
                        Console.WriteLine("Wybrana metoda płatności nie istnieje");
                        break;
                }

            } while (bledna_platnosc == false);

            Console.WriteLine("Dane przyjęte. Trwa przekierowanie do Podsumowania (5s)"); 
            Thread.Sleep(5000); 
            this.Podsumowanie();
        }

        public void Podsumowanie()
        {
            Console.Clear();
            Functions.CustomConsoleWrite("Czy twoje dane się zgadzają?\n","pink",true);

            Functions.CustomConsoleWrite("IMIE:", "red", false);
            Console.WriteLine(" " + GlobalData.KlientImie + "\n");
            Functions.CustomConsoleWrite("NAZWISKO:", "orange", false);
            Console.WriteLine(" " + GlobalData.KlientNazwisko + "\n");
            Functions.CustomConsoleWrite("ADRES:", "yellow", false);
            Console.WriteLine(" " + GlobalData.KlientAdres + "\n");
            Functions.CustomConsoleWrite("UWAGI:", "green", false);
            Console.WriteLine(" " + GlobalData.KlientUwagiDoZamowienia + "\n");
            Functions.CustomConsoleWrite("METODA PŁATNOŚCI", "blue", false);
            switch (GlobalData.KlientMetodaPlatnosci)
            {
                case 1:
                    Console.WriteLine(" wybrana metoda płatności to płatność kartą");
                    break;
                case 2:
                    Console.WriteLine(" wybrana metoda płatności to płatność gotówką przy odbiorze");
                    break;
                case 3 :
                    Console.WriteLine(" wybrana metoda płatności to BLIK");
                    break;
                default:
                    Console.WriteLine(" Wybrana metoda nie istnieje mimo że została zaakceptowana");
                    break;
            }
            string morb="";
            do
            {
                Functions.CustomConsoleWriteLine("Czy dane się zgadzają? (Y/N)", "purple", false);
                morb = Console.ReadLine();
                morb.ToLower();
            } while (morb != "y" && morb != "n");
            if (morb == "n") { Console.WriteLine("Następuje przekierowanie do ponownego wpisywania danych (5s)");Thread.Sleep(5000); this.Szczegoly_Zamowienia(); }
            this.ThankYouPage();
        }

        public void ThankYouPage()
        {
            Random rad = new Random();
            int a = rad.Next(45, 65);
            Console.Clear();
            Console.WriteLine("Dziekujemy za złozenie zamowienia." +
                "\nTwoje zamówienie będzie gotowe za około {0}",a);
            Console.WriteLine("\nNaciśnij dowolny przycisk aby zakończyć program");
            Console.ReadKey();
            this.KoniecProgramu();
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
            SoundPlayer player = new SoundPlayer();
            player.SoundLocation = "../../../../src/data/theme.wav";
            player.PlayLooping();

            //Console.Beep(800, 1000);
            string menuPath = "../../../../src/data/menu.json";
            string menuJSON = File.ReadAllText(menuPath);
            string ingredientsPath = "../../../../src/data/ingredients.json";
            string ingredientsJSON = File.ReadAllText(ingredientsPath);
            GlobalData.Menu = JsonSerializer.Deserialize<List<Product>>(menuJSON);
            GlobalData.Ingredients = JsonSerializer.Deserialize<List<Ingredient>>(ingredientsJSON);
            //Console.WriteLine("Załadowano {0} pozycji w menu", GlobalData.Menu.Count);
            //var functions = new Functions();
            var pizzeria = new Pizzeria();
             

            // TODO: DODAĆ UMERY ALBUMÓW DO README
        }
    }
}
