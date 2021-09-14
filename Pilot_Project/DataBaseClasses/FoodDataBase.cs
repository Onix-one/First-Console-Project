using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using Pilot_Project.FoodChoice;

namespace Pilot_Project.DataBaseClasses
{
    internal class FoodDataBase : DataBase
    {
        private static Table<Food> FoodTable { get; set; }
        readonly List<Food> _foods;
        private static FoodDataBase _foodDataBase;
        private FoodDataBase()
        {
            FoodTable = DataContext.GetTable<Food>();
            try
            {
                if (DataContext.DatabaseExists() == false)
                {
                    throw new Logger("Коллекция _foods = Null");
                }
                if (DataContext.DatabaseExists())
                {
                    IEnumerable<Food> foods = from u in FoodTable
                        select u;
                    _foods = foods.ToList();
                }
            }
            catch (Logger exception)
            {
                exception.LogWrite(exception.Error);
            }
        }
        public static FoodDataBase NewFoodDataBase()
        {
            if (_foodDataBase == null) _foodDataBase = new FoodDataBase();
            return _foodDataBase;
        }
        public string[,] GetFoodListOfSelectedType(string value)
        {
            int counter = _foods.Count(food => food.FoodType == value);
            string[,] foodListOfSelectedTypes = new string[counter, 3];
            counter = 0;
            foreach (var food in _foods.Where(food => food.FoodType == value))
            {
                foodListOfSelectedTypes[counter, 0] = food.Name;
                foodListOfSelectedTypes[counter, 1] = food.Price;
                foodListOfSelectedTypes[counter, 2] = food.Weight;
                counter++;
            }
            return foodListOfSelectedTypes;
        }
    }
}