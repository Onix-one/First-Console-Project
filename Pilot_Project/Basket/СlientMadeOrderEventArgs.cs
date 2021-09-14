using System;

namespace Pilot_Project.Basket
{
    public class СlientMadeOrderEventArgs : EventArgs
    {
        public string OrderListForUser { get; }
        public string OrderListForManager { get; }
        public СlientMadeOrderEventArgs(string orderListForUser, string orderListForManager)
        {
            OrderListForUser = orderListForUser;
            OrderListForManager = orderListForManager;
        }
    }
}
