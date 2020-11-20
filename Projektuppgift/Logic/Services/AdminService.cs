using Logic.DAL;
using Logic.Entities;
using Logic.Interface;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;

namespace Logic.Services
{
    public class AdminService : ILogic
    {
        string
        firstName = String.Empty,
        lastName = String.Empty,
        username = String.Empty,
        password = String.Empty,
        orderDescription = String.Empty,
        typeOfVehicle = String.Empty,
        ModelName = String.Empty,
        Regnum = String.Empty,
        matare = String.Empty,
        regDate = String.Empty,
        specificQ1 = String.Empty,
        specificQ2 = String.Empty,
        typeOfFuel = String.Empty,
        assignedMechanic = String.Empty;

        DateTime birthDay,
                 dateOfEmp;
        bool
        engine = false,
        tire = false,
        brakes = false,
        kaross = false,
        window = false,
        statusActive = false,
        statusInActive = false;
        /// <summary>
        /// Skapar en ny användare och lägger till det till rätt mekaniker.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="id"></param>
        public void NewUser(string username, string password, string id)
        {
            if (ActivClasses.mechanicDictionary.ContainsKey(id))//-------------------------Kollar efter användare finns
            {
                foreach (var item in ActivClasses.mechanicDictionary[id])
                {
                    item.Username = username;
                    item.Password = password;
                    item.UserID = id;

                }
            }
            ActivClasses.loginListUser.Add(new User { Username = username, Password = password, UserID = id });
        }

        /// <summary>
        /// Tar bort en vald mekaniker beroende på ID:t.
        /// </summary>
        /// <param name="id"></param>
        public void DeletLogin(string id)
        {
            if (ActivClasses.mechanicDictionary.ContainsKey(id))//-------------------------Kollar efter användare finns
            {
                foreach (var item in ActivClasses.mechanicDictionary[id])
                {
                    item.Username = string.Empty;
                    item.Password = string.Empty;
                    item.UserID = string.Empty;

                }
                int index = ActivClasses.loginListUser.FindIndex(x => x.UserID == id);
                ActivClasses.loginListUser.RemoveAt(index);
            }
        }
     
        /// <summary>
        /// Hämtar alla aktiva användare och lägger deras ID, name och lösenord i en lista. 
        /// </summary>
        /// <returns></returns>
        public List<string> GetActivUser()
        {
            List<string> DeklareraLista = new List<string>();

            foreach (var item in ActivClasses.loginListUser)
            {
                string add = ($"ID: {item.UserID}" +
                    $"\nName: {item.Username}" +
                    $"\nPassword: {item.Password}");
                DeklareraLista.Add(add);
            }
            return (DeklareraLista);
        }
    
        /// <summary>
        /// Skapar en ny mekaniker. Lägger till mekanikern i en dictionary med mekanikerns ID som key för dictionaryt och listan som innehåll. 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="listOfMechanic"></param>
        public void NewMechanic(string id, List<Mechanic> listOfMechanic)
        {
            ActivClasses.mechanicDictionary.Add(id, listOfMechanic);

        }

    
        /// <summary>
        /// Tar bort en aktiv mekaniker, från både mekaniker dictionaryt och User listan. 
        /// </summary>
        /// <param name="id"></param>
       
        public void DeleteMechanic(string id)
        {
            ActivClasses.mechanicDictionary.Remove(id);
            int index = ActivClasses.loginListUser.FindIndex(x => x.UserID == id);
            if (index != -1)
                ActivClasses.loginListUser.RemoveAt(index);
        }

