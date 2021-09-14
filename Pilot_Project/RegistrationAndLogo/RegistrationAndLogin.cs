using System;
using System.Threading.Tasks;
using Pilot_Project.DataBaseClasses;

namespace Pilot_Project.RegistrationAndLogo
{
    internal class RegistrationAndLogin
    {
        private User User { get; }
        private UsersDataBase UsersDataBase { get; }
        public RegistrationAndLogin()
        {
            User = User.NewUser();
            UsersDataBase = UsersDataBase.NewUsersDataBase();
        }
        public void Entry()
        {
            while (true)
            {
                if (UserChoiceRegistrationOrLogin() == "Регистрация")
                {
                    if (UserChoiceRegistration()) { break; }
                }
                else  //UserChoiceRegistrationOrLogin() == "Вход"
                {
                    if (UserChoiceLogo()) { break; }
                }
            }
        }
        private static string UserChoiceRegistrationOrLogin()
        {
            Menu menu = Menu.NewMenu();
            string[] items = { "Регистрация", "Вход" };
            int cursorPosition = 11;
            while (true)
            {
                Console.Clear();
                menu.TextBeforeMenu();
                string menuResult = Menu.GetMenuItem(items, cursorPosition);
                if (menuResult == "Регистрация") { return "Регистрация"; }
                if (menuResult == "Вход") { return "Вход"; }
            }
        }
        private bool UserChoiceRegistration()
        {
            IUsersDataBase creatUser = UsersDataBase;
            ConsoleKeyInfo userSelection;
            while (true)
            {
                Menu.AddFrameAroundText(" Введите имя: ");
                Console.SetCursorPosition(15, 8);
                User.Name = User.InputWithLimitedStringLength(1);
                if (User.ValidationTrueFalseCkeck)
                {
                    Menu.AddFrameAroundText($" Привет {User.Name}!"); Console.ReadKey(); break;
                }
                Console.WriteLine("Для выхода нажмите Escape. Для повторной попытки нажмите Enter!");
                while (true)
                {
                    userSelection = Console.ReadKey(true);
                    if (userSelection.Key == ConsoleKey.Escape) { return false; }
                    if (userSelection.Key == ConsoleKey.Enter) { break; }
                }
            }
            while (true)
            {
                Menu.AddFrameAroundText(" Введите логин(mail):  ");
                Console.SetCursorPosition(23, 8);
                User.Mail = User.InputWithLimitedStringLength(2);

                if (creatUser.CheckingEmailInUserDatabase())
                {
                    Console.WriteLine("\nПользователь с таким логином уже есть!");
                    Console.WriteLine("Для выхода нажмите Escape. Для повторной попытки нажмите Enter!");
                    while (true)
                    {
                        userSelection = Console.ReadKey(true);
                        if (userSelection.Key == ConsoleKey.Escape) { return false; }
                        if (userSelection.Key == ConsoleKey.Enter) { break; }
                    }
                    continue;
                }
                if (User.ValidationTrueFalseCkeck)
                { Menu.AddFrameAroundText($" Ваш логин(mail): {User.Mail}"); Console.ReadKey(); break; }
                else
                {
                    Console.WriteLine("Для выхода нажмите Escape. Для повторной попытки нажмите Enter!");
                    while (true)
                    {
                        userSelection = Console.ReadKey(true);
                        if (userSelection.Key == ConsoleKey.Escape) { return false; }
                        if (userSelection.Key == ConsoleKey.Enter) { break; }
                    }
                }
            }
            while (true)
            {
                Menu.AddFrameAroundText(" Введите пароль: ");
                Console.SetCursorPosition(17, 8);
                User.Pass = User.InputWithLimitedStringLength(3);
                if (User.ValidationTrueFalseCkeck)
                { Menu.AddFrameAroundText($" Ваш пароль: {User.Pass}"); Console.ReadKey(); break; }
                Console.WriteLine("Для выхода нажмите Escape. Для повторной попытки нажмите Enter!");
                while (true)
                {
                    userSelection = Console.ReadKey(true);
                    if (userSelection.Key == ConsoleKey.Escape) { return false; }
                    if (userSelection.Key == ConsoleKey.Enter) { break; }
                }
            }

            Task task = UsersDataBase.AddUserIntoDatabaseAsync();
            task.ContinueWith(t => Menu.AddFrameAroundText(" Спасибо за регистрацию!"));
            Console.ReadKey();
            return true;
        }
        private bool UserChoiceLogo()
        {
            Console.WriteLine(UsersDataBase.GetHashCode());
            IUsersDataBase userLogin = UsersDataBase;
            Console.WriteLine(userLogin.GetHashCode());
            ConsoleKeyInfo userSelection;
            while (true)
            {
                Menu.AddFrameAroundText("Введите логин(emale): ");
                Console.SetCursorPosition(23, 8);
                User.Mail = User.InputWithLimitedStringLength(2);
                if (User.ValidationTrueFalseCkeck)
                {
                    if (userLogin.CheckingEmailInUserDatabase()) { break; }
                    else { Console.WriteLine("\nПользователя с таким лого не существует!"); }
                }
                Console.WriteLine("Для выхода нажмите Escape. Для повторной попытки нажмите Enter!");
                while (true)
                {
                    userSelection = Console.ReadKey(true);
                    if (userSelection.Key == ConsoleKey.Escape) { return false; }
                    if (userSelection.Key == ConsoleKey.Enter) { break; }
                }

            }
            while (true)
            {
                Menu.AddFrameAroundText("Введите пароль: ");
                Console.SetCursorPosition(17, 8);
                User.Pass = User.InputWithLimitedStringLength(3);
                if (userLogin.CheckingPasswordInUserDatabase())
                {
                    break;
                }
                if (User.ValidationTrueFalseCkeck) { Console.WriteLine("\nПароль введен неверно!"); }
                Console.WriteLine("Для выхода нажмите Escape. Для повторной попытки нажмите Enter!");
                while (true)
                {
                    userSelection = Console.ReadKey(true);
                    if (userSelection.Key == ConsoleKey.Escape) { return false; }
                    if (userSelection.Key == ConsoleKey.Enter) { break; }
                }
            }
            Menu.AddFrameAroundText("Вы вошли в программу!");
            Console.ReadKey();
            return true;
        }
    }
}
