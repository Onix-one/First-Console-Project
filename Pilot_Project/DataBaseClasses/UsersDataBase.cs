using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Threading.Tasks;
using Pilot_Project.RegistrationAndLogo;


namespace Pilot_Project.DataBaseClasses
{
    internal class UsersDataBase : DataBase, IUsersDataBase, IDisposable
    {
        private Table<User> UsersTable { get; }
        private readonly List<User> _users;
        static UsersDataBase _usersDataBase;
        private UsersDataBase()
        {
            UsersTable = DataContext.GetTable<User>();
            try
            {
                if (DataContext.DatabaseExists() == false)
                {
                    throw new Logger("Коллекция _users = Null");
                }

                if (DataContext.DatabaseExists())
                {
                    IQueryable<User> users = from u in UsersTable
                        where u.Mail != null
                        select u;
                    _users = users.ToList();
                }
            }
            catch (Logger exception)
            {
                exception.LogWrite(exception.Error);
            }
        }
        public static UsersDataBase NewUsersDataBase()
        {
            if (_usersDataBase == null) _usersDataBase = new UsersDataBase();
            return _usersDataBase;
        }
        private void AddUserIntoDatabase()
        {
            User user = User.NewUser();
            UsersTable.InsertOnSubmit(user);
            DataContext.SubmitChanges();
            _usersDataBase?.Dispose();
        }
        public bool CheckingEmailInUserDatabase()
        {
            User user = User.NewUser();
            return _users.Any(u => u.Mail == user.Mail);
        }
        public bool CheckingPasswordInUserDatabase()
        {
            User user = User.NewUser();
            foreach (var u in _users.Where(u => u.Mail == user.Mail & u.Pass == user.Pass))
            {
                user.Name = u.Name; return true;
            }
            _usersDataBase?.Dispose();
            return false;
        }
        public async Task AddUserIntoDatabaseAsync()
        {
            await Task.Factory.StartNew(AddUserIntoDatabase);
        }

        public void Dispose()
        {
            _usersDataBase = null;
        }
    }
}