        /// <summary>
        /// Ändrar en mekanikers värde. Tar in alla uppdaterade variabler som behövs och ändrar. 
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="birthDay"></param>
        /// <param name="dateOfEmp"></param>
        /// <param name="engine"></param>
        /// <param name="tire"></param>
        /// <param name="window"></param>
        /// <param name="brakes"></param>
        /// <param name="kaross"></param>
        /// <param name="id"></param>
        public void ChangeMechanic(string firstName, string lastName, DateTime birthDay, DateTime dateOfEmp, bool engine, bool tire, bool window, bool brakes, bool kaross,string id)
        {
            foreach (var item in ActivClasses.mechanicDictionary[id])
            {
                item.FirstNameOfMechanic = firstName;
                 item.LastNameOfMechanic = lastName;
                item.BirthdayOfMechanic = birthDay;
                item.DateOfEmploymentOfMechanic = dateOfEmp;
                item.Engine = engine;
                item.Tire = tire;
                item.Brakes = brakes;
                item.Kaross = kaross;
                item.Window = window;
            }
            
        }

        /// <summary>
        /// Hämtar en specifik mekaniker beroende på vilket ID det är som skickas med. 
        /// Retunerar all info i en lista.
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
        /// Hämtar en mekaniker som är tillgänglig för den specifika ordern.
        /// För att en mekaniker ska vara tillgänglig så krävs det att den uppfyller vissa krav som visas nedan.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public List<string> GetMechanicForTheJob(string value)
        {
            List<string> listaAvKeys = GetOnlyKey();

            List<string> newListOfMechanics = new List<string>();

            for (int i = 0; i < listaAvKeys.Count; i++)
            {
                string key = listaAvKeys[i];
                foreach (var item in ActivClasses.mechanicDictionary[key])
                {
                    if (value == "Brakes" && item.Brakes == true && item.ActivOrdersMechanic.Count <= 1)
                    {
                        string Name = $"{item.UserID} {item.FirstNameOfMechanic} {item.LastNameOfMechanic}";
                        newListOfMechanics.Add(Name);

                    }
                    else if (value == "Tires" && item.Tire == true && item.ActivOrdersMechanic.Count <= 1)
                    {
                        string Name = $"{item.UserID} {item.FirstNameOfMechanic} {item.LastNameOfMechanic}";
                        newListOfMechanics.Add(Name);

                    }
                    else if (value == "Engine" && item.Engine == true && item.ActivOrdersMechanic.Count <= 1)
                    {
                        string Name = $"{item.UserID} {item.FirstNameOfMechanic} {item.LastNameOfMechanic}";
                        newListOfMechanics.Add(Name);

                    }
                    else if (value == "Window" && item.Window == true && item.ActivOrdersMechanic.Count <= 1)
                    {
                        string Name = $"{item.UserID} {item.FirstNameOfMechanic} {item.LastNameOfMechanic}";
                        newListOfMechanics.Add(Name);

                    }
                    else if (value == "Kaross" && item.Kaross == true && item.ActivOrdersMechanic.Count <= 1)
                    {
                        string Name = $"{item.UserID} {item.FirstNameOfMechanic} {item.LastNameOfMechanic}";
                        newListOfMechanics.Add(Name);

                    }
                }
            }
            return newListOfMechanics;
        }

        /// <summary>
        /// Ger en specifik mekaniker ett nytt ärende. 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="newOrder"></param>
        public void GiveMechanicOrder(string id, List<Orders> newOrder)
        {
            
       // ---------------------------------------------------------------Finns inga mekaniker... funkar som in matning, (FindNumber hittar inga siffror!)
            
            foreach (var item in ActivClasses.mechanicDictionary[id])
            {
                List<Orders> Order = item.ActivOrdersMechanic;
                if (Order.Count == 1) { item.ActivOrdersMechanic = newOrder.Concat(Order).ToList(); }
                else { item.ActivOrdersMechanic = newOrder; }
            }
        }

        /// <summary>
        /// Hämtar enbart nyckeln hos en mekaniker.
        /// </summary>
        /// <returns></returns>
        public List<string> GetOnlyKey()
        {
            string savekey = string.Empty;
            List<string> DeklareraLista = new List<string>();

            Dictionary<string, List<Mechanic>>.KeyCollection keys = ActivClasses.mechanicDictionary.Keys;
            foreach (string key in keys)
            {
                savekey = key;
                foreach (var item in ActivClasses.mechanicDictionary[key])
                {
                    DeklareraLista.Add(savekey);
                }
            }
            return (DeklareraLista);
        }

