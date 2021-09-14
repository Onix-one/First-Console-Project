using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pilot_Project.DataBaseClasses;
using Pilot_Project.FoodChoice;
using Pilot_Project.RegistrationAndLogo;


namespace Pilot_Project.Basket
{
    class ShoppingBasket : FoodMenu
    {
        private static ArrayList _productList = new ArrayList();
        private static List<decimal> _costList = new List<decimal>();
        private string _orderListForUser;
        private string _orderListForManager;
        public static event EventHandler<СlientMadeOrderEventArgs> СlientMadeOrder;
        private void OnСlientMadeOrder(СlientMadeOrderEventArgs clientMadeOrder)
        {
            СlientMadeOrder?.Invoke(this, clientMadeOrder);
        }
        public bool GetBasket()
        {
            _orderListForUser = GetOrderListForNotification().Item1;
            _orderListForManager = GetOrderListForNotification().Item2;
            if (_productList.Count == 0)
            {
                AddFrameAroundText(" Ваша корзина пуста!");
                Console.ReadKey();
                return false;
            }
            else
            {
                while (true)
                {
                    ShowcaseText();
                    int cursorPosition = _productList.Count + 11;
                    Console.WriteLine($"Добавленые товары:\n{_orderListForUser}\n\nХотите оформить Ваш заказ?");
                    while (true)
                    {
                        string[] yesOrNo = { "Оформить заказ", "Очистить корзину" };
                        string menuResult = GetMenuItem(yesOrNo, cursorPosition);
                        if (menuResult == "Оформить заказ")
                        {
                            if (GetUserContactDetails())
                            {
                                Notification notification = new Notification();
                                notification.EventSubscription();
                                OnСlientMadeOrder(new СlientMadeOrderEventArgs(_orderListForUser, _orderListForManager));
                                _productList = new ArrayList(); _costList = new List<decimal>(); return true;
                            }
                        }
                        if (menuResult == "Очистить корзину")
                        { _productList = new ArrayList(); _costList = new List<decimal>(); return true; }
                        if (menuResult == "Выход")
                        { Console.Clear(); return false; }
                        else { break; }
                    }
                }
            }
        }
        (string, string) GetOrderListForNotification()
        {
            int counter = 1;
            string orderList = string.Empty;
            foreach (string product in _productList)
            {
                if (orderList == string.Empty) { orderList = $"{counter}) {product}. "; }
                else { orderList += $"\n{counter}) {product}. "; }
                counter++;
            }
            string orderListForManager = orderList;
            decimal sumTotal = _costList.Sum();
            orderList += $"\nИтого к оплате: {sumTotal} р.";
            string orderListForUser = orderList;
            return (orderListForUser, orderListForManager);
        }
        private bool GetUserContactDetails()
        {
            StreetsDataBase streetsDataBase = StreetsDataBase.NewStreetsDataBase();
            ConsoleKeyInfo userSelection;
            User user = User.NewUser();
            while (true)
            {
                AddFrameAroundText("Введите Ваш контактный номер телефона: +375");
                Console.SetCursorPosition(44, 8); user.UserPhoneNumber = User.InputWithLimitedStringLength("userPhoneNumber");
                if (user.ValidationTrueFalseCkeck) break;
                Console.WriteLine("Для выхода нажмите Escape. Для повторной попытки нажмите Enter!");
                while (true)
                {
                    userSelection = Console.ReadKey(true);
                    if (userSelection.Key == ConsoleKey.Escape) return false;
                    if (userSelection.Key == ConsoleKey.Enter) break;
                }
            }
            string[] stringArray = { "название улицы", "№ дома", "№ квартиры" };
            for (int i = 0; i < stringArray.Length; i++)
            {
                while (true)
                {
                    AddFrameAroundText($"Адрес доставки ({stringArray[i]}):");
                    if (i == 0)
                    {
                        Console.SetCursorPosition(34, 8); user.UserAddressStreet = User.InputWithLimitedStringLength("userAddressStreet");
                        if (streetsDataBase.CheckingUserAddressStreetInStreetDatabase() == false)
                        {
                            if (user.ValidationTrueFalseCkeck) { Console.WriteLine("\nТакой улицы не существует!"); }
                            Console.ReadKey();
                            continue;
                        }
                    }
                    if (i == 1) { Console.SetCursorPosition(26, 8); user.UserAddressHouseNumber = User.InputWithLimitedStringLength("userAddressHouseNumber"); }
                    if (i == 2) { Console.SetCursorPosition(30, 8); user.UserAddressApartmentNumber = User.InputWithLimitedStringLength("userAddressApartmentNumber"); }
                    if (user.ValidationTrueFalseCkeck) break;
                    Console.WriteLine("Для выхода нажмите Escape. Для повторной попытки нажмите Enter!");
                    while (true)
                    {
                        userSelection = Console.ReadKey(true);
                        if (userSelection.Key == ConsoleKey.Escape) return false;
                        if (userSelection.Key == ConsoleKey.Enter) break;
                    }
                }
            }
            return true;
        }
        internal static void AddToShoppingBasket(string selectedProductName, decimal priceOfSelectedProduct)
        {
            _productList.Add(selectedProductName);
            _costList.Add(priceOfSelectedProduct);
            AddFrameAroundText("Заказ добавлен в корзину!");
            Console.ReadKey();
        }

    }
}
