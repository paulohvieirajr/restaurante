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
                .IsTrue(Validar(), "Price.Value", "Por favor, informe um preço maior que zero."));
        }

        public decimal Value { get; private set; }

        private bool Validar()
        {
            return Value <= 0;
        }
    }
}
