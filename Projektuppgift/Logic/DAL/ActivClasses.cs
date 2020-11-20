using Logic.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.DAL
{
    public class ActivClasses
    {
        // fält som tar ut nyckeln för en specifik mekaniker
        static public string UserKey { get; set; }
        //Dictionary av mekaniker med ett ID som nyckel.
        static public Dictionary<string, List<Mechanic>> mechanicDictionary { get; set; } = new Dictionary<string, List<Mechanic>>();
        //Lista av admin
        static public List<User> loginListAdmin { get; set; } = new List<User>();
        // Lista av användare
        static public List<User> loginListUser { get; set; } = new List<User>();
        // Dictionary av ärenden med ett ID som nyckel. 
        static public Dictionary<string, List<Orders>> orderDictionary { get; set; } = new Dictionary<string, List<Orders>>();
        // Lista av alla fordon som verkstaden kan hantera. 
        static public List<string> ListOfVehicles { get; set; } = new List<string>();
        // Lista med alla färdigmarkerade ärenden
        static public List<Orders> AllFinishedOrder { get; set; } = new List<Orders>();

     


    }
}
