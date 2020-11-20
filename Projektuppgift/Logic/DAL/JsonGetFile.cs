using Logic.Entities;

using Logic.MyExceptions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.Json;

namespace Logic.DAL
{
    public class JsonGetFile : JsonFile
    {

        //Hämtar alla aktiva JsonFiler.
        public void GetJson()
            {
            
            ActivClasses.loginListAdmin = GetList(pathAdmin, ActivClasses.loginListAdmin);
            ActivClasses.loginListUser = GetList(pathUser, ActivClasses.loginListUser);
            ActivClasses.ListOfVehicles = GetList(pathVehicles, ActivClasses.ListOfVehicles);
            ActivClasses.AllFinishedOrder = GetList(finishedOrder, ActivClasses.AllFinishedOrder);
            ActivClasses.mechanicDictionary = GetDictionary(pathMechanic, ActivClasses.mechanicDictionary);
            ActivClasses.orderDictionary = GetDictionary(pathOrder, ActivClasses.orderDictionary);
            
            if (ActivClasses.loginListAdmin.Count == 0)
            {
                ActivClasses.loginListAdmin.Add(new User { Username = "bosse", Password = "1", UserID = "Admin" });
            }
            if (ActivClasses.ListOfVehicles.Count == 0)
            {
                ActivClasses.ListOfVehicles.AddRange(new List<String>() { "Bil", "Buss", "Lastbil", "Motorcykel" });
            }
        }

        //Hämtar alla JsonListor som innehåller en sökväg och en lista. 
        private List<T> GetList<T>(string path, List<T> list)
        {

           
            if (File.Exists(path))
            {
                string jsonString = File.ReadAllText(path);
                list = JsonSerializer.Deserialize<List<T>>(jsonString);
               
            }
            else
            {
                FileStream fileStream = File.Create(path);
                using (var streamReader = new StreamReader(fileStream)) { }
            }
            return list;
        }

        //Hämtar alla JsonDictionarys som innehåller en sökväg och en Dictionary. 
        private Dictionary<string, T> GetDictionary<T>(string path, Dictionary<string, T> dictonary)
        {
            if (File.Exists(path))
            {
                string jsonString = File.ReadAllText(path);
                dictonary = JsonSerializer.Deserialize<Dictionary<string,T>>(jsonString);
              
            }
            else
            {
                FileStream fileStream = File.Create(path);
                using (var streamReader = new StreamReader(fileStream)){ }
            }
            return dictonary;
        }       
    }
}
