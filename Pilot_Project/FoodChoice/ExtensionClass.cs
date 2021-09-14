using System;

namespace Pilot_Project.FoodChoice
{
    static class ExtensionClass
    {
        public static string[] AddBasketItemToFoodMenu(this string[] foodMenuItems, string newItem)
        {
            string[] newFoodMenuItems = foodMenuItems;
            Array.Resize(ref newFoodMenuItems, newFoodMenuItems.Length + 1);
            newFoodMenuItems[newFoodMenuItems.Length - 1] = newItem;
            return newFoodMenuItems;
        }
    }
}
