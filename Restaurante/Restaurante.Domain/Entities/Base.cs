using Flunt.Notifications;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.Domain.Entities
{
    public class Base : Notifiable
    {
        public Base()
        {
            Status = true;
        }
        public bool Status { get; protected set; }
    }
}