        /// <summary>
        /// Hämtar nyckeln och info om aktiva mekaniker.
        /// </summary>
        /// <returns></returns>
        public List<string> GetKey()
        {
            string savekey = "";
            List<string> DeklareraLista = new List<string>();

            Dictionary<string, List<Mechanic>>.KeyCollection keys = ActivClasses.mechanicDictionary.Keys;
            foreach (string key in keys)
            {
                savekey = key;
                foreach (var item in ActivClasses.mechanicDictionary[key])
                {
                    string add = ($"ID: {savekey}" +
                        $"\nName: {item.FirstNameOfMechanic} {item.LastNameOfMechanic}");
                    DeklareraLista.Add(add);
                }
            }
            return (DeklareraLista);
        } 

        /// <summary>
        /// Hämtar en lista av alla fordon.
        /// </summary>
        /// <returns></returns>
        public List<string> GetVehicles()
        {
            List<string> showVehicles = new List<string>();

            foreach (var item in ActivClasses.ListOfVehicles)
            {
                showVehicles.Add(item);
            }
            return showVehicles;
        }
      

        /// <summary>
        /// Skapar en nytt ärende, tar en ett ärende ID och en lista med all info.
        /// Adderar detta till en dictionary. 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="newOrder"></param>
        public void NewOrder(string id, List<Orders> newOrder)
        {
            ActivClasses.orderDictionary.Add(id, newOrder);
        }
        /// <summary>
        /// Tar bort en aktiv order beroende på vilket ID som skickas in.
        /// Tar även bort ordern från den valda mekanikern. 
        /// </summary>
        /// <param name="id"></param>
        public void DeleteOrder(string id)
        {
            List<string> listaAvKeys = GetOnlyKey();

            for (int i = 0; i < listaAvKeys.Count; i++)
            {
                int index = -1;
                string key = listaAvKeys[i];
                foreach (var item in ActivClasses.mechanicDictionary[key])
                {
                    index = item.ActivOrdersMechanic.FindIndex(x => x.ID == id);

                    if (index != -1)
                    {
                        item.ActivOrdersMechanic.RemoveAt(index);
                    }
                }
            }
            ActivClasses.orderDictionary.Remove(id);
        }


        /// <summary>
        /// Hämtar ett specifik ärende baserat på vilket ID som skickas in.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<Orders> GetOrder(string id)
        {
            foreach (var item in ActivClasses.orderDictionary[id])
            {
                orderDescription = item.OrderDescription;
                typeOfVehicle = item.TypeOfVehicle;
                ModelName = item.ModelName;
                Regnum = item.RegNumber;
                matare = item.Matare;
                regDate = item.RegDate;
                specificQ1 = item.SpecificQuestionAboutVehicle1;
                specificQ2 = item.SpecificQuestionAboutVehicle2;
                engine = item.Engine;
                tire = item.Tire;
                brakes = item.Brakes;
                kaross = item.Kaross;
                window = item.BrokeWindow;
                typeOfFuel = item.Fuel;
                assignedMechanic = item.Mechanic;
            }
            List<Orders> changedOrder = new List<Orders>();
            changedOrder.Add(new Orders
            {
                OrderDescription = orderDescription,
                TypeOfVehicle = typeOfVehicle,
                ModelName = ModelName,
                RegNumber = Regnum,
                Matare = matare,
                RegDate = regDate,
                SpecificQuestionAboutVehicle1 = specificQ1,
                SpecificQuestionAboutVehicle2 = specificQ2,
                Engine = engine,
                Tire = tire,
                Brakes = brakes,
                Kaross = kaross,
                BrokeWindow = window,
                Fuel = typeOfFuel,
                Mechanic = assignedMechanic
            });
            return changedOrder;
        }
        /// <summary>
        /// Hämtar enbart nycklen för alla ärendenn.
        /// </summary>
        /// <returns></returns>
        public List<string> GetOnlyOrdersKey()
        {
            string savekey = string.Empty;
            List<string> DeklareraLista = new List<string>();

            Dictionary<string, List<Orders>>.KeyCollection keys = ActivClasses.orderDictionary.Keys;
            foreach (string key in keys)
            {
                savekey = key;
                foreach (var item in ActivClasses.orderDictionary[key])
                {
                    DeklareraLista.Add(savekey);
                }
            }
            return (DeklareraLista);
        }

