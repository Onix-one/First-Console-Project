using System;
using System.Data.Linq;
using System.IO;

namespace Pilot_Project.DataBaseClasses
{
    class DataBase
    {
        static DataContext _dataContext;
        protected DataContext DataContext => _dataContext;
        public DataBase()
        {
            string path = GetConnectionString();
             string connectionString = $"Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=" +
                                            $"\"{path}Pilot_Project\\SushiHouseDB.mdf\";" +
                                            "Integrated Security=True";
            _dataContext = new DataContext(connectionString);
        }
        public void Initialization()
        {
            try
            {
                if (DataContext.DatabaseExists() == false)
                {
                    throw new Logger("Проект не подключился к базе данных");
                }

                if (DataContext.DatabaseExists())
                {
                    FoodDataBase foodDataBase = FoodDataBase.NewFoodDataBase();
                    StreetsDataBase streetsDataBase = StreetsDataBase.NewStreetsDataBase();
                }
            }
            catch (Logger exception)
            {
                exception.LogWrite(exception.Error);
            }
        }

        static string GetConnectionString()
        {
            var directory = new DirectoryInfo(@".");
            string myDirectory = directory.FullName;
            int vav3 = 24;
            int val1 = myDirectory.Length;
            int val2 = val1 - vav3;
            string myNewDirectory = myDirectory.Remove(val2, vav3);
            var newLine = myNewDirectory.Split('\\');
            string newnewString = "";
            foreach (var VARIABLE3 in newLine)
            {
                newnewString += VARIABLE3 + "\\";
            }
            return newnewString;
        }
    }
}
