using System;

namespace Pilot_Project
{
    internal class Menu
    {
        private static Menu _menu;
        public static Menu NewMenu()
        {
            if (_menu == null) _menu = new Menu();
            return _menu;
        }
        private static string _spacesForString = "";
        public static string GetMenuItem(string[] menuItems, int cursorPosition)
        {
            int counter = 0;
            while (true)
            {
                Console.CursorVisible = false;
                Console.SetCursorPosition(0, cursorPosition);
                for (int i = 0; i < menuItems.Length; i++)
                {
                    if (counter == i)
                    {
                        Console.BackgroundColor = ConsoleColor.Magenta;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.WriteLine($">{menuItems[i]}");
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else
                        Console.WriteLine($" {menuItems[i]}");
                }
                var userSelection = Console.ReadKey(true);
                if (userSelection.Key == ConsoleKey.UpArrow)
                {
                    counter--;
                    if (counter == -1) { counter = menuItems.Length - 1; }
                }
                if (userSelection.Key == ConsoleKey.DownArrow)
                {
                    counter++;
                    if (counter == menuItems.Length) { counter = 0; }
                }
                if (userSelection.Key == ConsoleKey.Enter) { Console.Clear(); break; }
                if (userSelection.Key == ConsoleKey.Escape)
                {
                    Console.Clear();
                    return "Выход";
                }
            }
            Console.Clear();

            return menuItems[counter];
        }
        public virtual void TextBeforeMenu()
        {
            ShowcaseText();
            Console.Write("╔"); Console.Write(new string('═', 65)); Console.WriteLine("╗");
            Console.WriteLine("║Войдите в приложение или зарегистризуйтесь!                      ║" +
                            "\n║Для перемещиния по меню используйте стрезки вверх и вниз!        ║");
            Console.Write("╚"); Console.Write(new string('═', 65)); Console.WriteLine("╝");
        }

        protected static void ShowcaseText()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("╔══╗ ╔╗╔╗ ╔══╗ ╔╗╔╗ ╔══╗     ╔╗╔╗ ╔══╗ ╔╗╔╗ ╔══╗ ╔═══╗    ╔══╗ ╔╗╔╗" +
                            "\n║╔═╝ ║║║║ ║╔═╝ ║║║║ ╚╗╔╝     ║║║║ ║╔╗║ ║║║║ ║╔═╝ ║╔══╝    ║╔╗║ ║║║║" +
                            "\n║╚═╗ ║║║║ ║╚═╗ ║╚╝║  ║║ ╔══╗ ║╚╝║ ║║║║ ║║║║ ║╚═╗ ║╚══╗    ║╚╝╚╗║╚╝║" +
                            "\n╚═╗║ ║║║║ ╚═╗║ ║╔╗║  ║║ ╚══╝ ║╔╗║ ║║║║ ║║║║ ╚═╗║ ║╔══╝    ║╔═╗║╚═╗║" +
                            "\n╔═╝║ ║╚╝║ ╔═╝║ ║║║║ ╔╝╚╗     ║║║║ ║╚╝║ ║╚╝║ ╔═╝║ ║╚══╗ ╔╗ ║╚═╝║ ╔╝║" +
                            "\n╚══╝ ╚══╝ ╚══╝ ╚╝╚╝ ╚══╝     ╚╝╚╝ ╚══╝ ╚══╝ ╚══╝ ╚═══╝ ╚╝ ╚═══╝ ╚═╝");
            Console.WriteLine(new string('═', 67));
            Console.ResetColor();
        }
        public static void AddFrameAroundText(string value)
        {
            ShowcaseText();
            for (int i = value.Length; i < 65; i++)
            {
                _spacesForString += " ";
            }
            string newString = $"{value}{_spacesForString}";
            Console.Write("╔"); Console.Write(new string('═', 65)); Console.WriteLine("╗");
            Console.WriteLine($"║{newString}║");
            Console.Write("╚"); Console.Write(new string('═', 65)); Console.WriteLine("╝");
            _spacesForString = null;
        }
    }
}
