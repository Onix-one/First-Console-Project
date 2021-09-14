using System;
using System.Collections;
using System.Data.Linq.Mapping;
using System.Reflection;
using System.Text;

namespace Pilot_Project.RegistrationAndLogo
{
    [Table(Name = "Users")]
    class User
    {
        private string _userName;
        private string _userMail;
        private string _userPass;
        [UserDataValidation(FixedPhoneNumberLength = 9, RunLetterCheck = true)]
        private string _userPhoneNumber;
        private string _userAddressStreet;
        private string _userAddressHouseNumber;
        private string _userAddressApartmentNumber;
        private static User _user;
        public bool ValidationTrueFalseCkeck { get; private set; }
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id { get; set; }
        [Column(Name = "UserName")]
        public string Name
        {
            get => _userName;
            set
            {
                ValidationTrueFalseCkeck = NameValidation(value);
                if (ValidationTrueFalseCkeck) _userName = FirstCharInNameToUpper(value);
                else
                {
                    Logger logger = new Logger("Имя введено некорректно!");
                    logger.LogWrite(logger.Info);
                    _userName = null; Console.WriteLine("\nИмя введено некорректно!");
                };
            }
        }
        [Column(Name = "UserMail")]
        public string Mail
        {
            get => _userMail;
            set
            {
                ValidationTrueFalseCkeck = MailValidation(value);
                if (ValidationTrueFalseCkeck) _userMail = value;
                else
                {
                    Logger logger = new Logger("Логин введен некорректно!");
                    logger.LogWrite(logger.Info);
                    _userMail = null; Console.WriteLine("\nЛогин введен некорректно!");
                }
            }
        }
        [Column(Name = "UserPass")]
        public string Pass
        {
            get => _userPass;
            set
            {
                ValidationTrueFalseCkeck = PassValidation(value);
                if (ValidationTrueFalseCkeck) _userPass = value;
                else
                {
                    Logger logger = new Logger("Пароль введен некорректно!");
                    logger.LogWrite(logger.Info);
                    _userPass = null; Console.WriteLine("\nПароль введен некорректно!\nПароль должен иметь длинну от 3 до 30 символов.");
                }
            }
        }
        public string UserPhoneNumber
        {
            get => _userPhoneNumber;
            set
            {
                _userPhoneNumber = value;
                ValidationTrueFalseCkeck = PhoneNumberValidation();
                if (ValidationTrueFalseCkeck == false)
                {
                    Logger logger = new Logger("Номер телефона введен некорректно!");
                    logger.LogWrite(logger.Info);
                    Console.WriteLine("\nНомер телефона введен некорректно!");
                }
            }
        }
        public string UserAddressStreet
        {
            get => _userAddressStreet;
            set
            {
                ValidationTrueFalseCkeck = AddressStreetValidation(value);
                if (ValidationTrueFalseCkeck) _userAddressStreet = value;
                else
                {
                    Logger logger = new Logger("Название улицы введено некорректно!");
                    logger.LogWrite(logger.Info);
                    Console.WriteLine("\nНазвание улицы введено некорректно!");
                }
            }
        }
        public string UserAddressHouseNumber
        {

            get => _userAddressHouseNumber;
            set
            {
                ValidationTrueFalseCkeck = AddressHouseNumberValidation(value);
                if (ValidationTrueFalseCkeck) _userAddressHouseNumber = value;
                else
                {
                    Logger logger = new Logger("Номер дома введен некорректно!");
                    logger.LogWrite(logger.Info);
                    Console.WriteLine("\nНомер дома введен некорректно!");
                }
            }
        }
        public string UserAddressApartmentNumber
        {
            get => _userAddressApartmentNumber;
            set
            {
                ValidationTrueFalseCkeck = AddressApartmentNumberValidation(value);
                if (ValidationTrueFalseCkeck) _userAddressApartmentNumber = value;
                else
                {
                    Logger logger = new Logger("Номер квартиры введен некорректно!");
                    logger.LogWrite(logger.Info);
                    Console.WriteLine("\nНомер квартиры введен некорректно!");
                }
            }
        }
        public static User NewUser()
        {
            if (_user == null) { _user = new User(); }
            return _user;
        }
        string FirstCharInNameToUpper(string value)
        {
            string newValue = string.Empty;
            for (int i = 0; i < value.Length; i++)
            {
                if (i == 0) { newValue += value[i].ToString().ToUpper(); }
                else { newValue += value[i].ToString(); }
            }
            return newValue;
        }
        [Obsolete]
        bool NameValidation(string value)
        {
            if (value == string.Empty) return false;
            if (value.Length > 40) return false;
            return true;
        }
        [Obsolete]
        bool MailValidation(string value)
        {
            ArrayList charList = new ArrayList();
            foreach (char x in value) charList.Add(x);
            if (value.Length > 20) return false;
            if (charList.Contains('@') & charList.Contains('.')) return true;
            return false;
        }
        [Obsolete]
        bool PassValidation(string value)
        {
            if (value == string.Empty) return false;
            if (value.Length > 30 | value.Length < 3) return false;
            return true;
        }
        [Obsolete]
        bool PhoneNumberValidation()
        {
            Assembly assembly = Assembly.Load("Pilot_Project");
            Type userClassType = assembly.GetType("Pilot_Project.RegistrationAndLogo.User");
            var tempInstanceUserClass = Activator.CreateInstance(userClassType);
            MethodInfo method = userClassType.GetMethod("NewUser", BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static);
            var instanceUserClass = method.Invoke(tempInstanceUserClass, new object[] { });
            FieldInfo field = userClassType.GetField("_userPhoneNumber", BindingFlags.Instance | BindingFlags.NonPublic);

            foreach (FieldInfo tempfield in userClassType.GetFields(BindingFlags.Instance | BindingFlags.NonPublic))
            {
                foreach (Attribute attribute in tempfield.GetCustomAttributes())
                {
                    UserDataValidationAttribute userDataValidationAttribute = attribute as UserDataValidationAttribute;
                    if (userDataValidationAttribute == null) continue;
                    if (tempfield.Name == field.Name & Convert.ToString(field.GetValue(instanceUserClass)).Length == userDataValidationAttribute.FixedPhoneNumberLength)
                    {
                        if (userDataValidationAttribute.RunLetterCheck == true)
                        {
                            if (userDataValidationAttribute.CheckForLetters(Convert.ToString(field.GetValue(instanceUserClass))))
                            {
                                return true;
                            }
                            return false;
                        }
                        return true;
                    }
                }
            }
            return false;
        }
        [Obsolete]
        bool AddressStreetValidation(string value)
        {
            if (value == string.Empty) return false;
            if (value.Length > 50) return false;
            return true;
        }
        [Obsolete]
        bool AddressHouseNumberValidation(string value)
        {
            if (value == string.Empty) return false;
            if (value.Length > 3) return false;
            if (Convert.ToInt32(value) > 300 | Convert.ToInt32(value) < 1) return false;
            return true;
        }
        [Obsolete]
        bool AddressApartmentNumberValidation(string value)
        {
            if (value == string.Empty) return false;
            if (value.Length > 4) return false;
            if (Convert.ToInt32(value) > 1329 | Convert.ToInt32(value) < 1) return false;
            return true;
        }
        public static string InputWithLimitedStringLength<T>(T value) 
        {
            int maxLength = 0;
            bool allowedInput = false;
            Console.CursorVisible = false;
            StringBuilder userValue = new StringBuilder(maxLength);
            int curStart = Console.CursorLeft;
            int curOffset = 0;

            ConsoleKeyInfo keyInfo;
            do
            {
                keyInfo = Console.ReadKey(true);
                if (value.Equals(1)) { maxLength = 50; allowedInput = char.IsLetter(keyInfo.KeyChar); }
                if (value.Equals(2)) { maxLength = 42; allowedInput = char.IsLetter(keyInfo.KeyChar) | char.IsNumber(keyInfo.KeyChar) | char.IsPunctuation(keyInfo.KeyChar); }
                if (value.Equals(3)) { maxLength = 47; allowedInput = char.IsLetter(keyInfo.KeyChar) | char.IsNumber(keyInfo.KeyChar); }
                if (value.Equals("userPhoneNumber")) { maxLength = 21; allowedInput = /*char.IsLetter(keyInfo.KeyChar) |*/ char.IsNumber(keyInfo.KeyChar); }
                if (value.Equals("userAddressStreet")) { maxLength = 31; allowedInput = char.IsLetter(keyInfo.KeyChar) | char.IsNumber(keyInfo.KeyChar) | char.IsSeparator(keyInfo.KeyChar); }
                if (value.Equals("userAddressHouseNumber")) { maxLength = 39; allowedInput = char.IsNumber(keyInfo.KeyChar); }
                if (value.Equals("userAddressApartmentNumber")) { maxLength = 35; allowedInput = char.IsNumber(keyInfo.KeyChar); }

                if (allowedInput && userValue.Length < maxLength)
                {
                    userValue.Insert(curOffset, keyInfo.KeyChar);
                    curOffset++;
                    Console.Write(keyInfo.KeyChar);
                }
                if (keyInfo.Key == ConsoleKey.LeftArrow && curOffset > 0) curOffset--;
                if (keyInfo.Key == ConsoleKey.RightArrow && curOffset < userValue.Length) curOffset++;
                if (keyInfo.Key == ConsoleKey.Backspace | keyInfo.Key == ConsoleKey.Delete && curOffset > 0)
                {
                    curOffset--;
                    userValue.Remove(curOffset, 1);
                    Console.CursorLeft = curStart;
                    Console.Write(userValue.ToString().PadRight(maxLength));
                }
                Console.CursorLeft = curStart + curOffset;
            }
            while (!(keyInfo.Key == ConsoleKey.Enter && userValue.Length >= 0));
            Console.WriteLine();
            return userValue.ToString();
        }
    }
}