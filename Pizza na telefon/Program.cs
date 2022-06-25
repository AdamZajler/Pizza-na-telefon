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
        public float Price { get; set; }
        public int[] IngredientsId { get; set; }
        public string CurrencySymbol { get; set; }
    }
    public class Product : IProduct
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public int[] IngredientsId { get; set; }
        public string CurrencySymbol { get; set; }
    }
    interface IIngredient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public string CurrencySymbol { get; set; }

    }
    public class Ingredient : IIngredient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public string CurrencySymbol { get; set; }

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
        public static void AdminGreeting()
        {
            Functions.CustomConsoleWriteLine("====== Panel Administracyjny ======", "red", true);
        }
        public static void Login()
        {
            Panel();
            //bool loginPassed = false;
            //do
            //{
            //    Console.Clear();
            //    Console.Write("Podaj login: ");
            //    string login = Console.ReadLine();
            //    Console.Write("Podaj hasło: ");
            //    var pass = string.Empty;

            //    ConsoleKey key;
            //    do
            //    {
            //        var keyInfo = Console.ReadKey(intercept: true);
            //        key = keyInfo.Key;

            //        if (key == ConsoleKey.Backspace && pass.Length > 0)
            //        {
            //            Console.Write("\b \b");
            //            pass = pass[0..^1];
            //        }
            //        else if (!char.IsControl(keyInfo.KeyChar))
            //        {
            //            Console.Write("*");
            //            pass += keyInfo.KeyChar;
            //        }
            //    } while (key != ConsoleKey.Enter);

            //    if (login == GlobalData.AdminLogin && pass == GlobalData.AdminPass)
            //    {
            //        Functions.CustomConsoleWriteLine("\n\n Poprawne hasło! Przekierowywanie...", "green", true);
            //        Thread.Sleep(1000);
            //        Panel();
            //        return;
            //    }
            //    else
            //    {
            //        Functions.CustomConsoleWriteLine("\n\nLogin i hasło nie są zgodne! Wpisz '0' aby wrócić do menu głównego lub dowolny inny klawisz aby spróbować ponownie!", "red", true);

            //        var keyInfo = Console.ReadKey(intercept: true);
            //        key = keyInfo.Key;

            //        if (key == ConsoleKey.D0)
            //        {
            //            return;
            //        }
            //    }
            //} while (loginPassed == false);
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
                Console.WriteLine("2. Dodaj składnik");
                Console.WriteLine("3. Edytuj składnik");
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
                            AddIngredient();
                            break;
                        case 3:
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

        public static void SaveProducts()
        {
            string menuPath = "../../../../src/data/menu.json";
            string menu = JsonSerializer.Serialize(GlobalData.Menu);
            File.WriteAllText(menuPath, menu);
            Functions.CustomConsoleWriteLine("\nZapisano do pliku!", "green", true);
        }

        public static void AddProduct()
        {
            int option = 0, step = 0;
            bool success = false;
            float price = 0;
            int[] ingredients = new int[0];

            Product newProduct = new Product();
            newProduct.Id = GlobalData.Menu.Max(i => i.Id) + 1;

            do
            {
                success = false;
                Console.Clear();
                Functions.CustomConsoleWriteLine("Dodawanie produktu o ID " + newProduct.Id, "", false);
                if (step > 0) Functions.CustomConsoleWriteLine("ID kategorii: " + newProduct.CategoryId, "", false);
                if (step > 1) Functions.CustomConsoleWriteLine("Nazwa: " + newProduct.Name, "", false);
                if (step > 2) Functions.CustomConsoleWriteLine("ID składników: [" + string.Join(", ", newProduct.IngredientsId) + "]", "", false);
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
                        Thread.Sleep(1000);
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
                    Console.Write("\nPodaj nazwę produktu (nazwa nie może się powtarzać, -1 aby anulować dodawanie): ");
                    name = Console.ReadLine();

                    if (name == "-1")
                    {
                        return;
                    }
                    if (GlobalData.Menu.Any(i => i.Name.ToLower() == name.ToLower() && i.CategoryId == newProduct.CategoryId))
                    {
                        Functions.CustomConsoleWriteLine("Nazwa już istnieje w tej kategorii!", "red", false);
                        Thread.Sleep(1000);
                    }
                    else if (name == "")
                    {
                        Functions.CustomConsoleWriteLine("Nazwa nie może być pusta", "red", false);
                        Thread.Sleep(1000);
                    }
                    else
                    {
                        newProduct.Name = name;
                        if (newProduct.CategoryId == 1)
                        {
                            step = 3;
                            option = 0;
                        }
                        else
                        {
                            success = true;
                        }
                    }
                }

                if(step == 2)
                {
                    Console.WriteLine("\nPodaj ID składniku (0 aby zakończyć dodawanie składników, -1 aby anulować dodawanie produktu)");
                    if (ingredients.Length > 0)
                    {
                        Functions.CustomConsoleWrite("ID dodanych składników:");
                        Console.Write("[{0}]\n", string.Join(", ", ingredients));
                    }
                    foreach(Ingredient Ingredient in GlobalData.Ingredients)
                    {
                        if (!ingredients.Contains(Ingredient.Id))
                        {
                            Console.WriteLine("{0}. {1}", Ingredient.Id, Ingredient.Name);
                        }
                        else
                        {
                            Functions.CustomConsoleWriteLine(Ingredient.Id + ". " + Ingredient.Name, "green", false);
                        }
                    }

                    Console.Write("Dodany składnik: ");

                    try
                    {
                        option = int.Parse(Console.ReadLine());
                    }
                    catch
                    {
                        Functions.CustomConsoleWriteLine("Podaj liczbę!", "red", false);
                        option = 0;
                        Thread.Sleep(1000);
                        continue;
                    }

                    if (option == -1)
                    {
                        return;
                    }
                    else if (option == 0)
                    {
                        success = true;
                        newProduct.IngredientsId = ingredients;
                    }
                    else
                    {
                        if (ingredients.Contains(option))
                        {
                            ingredients = ingredients.Where(i => i != option).ToArray();
                        }
                        else if (GlobalData.Ingredients.Any(i => i.Id == option))
                        {
                            ingredients = ingredients.Append(option).ToArray();
                            option = 0;
                        }
                        else
                        {
                            Functions.CustomConsoleWriteLine("Nie ma takiej pozycji!", "red", false);
                            Thread.Sleep(1000);
                        }
                    }
                }

                if (step == 3)
                {
                    Console.WriteLine("\nPodaj cenę produktu (-1 aby anulować dodawanie produktu)");
                    Console.Write("Cena: ");
                    try
                    {
                        price = float.Parse(Console.ReadLine());
                    }
                    catch
                    {
                        Functions.CustomConsoleWriteLine("Podaj liczbę!", "red", false);
                        price = 0;
                        Thread.Sleep(1000);
                    }

                    if (price == -1)
                    {
                        return;
                    }
                    else if (price >= 0)
                    {
                        newProduct.Price = price;
                        success = true;
                    }
                    else
                    {
                        Functions.CustomConsoleWriteLine("Cena nie może być mniejsza od 0!", "red", false);
                        Thread.Sleep(1000);
                    }
                }

                if (step == 4)
                {
                    string symbol;
                    Console.Write("\nPodaj symbol waluty (-1 aby anulować dodawanie produktu): ");
                    symbol = Console.ReadLine();

                    if (symbol == "-1")
                    {
                        return;
                    }
                    if (symbol == "")
                    {
                        Functions.CustomConsoleWriteLine("Symbol nie może być pusty", "red", false);
                        Thread.Sleep(1000);
                    }
                    else
                    {
                        newProduct.CurrencySymbol = symbol;
                        success = true;
                    }
                }

                if (success == true) step++; ;
            } while (step < 5);

            Console.Clear();
            Functions.CustomConsoleWriteLine("Podsumowanie dodania produktu:", "green", false);
            Functions.CustomConsoleWriteLine("ID produktu: " + newProduct.Id, "", false);
            Functions.CustomConsoleWriteLine("ID kategorii: " + newProduct.CategoryId, "", false);
            Functions.CustomConsoleWriteLine("Nazwa: " + newProduct.Name, "", false);
            Functions.CustomConsoleWriteLine("ID składników: [" + string.Join(", ", newProduct.IngredientsId) + "]", "", false);
            Functions.CustomConsoleWriteLine("Cena: " + newProduct.Price + " " + newProduct.CurrencySymbol, "", false);

            GlobalData.Menu.Add(newProduct);
            SaveProducts();
            Thread.Sleep(3000);
        }
        
        public static void SaveIngredients()
        {
            string ingredientsPath = "../../../../src/data/ingredients.json";
            string ingredients = JsonSerializer.Serialize(GlobalData.Ingredients);
            File.WriteAllText(ingredientsPath, ingredients);
            Functions.CustomConsoleWriteLine("\nDodano składnik!", "green", false);
        }

        //public static void EditProduct()
        //{
        //    int option = 0;
        //    do
        //    {
        //        Console.Clear();
        //        Functions.CustomConsoleWriteLine("Wybierz produkt do edycji (-1 aby powrócić do panelu administatora)", "", false);
        //        foreach (Product product in GlobalData.Menu)
        //        {
        //            Console.WriteLine("{0}. {1}", product.Id, product.Name);
        //        }

        //        Console.Write("Podaj ID produktu do edycji: ");

        //        try
        //        {
        //            option = int.Parse(Console.ReadLine());
        //        }
        //        catch
        //        {
        //            Functions.CustomConsoleWriteLine("Podaj liczbę!", "red", false);
        //            Thread.Sleep(1000);
        //            continue;
        //        }

        //        if(GlobalData.Menu.Any(i => i.Id == option))
        //        {
        //            do
        //            {
        //                Product product = GlobalData.Menu.Find(i => i.Id == option);
        //                Functions.CustomConsoleWriteLine("Edycja produktu " + product.Name + " (ID - " + product.Id + ")", "green", false);
        //                Console.WriteLine("1. Edycja kategorii");
        //                Console.WriteLine("2. Edycja nazwy");
        //                Console.WriteLine("3. Edycja nazwy");
        //                Console.WriteLine("4. Edycja ceny");
        //                Console.WriteLine("5. Edycja symbolu ceny");
        //                Console.WriteLine("-1. Cofnij do wyboru produktu");

        //                try
        //                {
        //                    option = int.Parse(Console.ReadLine());
        //                }
        //                catch
        //                {
        //                    Functions.CustomConsoleWriteLine("Podaj liczbę!", "red", false);
        //                    Thread.Sleep(1000);
        //                    continue;
        //                }

        //                switch(option)
        //                {
        //                    case 1:
        //                        ConsoleKey key;
        //                        do
        //                        {
        //                            int newCategory = 0;
        //                            Console.WriteLine("\nPodaj kategorię produktu: ");
        //                            Console.WriteLine("1. Napoje");
        //                            Console.WriteLine("2. Pizze");
        //                            Console.WriteLine("-1. Anuluj edycję");
        //                            Console.Write("Wybrana kategoria: ");
        //                            try
        //                            {
        //                                option = int.Parse(Console.ReadLine());
        //                            }
        //                            catch
        //                            {
        //                                Functions.CustomConsoleWriteLine("Podaj liczbę!", "red", false);
        //                                option = 0;
        //                                Thread.Sleep(1000);
        //                            }

        //                            if (option != 0)
        //                            {
        //                                if (GlobalData.Menu.Any(i => i.CategoryId == option))
        //                                {
        //                                    newCategory = option;
        //                                    option = 0;
        //                                }
        //                                else
        //                                {
        //                                    Functions.CustomConsoleWriteLine("Nie ma takiej kategorii!", "red", false);
        //                                    Thread.Sleep(1000);
        //                                }
        //                            }

        //                            if (option != -1)
        //                            {
        //                                Console.WriteLine("Czy chcesz zmienić kategorię na {0}? (Y/N)", newCategory);
        //                                var keyInfo = Console.ReadKey(intercept: true);
        //                                key = keyInfo.Key;

        //                                if (key == ConsoleKey.Y)
        //                                {
        //                                    Cons
        //                                }
        //                            }
        //                            else
        //                            {
        //                                key = ConsoleKey.Y;
        //                            }
        //                        } while (key != ConsoleKey.Y && option != -1);
                                
        //                        break;
        //                }

        //            } while (option != -1);
        //            option = 0;
        //            continue;
        //        }
        //        else
        //        {
        //            Functions.CustomConsoleWriteLine("Nie znaleziono takiej pozycji w menu!", "red", false);
        //            Thread.Sleep(1000);
        //            continue;
        //        }
        //    } while (option != -1);
        //}

        public static void AddIngredient()
        {
            bool next_ingredient = true;
            do
            {
                Console.Clear();
                AdminGreeting();
                Functions.CustomConsoleWriteLine("====== Dodawanie składnika ======", "red", true);

                Ingredient newIngredient = new Ingredient();
                newIngredient.Id = GlobalData.Ingredients.Max(i => i.Id) + 1;
                Functions.CustomConsoleWriteLine("Dodawanie dodatku o ID " + newIngredient.Id, "", false);

                newIngredient.Name = "";
                newIngredient.Price = 0;
                newIngredient.CurrencySymbol = "zł";

                bool ingredient_correct = false;
                do
                {
                    do
                    {
                        Functions.CustomConsoleWrite("\nPodaj nazwę dodatku: ");
                        newIngredient.Name = Console.ReadLine();

                        if (newIngredient.Name == "")
                        {
                            Functions.CustomConsoleWriteLine("Nie możesz wpisać pustej nazwy skladnika!", "red", false);
                        }
                    } while (newIngredient.Name == "");

                    do
                    {
                        Functions.CustomConsoleWrite("\nPodaj cene dodatku: ");
                        try
                        {
                            newIngredient.Price = float.Parse(Console.ReadLine());
                        }
                        catch
                        {
                            Functions.CustomConsoleWriteLine("Podana cena nie jest liczbą!", "red", false);
                        }

                        if (newIngredient.Price < 0)
                        {
                            Functions.CustomConsoleWriteLine("Nie możesz ujemna!", "red", false);
                        }
                    } while (newIngredient.Price < 0);

                    do
                    {
                        Functions.CustomConsoleWrite("\nPodaj symbol ceny dodatku: ");
                        newIngredient.CurrencySymbol = Console.ReadLine();

                        if (newIngredient.CurrencySymbol == "")
                        {
                            Functions.CustomConsoleWriteLine("Nie możesz wpisać pustego symbolu ceny skladnika!", "red", false);
                        }
                    } while (newIngredient.CurrencySymbol == "");

                    Functions.CustomConsoleWriteLine("\nDane nowego dodatku: ");
                    Console.WriteLine("Id: {0}", newIngredient.Id);
                    Console.WriteLine("Nazwa: {0}", newIngredient.Name);
                    Console.WriteLine("Cena: {0}", newIngredient.Price);
                    Console.WriteLine("Symbol ceny: {0}", newIngredient.CurrencySymbol);

                    Functions.CustomConsoleWrite("\nCzy podane dane są prawidłowe? (Y/N):  ", "", false);
                    string ing_correct = Console.ReadLine().ToLower();
                    ingredient_correct = ing_correct == "y" ? true : false;
                } while (ingredient_correct == false);

                GlobalData.Ingredients.Add(newIngredient);
                SaveIngredients();

                Functions.CustomConsoleWrite("\nCzy chcesz dodać kolejny składnik? (Y/N):  ");
                string next_ing = Console.ReadLine().ToLower();
                next_ingredient = next_ing == "y" ? true : false;

            } while (next_ingredient == true);
            

            Functions.CustomConsoleWriteLine("\nNaciśnij dowolny przycisk aby wyjść z dodawania składniku.");
            Console.ReadKey();
        }
        public static void SaveEditedIngredient(int Id, Ingredient newIngredient)
        {
            GlobalData.Ingredients.RemoveAt(GlobalData.Ingredients.IndexOf(GlobalData.Ingredients.Find(i => i.Id == Id)));
            GlobalData.Ingredients.Add(newIngredient);

            string ingredientsPath = "../../../../src/data/ingredients.json";
            string ingredients = JsonSerializer.Serialize(GlobalData.Ingredients);
            File.WriteAllText(ingredientsPath, ingredients);
            Functions.CustomConsoleWriteLine("\nZapisano edytowany składnik!", "green", false);
        }
        public static void EditIngredient()
        {
            Console.Clear();
            AdminGreeting();
            Functions.CustomConsoleWriteLine("====== Edycja składnika ======\n", "red", true);

            foreach(Ingredient ingredient in GlobalData.Ingredients)
            {
                Console.WriteLine("{0}. {1} {2}{3}", ingredient.Id, ingredient.Name, ingredient.Price, ingredient.CurrencySymbol);
            }

            bool ingredientExists = false;
            do
            {
                bool isIngredientCorrect = false;
                int ingreditentIdToCustomize = 0;
                do
                {
                    Functions.CustomConsoleWrite("\nJaki chcesz edytować składnik ('0' wyjście): ");
                    try
                    {
                        ingreditentIdToCustomize = int.Parse(Console.ReadLine());
                    }
                    catch
                    {
                        Functions.CustomConsoleWrite("\nNie możesz podać litery!", "red", false);
                        ingreditentIdToCustomize = -1;
                    }

                    if(ingreditentIdToCustomize != -1)
                    {
                        isIngredientCorrect = true;
                    }

                } while (isIngredientCorrect == false);

                if (ingreditentIdToCustomize == 0)
                {
                    ingredientExists = true;
                    return;
                }

                if (GlobalData.Ingredients.Any(i => i.Id == ingreditentIdToCustomize))
                {
                    Ingredient ingredientToCustomize = GlobalData.Ingredients.Find(i => i.Id == ingreditentIdToCustomize);

                    Ingredient newIngredient = new Ingredient();
                    newIngredient.Id = ingredientToCustomize.Id;
                    newIngredient.Name = ingredientToCustomize.Name;
                    newIngredient.Price = ingredientToCustomize.Price;
                    newIngredient.CurrencySymbol = ingredientToCustomize.CurrencySymbol;

                    ingredientExists = true;

                    Functions.CustomConsoleWrite("\nProdukt do edycji: ", "green", false);
                    Console.Write("{0}. {1} {2}{3}", newIngredient.Id, newIngredient.Name, newIngredient.Price, newIngredient.CurrencySymbol);

                    bool isStillEditing = false;
                    do
                    {
                        Console.WriteLine("\n\n1. Edycja nazwy");
                        Console.WriteLine("2. Edycja ceny");
                        Console.WriteLine("3. Edycja symbolu ceny");

                        int positionToEdit = 0;
                        do
                        {
                            Console.Write("\nKtórą pozycję chcesz edytować?: ");
                            try
                            {
                                positionToEdit = int.Parse(Console.ReadLine());
                            }
                            catch
                            {
                                Thread.Sleep(1);
                            }

                            if (positionToEdit != 1 && positionToEdit != 2 && positionToEdit != 3)
                            {
                                Functions.CustomConsoleWriteLine("Nie ma takiej opcji!", "red", false);
                                Thread.Sleep(1000);
                            }
                        } while (positionToEdit != 1 && positionToEdit != 2 && positionToEdit != 3);

                        Console.Clear();
                        AdminGreeting();

                        switch (positionToEdit)
                        {
                            case 1:
                                Functions.CustomConsoleWriteLine("====== Edycja nazwy składnika ======\n", "red", true);

                                Functions.CustomConsoleWriteLine("Aktualna nazwa: " + ingredientToCustomize.Name, "green", false);

                                bool isNewNameCorrect = false;
                                string newIngredientName = "";
                                do
                                {
                                    do
                                    {
                                        Functions.CustomConsoleWrite("\nNowa nazwa: ", "", false);
                                        newIngredientName = Console.ReadLine();

                                        if (newIngredientName == "")
                                        {
                                            Functions.CustomConsoleWriteLine("Nie mmożesz podać pustej nazwy!", "red", false);
                                            Thread.Sleep(1000);
                                        }
                                    } while (newIngredientName == "");

                                    Console.Write("\nCzy chcesz jeszcze raz edytować nazwę? (Y/N): ");
                                    string nameCorrect = Console.ReadLine().ToLower();
                                    isNewNameCorrect = nameCorrect == "y" ? false : true;
                                } while (isNewNameCorrect == false);

                                newIngredient.Name = newIngredientName;
                                SaveEditedIngredient(newIngredient.Id, newIngredient);
                                break;

                            case 2:
                                Functions.CustomConsoleWriteLine("====== Edycja ceny składnika ======\n", "red", true);

                                Functions.CustomConsoleWriteLine("Aktualna cena: " + ingredientToCustomize.Price, "green", false);

                                bool isNewPriceCorrect = false;
                                float newIngredientPrice = 0;
                                do
                                {
                                    do
                                    {
                                        Functions.CustomConsoleWrite("\nNowa cena: ", "", false);
                                        try
                                        {
                                            newIngredientPrice = float.Parse(Console.ReadLine());
                                        }
                                        catch
                                        {
                                            Functions.CustomConsoleWriteLine("Podana cena nie jest liczbą!", "red", false);
                                            newIngredientPrice = -1;
                                        }

                                        if (newIngredientPrice < 0)
                                        {
                                            Functions.CustomConsoleWriteLine("Cena nie może być ujemna!", "red", false);
                                            newIngredientPrice = -1;
                                            Thread.Sleep(1000);
                                        }
                                    } while (newIngredientPrice < 0);

                                    Console.Write("\nCzy chcesz jeszcze raz edytować cenę? (Y/N): ");
                                    string priceCorrect = Console.ReadLine().ToLower();
                                    isNewPriceCorrect = priceCorrect == "y" ? false : true;
                                } while (isNewPriceCorrect == false);

                                newIngredient.Price = newIngredientPrice;
                                SaveEditedIngredient(newIngredient.Id, newIngredient);
                                break;
                            case 3:
                                Functions.CustomConsoleWriteLine("====== Edycja symbolu ceny składnika ======\n", "red", true);

                                Functions.CustomConsoleWriteLine("Aktualna cena: " + ingredientToCustomize.CurrencySymbol, "green", false);

                                bool isNewPriceCurrencySymbolCorrect = false;
                                string newIngredientPriceCurrencySymbol = "zł";
                                do
                                {
                                    do
                                    {
                                        Functions.CustomConsoleWrite("\nNowy symbol ceny: ", "", false);
                                        newIngredientPriceCurrencySymbol = Console.ReadLine();

                                        if (newIngredientPriceCurrencySymbol == "")
                                        {
                                            Functions.CustomConsoleWriteLine("Symbol ceny nie może być pusty!", "red", false);
                                            newIngredientPriceCurrencySymbol = "";
                                            Thread.Sleep(1000);
                                        }
                                    } while (newIngredientPriceCurrencySymbol == "");

                                    Console.Write("\nCzy chcesz jeszcze raz edytować symbol ceny? (Y/N): ");
                                    string priceSymbolCorrect = Console.ReadLine().ToLower();
                                    isNewPriceCurrencySymbolCorrect = priceSymbolCorrect == "y" ? false : true;
                                } while (isNewPriceCurrencySymbolCorrect == false);

                                newIngredient.CurrencySymbol = newIngredientPriceCurrencySymbol;
                                SaveEditedIngredient(newIngredient.Id, newIngredient);
                                break;
                            default:
                                Console.WriteLine("elo errollel");
                                break;
                        }

                        Console.Write("\nCzy chcesz dalej edytować składnik? (Y/N)");
                        string isStillEdit = Console.ReadLine().ToLower();
                        isStillEditing = isStillEdit == "y" ? false : true;

                        if(isStillEditing == false)
                        {
                            Console.Clear();
                            AdminGreeting();
                            Functions.CustomConsoleWriteLine("====== Edycja nazwy składnika ======\n", "red", true);
                            Functions.CustomConsoleWrite("\nProdukt do edycji: ", "green", false);
                            Console.Write("{0}. {1} {2}{3}", newIngredient.Id, newIngredient.Name, newIngredient.Price, newIngredient.CurrencySymbol);
                        }
                    } while (isStillEditing == false);
                }
                else
                {
                    Functions.CustomConsoleWriteLine("\nSkładnik o podanym Id nie istnieje", "red", false);
                    Thread.Sleep(1500);
                }
            } while (ingredientExists == false);

            Functions.CustomConsoleWriteLine("\nNaciśnij dowolny przycisk aby wyjść z edycji składnika.");
            Console.ReadKey();
        }
    }
    public class Pizzeria
    {
        public static void PlayThemeSong()
        {
            Random rnd = new Random();
            int song = rnd.Next(1, 3);

            SoundPlayer player = new SoundPlayer();
            player.SoundLocation = "../../../../src/data/theme" + song + ".wav";
            player.PlayLooping();
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
                    //Admin.Panel();
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

            int selected_category = this.ChooseCategory();

            string value_string;
            int value = -1;
            bool is_choosing_position = true;

            Console.Write("\nWybierz pozycje ('0' powrót do kategori)");
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
                else
                {
                    Functions.CustomConsoleWriteLine("Nie ma takiej pozycji w menu", "red", false);
                }
            }
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


            Console.WriteLine("Naciśnij dowolny klawisz aby przejśc do podsumowania");
            Console.ReadKey();
            this.Podsumowanie();
        }

        public void Podsumowanie()
        {
            Console.WriteLine("czyszczenie 3s, może jakiś loader?");

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
            Console.WriteLine(" " + GlobalData.KlientMetodaPlatnosci + "\n");

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
            Pizzeria.PlayThemeSong();
            string menuPath = "../../../../src/data/menu.json";
            string menuJSON = File.ReadAllText(menuPath);
            string ingredientsPath = "../../../../src/data/ingredients.json";
            string ingredientsJSON = File.ReadAllText(ingredientsPath);
            GlobalData.Menu = JsonSerializer.Deserialize<List<Product>>(menuJSON);
            GlobalData.Ingredients = JsonSerializer.Deserialize<List<Ingredient>>(ingredientsJSON);
            var pizzeria = new Pizzeria();
        }
    }
}
