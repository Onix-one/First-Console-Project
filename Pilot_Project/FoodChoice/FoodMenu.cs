using System;
using Pilot_Project.Basket;
using Pilot_Project.DataBaseClasses;

namespace Pilot_Project.FoodChoice
{
    internal class FoodMenu : Menu
    {
        private string[] FoodMenuItems { get; } = { "Cуши", "Рамэны", "Лапша", "Салаты", "Донбури" };
        private string SelectedProductName { get; set; }
        private decimal PriceOfSelectedProduct { get; set; }
        public void FoodMenuNavigation()
        {
            while (true)
            {
                Console.Clear();
                ShoppingBasket shoppingBasket = new ShoppingBasket();
                string typeOfSelectedFood = GetTypeOfSelectedFood();
                if (typeOfSelectedFood != "КОРЗИНА")
                {
                    FoodChoice(typeOfSelectedFood);
                }
                else
                {
                    shoppingBasket.GetBasket();
                }
            }
        }
        private string GetTypeOfSelectedFood()
        {
            while (true)
            {
                TextBeforeMenu();
                string menuResult = GetMenuItem(FoodMenuItems.AddBasketItemToFoodMenu("КОРЗИНА"), 11);
                if (menuResult == EnumFoodMenu.Cуши.ToString()) { return "Sushi"; }
                if (menuResult == EnumFoodMenu.Рамэны.ToString()) { return "Ramen"; }
                if (menuResult == EnumFoodMenu.Лапша.ToString()) { return "Noodl"; }
                if (menuResult == EnumFoodMenu.Салаты.ToString()) { return "Salad"; }
                if (menuResult == EnumFoodMenu.Донбури.ToString()) { return "Donburi"; }
                if (menuResult == EnumFoodMenu.КОРЗИНА.ToString()) { return "КОРЗИНА"; }
            }
        }
        private bool FoodChoice(string typeOfSelectedFood)
        {
            FoodDataBase foodDataBase = FoodDataBase.NewFoodDataBase();
            string[,] foodListSelectedType = foodDataBase.GetFoodListOfSelectedType(typeOfSelectedFood);
            while (true)
            {
                TextBeforeMenu();
                int cursorPosition = 11;
                string menuResult = GetMenuItem(foodListSelectedType, cursorPosition);
                if (menuResult == "Выход") return false;
                else
                {
                    AddFoodToBasketOrNot(); continue;
                }
            }
        }
        private bool AddFoodToBasketOrNot()
        {
            AddFrameAroundText($"Добавить \"{SelectedProductName}\" в корзину?");
            string[] yesOrNo = { "Да", "Нет" };
            while (true)
            {
                int cursorPosition = 10;
                string menuResult = GetMenuItem(yesOrNo, cursorPosition);
                if (menuResult == "Выход" | menuResult == "Нет") { return false; }
                if (menuResult == "Да")
                {
                    ShoppingBasket.AddToShoppingBasket(SelectedProductName, PriceOfSelectedProduct);
                    return true;
                }
            }
        }
        public override void TextBeforeMenu()
        {
            ShowcaseText();
            Console.Write("╔"); Console.Write(new string('═', 65)); Console.WriteLine("╗");
            Console.WriteLine("║Сделайте Ваш выбор!                                              ║" +
                            "\n║Для перемещиния по меню используйте стрелки вверх и вниз!        ║");
            Console.Write("╚"); Console.Write(new string('═', 65)); Console.WriteLine("╝");
        }
        private string GetMenuItem(string[,] menuItems, int cursorPosition)
        {
            int counter = 0;
            while (true)
            {
                Console.CursorVisible = false;
                Console.SetCursorPosition(0, cursorPosition);
                for (int i = 0; i < menuItems.GetLength(0); i++)
                {
                    string spaces = $" ";
                    for (int j = menuItems[i, 0].Length; j < 38; j++)
                    {
                        spaces += " ";
                    }
                    if (counter == i)
                    {
                        Console.BackgroundColor = ConsoleColor.Magenta;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.WriteLine($">{menuItems[i, 0]}{spaces}Цена: { menuItems[i, 1]} р. Вес: { menuItems[i, 2]} гр.");
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else
                        Console.WriteLine($" {menuItems[i, 0]}{spaces}Цена: { menuItems[i, 1]} р. Вес: { menuItems[i, 2]} гр.");
                }
                var userSelection = Console.ReadKey(true);
                if (userSelection.Key == ConsoleKey.UpArrow)
                {
                    counter--;
                    if (counter == -1) { counter = menuItems.GetLength(0) - 1; }
                }
                if (userSelection.Key == ConsoleKey.DownArrow)
                {
                    counter++;
                    if (counter == menuItems.GetLength(0)) { counter = 0; }
                }
                if (userSelection.Key == ConsoleKey.Enter) { Console.Clear(); break; }
                if (userSelection.Key == ConsoleKey.Escape)
                { Console.Clear(); return "Выход"; }
            }
            Console.Clear();
            SelectedProductName = menuItems[counter, 0];
            PriceOfSelectedProduct = Convert.ToDecimal(menuItems[counter, 1]);
            return menuItems[counter, 0];
        }
        enum EnumFoodMenu
        {
            Cуши = 0,
            Рамэны = 1,
            Лапша = 2,
            Салаты = 3,
            Донбури = 4,
            КОРЗИНА = 5
        }
    }
}
