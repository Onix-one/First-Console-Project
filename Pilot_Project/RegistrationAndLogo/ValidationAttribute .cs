using System;

namespace Pilot_Project.RegistrationAndLogo
{
    [AttributeUsage(AttributeTargets.Field)]
    class UserDataValidationAttribute : Attribute
    {
        public int FixedPhoneNumberLength { get; set; }
        public bool RunLetterCheck { get; set; }

        public bool CheckForLetters(string value)
        {
            foreach (char x in Convert.ToString(value))
                if (char.IsLetter(x)) return false;
            return true;
        }
    }
}
