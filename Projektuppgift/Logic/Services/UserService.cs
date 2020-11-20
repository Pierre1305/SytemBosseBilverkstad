 using Logic.DAL;
using Logic.Entities;
using Logic.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Logic.Services
{
    public class UserService : ILogicUser
    {
        string
        firstName = String.Empty,
        lastName = String.Empty,
        username = String.Empty,
        password = String.Empty;

        DateTime birthDay,
         dateOfEmp;

        bool
        engine = false,
        tire = false,
        brakes = false,
        kaross = false,
        window = false;

        /// <summary>
        /// Håller koll på vem som är inloggad
        /// </summary>
        /// <param name="user"></param>
        public void SetUser(string user)
        {
            var key = from x in ActivClasses.loginListUser
                      where x.Username == user
                      select x.UserID;
            key.ToList();

            foreach (var users in key)
            {
                ActivClasses.UserKey = users;
            }
        }

        /// <summary>
        /// Får ut all info om den inloggade mekanikern.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<Mechanic> GetMechanic(string id)
        {
            foreach (var item in ActivClasses.mechanicDictionary[id])
            {
                firstName = item.FirstNameOfMechanic;
                lastName = item.LastNameOfMechanic;
                birthDay = item.BirthdayOfMechanic;
                dateOfEmp = item.DateOfEmploymentOfMechanic;
                engine = item.Engine;
                tire = item.Tire;
                brakes = item.Brakes;
                kaross = item.Kaross;
                window = item.Window;
            }
            List<Mechanic> DeklareraLista = new List<Mechanic>();
            DeklareraLista.Add(new Mechanic
            {
                FirstNameOfMechanic = firstName,
                LastNameOfMechanic = lastName,
                BirthdayOfMechanic = birthDay,
                DateOfEmploymentOfMechanic = dateOfEmp,
                Engine = engine,
                Tire = tire,
                Brakes = brakes,
                Kaross = kaross,
                Window = window,
            });
            return DeklareraLista;
        }

        /// <summary>
        /// Ändra kompetens för den inloggade mekanikernn
        /// </summary>
        /// <param name="Engine"></param>
        /// <param name="Tire"></param>
        /// <param name="Window"></param>
        /// <param name="Brakes"></param>
        /// <param name="Kaross"></param>
        public void Changes
        (
        bool Engine,
        bool Tire,
        bool Window,
        bool Brakes,
        bool Kaross)
        {
            foreach (var item in ActivClasses.mechanicDictionary[ActivClasses.UserKey])
            {
                item.Engine = Engine;
                item.Tire = Tire;
                item.Brakes = Brakes;
                item.Kaross = Kaross;
                item.Window = Window;
            }
        }

        /// <summary>
        /// Hämtar information om aktiva ärenden för ADMIN.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<string> GetOrder(string id)
        {
            List<Orders> DeklareraLista = new List<Orders>();
            foreach (var item in ActivClasses.mechanicDictionary[id])
            {
                foreach (var items in item.ActivOrdersMechanic)
                {
                    DeklareraLista.Add(items);
                }
            }
            List<string> Lista = new List<string>();
            foreach (var item in DeklareraLista)
            {
                string add = ($"ID: {item.ID}" +
                    $"\n Beskrivning: {item.OrderDescription} {item.TypeOfVehicle}" +
                    $"\n Fordnon:{item.TypeOfVehicle} ");
                Lista.Add(add);
            }
            return (Lista);
        }


        /// <summary>
        /// Hämtar information om aktiva ärenden för INLOGGADE MEKANIKER.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<string> GetOrder()
        {
            List<Orders> DeklareraLista = new List<Orders>();
            foreach (var item in ActivClasses.mechanicDictionary[ActivClasses.UserKey])
            {
                foreach (var items in item.ActivOrdersMechanic)
                {
                    DeklareraLista.Add(items);
                }
            }
            List<string> Lista = new List<string>();
            foreach (var item in DeklareraLista)
            {
                string add = ($"ID: {item.ID}" +
                    $"\n Beskrivning: {item.OrderDescription} {item.TypeOfVehicle}" +
                    $"\n Fordnon:{item.TypeOfVehicle} ");
                Lista.Add(add);
            }
            return (Lista);
        }


        /// <summary>
        /// Hämtar lista av alla färdiga orders för den inloggade mekanikern
        /// </summary>
        /// <returns></returns>


        public List<string> GetfinishedOrder()
        {
            List<Orders> DeklareraLista = new List<Orders>();
            foreach (var item in ActivClasses.mechanicDictionary[ActivClasses.UserKey])
            {
                foreach (var items in item.FinishedOrdersMechanic)
                {
                    DeklareraLista.Add(items);
                }
            }
            List<string> Lista = new List<string>();
            foreach (var item in DeklareraLista)
            {
                string add = ($"ID: {item.ID}" +
                    $"\n Beskrivning: {item.OrderDescription} {item.TypeOfVehicle}" +
                    $"\n Fordnon:{item.TypeOfVehicle} ");
                Lista.Add(add);
            }
            return (Lista);
        }

        /// <summary>
        /// Hämtar lista av alla färdiga orders för ADMIN
        /// </summary>
        /// <returns></returns>
        public List<string> GetfinishedOrder(string id)
        {
            List<Orders> DeklareraLista = new List<Orders>();
            foreach (var item in ActivClasses.mechanicDictionary[id])
            {
                foreach (var items in item.FinishedOrdersMechanic)
                {
                    DeklareraLista.Add(items);
                }
            }
            List<string> Lista = new List<string>();
            foreach (var item in DeklareraLista)
            {
                string add = ($"ID: {item.ID}" +
                    $"\n Beskrivning: {item.OrderDescription} {item.TypeOfVehicle}" +
                    $"\n Fordnon:{item.TypeOfVehicle} ");
                Lista.Add(add);
            }
            return (Lista);
        }

        /// <summary>
        /// Ändrar ett aktivt ärende till färdig och tar bort från den aktiva listan. 
        /// </summary>
        /// <param name="id"></param>
        public void finishedOrder(string id)
        {
            List<Orders> DeklareraLista = new List<Orders>();
            foreach (var items in ActivClasses.orderDictionary[id])
            {

                string orderDescription = items.OrderDescription;
                bool whatIsBroken1 = items.Brakes;
                bool whatIsBroken2 = items.BrokeWindow;
                bool whatIsBroken3 = items.Engine;
                bool whatIsBroken4 = items.Kaross;
                bool whatIsBroken5 = items.Tire;
                string vehicle = items.TypeOfVehicle;
                string mechanic = items.Mechanic;
                string modellName = items.ModelName;
                string regNumber = items.RegNumber;
                string matare = items.Matare;
                string regDate = items.RegDate;
                string typeOfFuel = items.Fuel;
                string specificQOne = items.SpecificQuestionAboutVehicle1;
                string specificQTwo = items.SpecificQuestionAboutVehicle2;
                string mehanicID = items.MechanicID;
    
                string ide = items.ID;

                 DeklareraLista.Add(new Orders(orderDescription, whatIsBroken1, whatIsBroken2, whatIsBroken3, whatIsBroken4, whatIsBroken5, vehicle, mechanic, modellName, regNumber, matare, regDate
                    , typeOfFuel, specificQOne, specificQTwo, ide, mehanicID));

              }
            foreach (var item in ActivClasses.mechanicDictionary[ActivClasses.UserKey])
            {
                item.FinishedOrdersMechanic.AddRange(DeklareraLista);
                ActivClasses.AllFinishedOrder.AddRange(DeklareraLista);
                
                int index = item.ActivOrdersMechanic.FindIndex(x => x.ID == id);
                item.ActivOrdersMechanic.RemoveAt(index);
            }
            ActivClasses.orderDictionary.Remove(id);
      
            }

        /// <summary>
        /// Kontrollerar att det är ett aktivt ärende som skickas in. 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool ActivOrder(string id)
        {
            if (ActivClasses.orderDictionary.ContainsKey(id)) { return true; }
            else { return false; }
        }
 }  }
