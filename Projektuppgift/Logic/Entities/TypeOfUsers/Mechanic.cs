using Logic.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Logic.Entities
{
    public class Mechanic : User
    {
        public Mechanic()
        {

        }
        public Mechanic(string firstName, string lastName, DateTime birthDay, DateTime dateOfEmp, bool engine, bool tire, bool window, bool brakes, bool kaross, string id)
        {
            FirstNameOfMechanic = firstName;
            LastNameOfMechanic = lastName;
            BirthdayOfMechanic = birthDay;
            DateOfEmploymentOfMechanic = dateOfEmp;
            Engine = engine;
            Tire = tire;
            Brakes = brakes;
            Kaross = kaross;
            Window = window;
            UserID = id;
       
        }

        
        public string FirstNameOfMechanic { get; set; }

        public string LastNameOfMechanic { get; set; }

        public DateTime BirthdayOfMechanic { get; set; }

        public DateTime DateOfEmploymentOfMechanic { get; set; }

        public bool Engine { get; set; }
        public bool Tire { get; set; }
        public bool Window { get; set; }
        public bool Brakes { get; set; }
        public bool Kaross { get; set; }

        public List<Orders> ActivOrdersMechanic{ get; set; } = new List<Orders>();
        public List<Orders> FinishedOrdersMechanic { get; set; } = new List<Orders>();


    }
}
