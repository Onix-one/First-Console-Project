using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.IO;
using System.Linq;
using System.Threading;
using Pilot_Project.Basket;
using Pilot_Project.RegistrationAndLogo;

namespace Pilot_Project.DataBaseClasses
{
    class StreetsDataBase : DataBase
    {
        private static Table<Streets> StreetsTable { get; set; }
        private readonly List<Streets> _streets;
        static StreetsDataBase _streetsDataBase;
        private StreetsDataBase()
        {
            StreetsTable = DataContext.GetTable<Streets>();
            try
            {
                if (DataContext.DatabaseExists() == false)
                {
                    throw new Logger("Коллекция _streets = Null");
                }

                if (DataContext.DatabaseExists())
                {
                    IQueryable<Streets> streets = from u in StreetsTable
                        select u;
                    _streets = streets.ToList();
                }
            }
            catch (Logger exception)
            {
                exception.LogWrite(exception.Error);
            }
        }
        public static StreetsDataBase NewStreetsDataBase()
        {
            if (_streetsDataBase == null) _streetsDataBase = new StreetsDataBase();
            return _streetsDataBase;
        }
        public bool CheckingUserAddressStreetInStreetDatabase()
        {
            User user = User.NewUser();
            return _streets.Any(street => street.MinskStreets.Equals(user.UserAddressStreet, StringComparison.OrdinalIgnoreCase));
        }
        private void AddStreetIntoDatabase(string value)
        {
            Streets streets = new Streets { MinskStreets = value };
            StreetsTable.InsertOnSubmit(streets);
            DataContext.SubmitChanges();
        }
        private void DelStreetIntoDatabase()
        {
            foreach (var street in _streets)
            {
                StreetsTable.DeleteOnSubmit(street);
                DataContext.SubmitChanges();
            }
        }
        public static void Method(string value)
        {
            StreetsDataBase streetsDataBase = NewStreetsDataBase();
            StreamReader textReader = new StreamReader(@"C:\Users\U4iha\Desktop\Pilot_Project\New Text Document.txt", false);
            string textReaderResult = textReader.ReadToEnd();
            textReader.Dispose();
            string[] arrayOfTextResult = textReaderResult.Split(',', '\r', '\n');
            List<string> list = arrayOfTextResult.Where(val => val != "").ToList();
            var uniq = list.Distinct();

            if (value.Equals("Add"))
            {
                foreach (string val in uniq)
                {
                    streetsDataBase.AddStreetIntoDatabase(val);
                }
            }
            if (value.Equals("Del"))
            {
                ThreadStart threadStart = streetsDataBase.DelStreetIntoDatabase;
                Thread thread = new Thread(threadStart);
                thread.Start();
            }
            Console.ReadKey();
        }
    }
}
