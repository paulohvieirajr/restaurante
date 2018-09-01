using Flunt.Validations;
using Restaurant.Shared.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.Domain.ValueObjects
{
    public class Price : ValueObject
    {
        public Price(decimal value)
        {
            Value = value;

            AddNotifications(new Contract()
                .Requires()
                .IsLowerThan(0, value, "Price.Value", "Please, put the price value greater than zero"));
        }

        public decimal Value { get; private set; }

        private bool Validar()
        {
            return Value >= 0;
        }
    }
}
