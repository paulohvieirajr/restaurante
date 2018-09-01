using Flunt.Validations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.Domain.Entities
{
    public class Restaurant : Base
    {
        #region Constructors

        protected Restaurant()
        {

        }

        public Restaurant(string name)
        {
            Name = name;
            AddValidation();
        }

        #endregion

        #region Properts

        public int IdRestaurant { get; protected set; }

        public string Name { get; protected set; }

        public virtual List<Dish> Dishes { get; set; }

        #endregion

        #region Methods

        public void ModifyName(string name)
        {
            Name = name;
            AddValidation();
        }

        public void Disable()
        {
            this.Status = false;
        }

        private void AddValidation()
        {
            AddNotifications(new Contract()
                .Requires()
                .HasMinLen(Name, 3, "Restaurant.Name", "Please, put a name with at least 3 characters")
                .HasMaxLen(Name, 50, "Restaurant.Name", "Please, put a name with a maximum of 40 characters"));
        }

        #endregion
    }
}
