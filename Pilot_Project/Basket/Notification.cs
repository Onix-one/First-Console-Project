using System;
using System.Net;
using System.Net.Mail;
using Pilot_Project.RegistrationAndLogo;

namespace Pilot_Project.Basket
{
    class Notification
    {
        internal void EventSubscription()
        {
            ShoppingBasket.СlientMadeOrder += NotificationForManager;
            ShoppingBasket.СlientMadeOrder += NotificationForUser;
        }
        private void NotificationForUser(object sender, СlientMadeOrderEventArgs val)
        {
            MailAddress from = new MailAddress("u4iha@rambler.ru", "Sushi Hous");
            MailAddress to = new MailAddress("Puffa.market@gmail.com");
            MailMessage mail = new MailMessage(from, to)
            {
                Subject = "Ваш заказ!",
                Body = $"Вас приветствует Sushi Hous!\nВаш заказ:\n{val.OrderListForUser}" +
                $"\nЗаказ будет доставлен в течении 45 минут с момента получения этого уведомления!",
                IsBodyHtml = false
            };
            SmtpClient smtp = new SmtpClient("smtp.rambler.ru", 587)
            {
                Credentials = new NetworkCredential("u4iha@rambler.ru", "qazol05081987"),
                EnableSsl = false
            };
            Console.Clear();
            Console.WriteLine("Идет отправка сообщения...");
            smtp.Send(mail);
            Console.WriteLine("На Вашу почту отправлено письмо с Вашим заказом!");
            Console.ReadKey();
        }
        private void NotificationForManager(object sender, СlientMadeOrderEventArgs val)
        {
            User user = User.NewUser();
            DateTime orderTime = DateTime.Now;
            MailAddress from = new MailAddress("u4iha@rambler.ru", "For Sushi Hous");
            MailAddress to = new MailAddress("Puffa.market@gmail.com");
            MailMessage mail = new MailMessage(from, to)
            {
                Subject = "ЗАКАЗ!",
                Body = $"Клиент: {user.Name} \nЗаказ клиента:\n{val.OrderListForManager}\nВремя заказа: {orderTime}" +
                $"\nНомер телефона: +375{user.UserPhoneNumber}" +
                $"\n Адрес доставки: ул.{user.UserAddressStreet}/д.{user.UserAddressHouseNumber}/кв.{user.UserAddressApartmentNumber}",
                IsBodyHtml = false
            };
            SmtpClient smtp = new SmtpClient("smtp.rambler.ru", 587)
            {
                Credentials = new NetworkCredential("u4iha@rambler.ru", "qazol05081987"),
                EnableSsl = false
            };
            smtp.Send(mail);
        }
    }
}
