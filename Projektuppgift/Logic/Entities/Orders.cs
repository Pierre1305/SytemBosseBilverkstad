using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Entities
{
    /// <summary>
    /// Orders är basklassen som är mallen för hur en order ska se ut och vad den ska innehålla.
    /// Vehicles ärver av denna klass då det behövs ett fordon för att skapa en order. 
    /// </summary>
    public class Orders : Vehicles
    {
        public bool Engine { get; set; }
        public bool Tire { get; set; }
        public bool BrokeWindow { get; set; }
        public bool Brakes { get; set; }
        public bool Kaross { get; set; }

        public string OrderDescription { get; set; }

        public string TypeOfVehicle { get; set; }

        public string Mechanic { get; set; }

        
        public string ID { get; set; }

        public string MechanicID { get; set; }




        public Orders(string orderDescription, bool whatIsBroken1, bool whatIsBroken2, bool whatIsBroken3, bool whatIsBroken4, bool whatIsBroken5, string vehicle, string mechanic,
                               string modellName, string regNumber, string matare, string regDate, string typeOfFuel, string specificQOne, string specificQTwo, string id, string mechanicID)
        {
            OrderDescription = orderDescription;
            TypeOfVehicle = vehicle;
            Brakes = whatIsBroken1;
            BrokeWindow = whatIsBroken2;
            Engine = whatIsBroken3;
            Kaross = whatIsBroken4;
            Tire = whatIsBroken5;
            ModelName = modellName;
            RegNumber = regNumber;
            Matare = matare;
            RegDate = regDate;
            Fuel = typeOfFuel;
            Mechanic = mechanic;
            ID = id;
            MechanicID = mechanicID;
            

            if(TypeOfVehicle == "Bil")
            {
                SpecificQuestionAboutVehicle1 = specificQOne;
                SpecificQuestionAboutVehicle2 = specificQTwo;
            }
           else if(vehicle == "Lastbil")
            {
                SpecificQuestionAboutVehicle1 = specificQOne;
                SpecificQuestionAboutVehicle2 = String.Empty;
            }
           else if (vehicle == "Buss")
            {
                SpecificQuestionAboutVehicle1 = specificQOne;
                SpecificQuestionAboutVehicle2 = String.Empty;
            }

        }

        public Orders()
        {
        }




    }
}
