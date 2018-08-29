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

        private void AddValidation()
        {
            AddNotifications(new Contract()
                .Requires()
                .HasMinLen(Name, 3, "Restaurant.Name", "Por favor, informe um nome com pelo menos 3 caracteres")
                .HasMaxLen(Name, 50, "Restaurant.Name", "Por favor, informe um nome com no máximo 50 caracteres"));
        }

        #endregion
    }
}