        /// <summary>
        /// Hämtar nycklen och info om alla aktiva ärenden.
        /// </summary>
        /// <returns></returns>
        public List<string> GetKeyForOrder()
        {
            string savekey = "";
            List<string> DeklareraLista = new List<string>();

            Dictionary<string, List<Orders>>.KeyCollection keys = ActivClasses.orderDictionary.Keys;
            foreach (string key in keys)
            {
                savekey = key;
                foreach (var item in ActivClasses.orderDictionary[key])
                {
                    string add = ($"ID: {savekey}" +
                        $"\nName: {item.OrderDescription} {item.TypeOfVehicle}");
                    DeklareraLista.Add(add);
                }
            }
            return (DeklareraLista);
        }
        /// <summary>
        /// hämtar info om alla ärenden som är avslutade och ligger i AllFinishedOrder. 
        /// </summary>
        /// <returns></returns>
        public List<string> GetfinishedOrder()
        {
            List<string> Lista = new List<string>();
            foreach (var item in ActivClasses.AllFinishedOrder)
            {
                string add = ($"ID: {item.ID}" +
                    $"\n Beskrivning: {item.OrderDescription} {item.TypeOfVehicle}" +
                    $"\n Fordnon:{item.TypeOfVehicle} " +
                    $"\n Ansvarig:{item.Mechanic}");
                Lista.Add(add);
            }
            return (Lista);
        }
        /// <summary>
        /// Den här visar alla avslutade ärenden i en lista. 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<Orders> finishedOrder(string id)
        {
            List<Orders> DeklareraLista = new List<Orders>();
            string mechanic = null;
            foreach (var items in ActivClasses.orderDictionary[id])
            {

                string orderDescription = items.OrderDescription;
                bool whatIsBroken1 = items.Brakes;
                bool whatIsBroken2 = items.BrokeWindow;
                bool whatIsBroken3 = items.Engine;
                bool whatIsBroken4 = items.Kaross;
                bool whatIsBroken5 = items.Tire;
                string vehicle = items.TypeOfVehicle;
                mechanic = items.Mechanic;
                string modellName = items.ModelName;
                string regNumber = items.RegNumber;
                string matare = items.Matare;
                string regDate = items.RegDate;
                string typeOfFuel = items.Fuel;
                string specificQOne = items.SpecificQuestionAboutVehicle1;
                string specificQTwo = items.SpecificQuestionAboutVehicle2;
                string mechanicID = items.MechanicID;

                string ide = items.ID;

                DeklareraLista.Add(new Orders(orderDescription, whatIsBroken1, whatIsBroken2, whatIsBroken3, whatIsBroken4, whatIsBroken5, vehicle, mechanic, modellName, regNumber, matare, regDate
                   , typeOfFuel, specificQOne, specificQTwo, ide, mechanicID));

            }
            return DeklareraLista;
        }

        /// <summary>
        /// Flyttar ett ärende från aktiv till Avslutat.
        /// </summary>
        /// <param name="finished"></param>
        /// <param name="id"></param>
        public void MoveFinishedOrder(List<Orders> finished, string id)
        {
            string mechanicID = "";

            foreach (var item in finished)
            {
                mechanicID = item.MechanicID;
            }

            foreach (var item in ActivClasses.mechanicDictionary[mechanicID])
            {
                item.FinishedOrdersMechanic.AddRange(finished);
                ActivClasses.AllFinishedOrder.AddRange(finished);
                int index = item.ActivOrdersMechanic.FindIndex(x => x.ID == id);
                item.ActivOrdersMechanic.RemoveAt(index);
            }
            ActivClasses.orderDictionary.Remove(id);
        }

    }
}


