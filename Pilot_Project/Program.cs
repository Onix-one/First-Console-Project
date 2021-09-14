using System;
using System.IO;
using Pilot_Project.DataBaseClasses;
using Pilot_Project.FoodChoice;
using Pilot_Project.RegistrationAndLogo;


namespace Pilot_Project
{
    static class Program
    {
        static void Main()
        {

            DataBase dataBase = new DataBase();
            dataBase.Initialization();

            RegistrationAndLogin registrationAndLogin = new RegistrationAndLogin();
            registrationAndLogin.Entry();

            FoodMenu foodMenu = new FoodMenu();
            foodMenu.FoodMenuNavigation();
        }
       
    }
}
