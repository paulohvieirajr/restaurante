using Flunt.Validations;
using Restaurant.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.Domain.Entities
{
    public class Dish : Base
    {
        #region Value Objects

        private Price _Price { get; set; }

        #endregion

        #region Constructors

        protected Dish()
        {

        }

        public Dish(string name, Restaurant restaurant, decimal price)
        {
            Name = name;
            Restaurant = restaurant;
            Price = price;

            AddValidations();
        }

        #endregion

        #region Properts

        public int IdDish { get; protected set; }

        public string Name { get; protected set; }

        public decimal Price
        {
            get { return _Price.Value; }
            protected set { _Price = new Price(value); }
        }

        public int IdRestaurant { get; protected set; }

        public virtual Restaurant Restaurant { get; protected set; }

        #endregion

        #region Methods

        public void Modify(string name, Restaurant restaurant, decimal price)
        {
            Name = name;
            Restaurant = restaurant;
            Price = price;

            AddValidations();
        }

        private void AddValidations()
        {
            AddNotifications(new Contract()
                .Requires()
                .HasMinLen(Name, 3, "Dish.Name", "Por favor, informe um nome com pelo menos 3 caracteres")
                .HasMaxLen(Name, 50, "Dish.Name", "Por favor, informe um nome com no máximo 40 caracteres"));

            AddNotifications(Restaurant, _Price);
        }

        #endregion
    }
}
