using Logic.DAL;
using Logic.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Logic.Services
{
    /// <summary>
    /// Validerar och kontrollerar användarens input. 
    /// </summary>
    public class ValidService : IValid
    {
       //-------------------------------------KONTROLLERAR INLOGG
        public bool Login(string username, string password)
        {
            return ActivClasses.loginListAdmin.Exists(user => user.Username.Equals(username) && user.Password.Equals(password));
        }
        public bool LoginUser(string username, string password)
        {
            return ActivClasses.loginListUser.Exists(user => user.Username.Equals(username) && user.Password.Equals(password));
        }

        //-------------------------------------KONTROLLERAR AKTIVA ID:s
        public bool ActivUser(string id)
        {
            if (ActivClasses.mechanicDictionary.ContainsKey(id)) { return true; }
            else { return false; }
        }
        public bool ActivOrder(string id)
        {
            if (ActivClasses.orderDictionary.ContainsKey(id)) { return true; }
            else { return false; }
        }

        //-------------------------------------KONTROLLERAR TILLÅTNA INPUTS.
        public bool ValidOrder(string orderDescription, string vehicle, string mechanic,
                              string modellName, string regNumber, string matare, string regDate, string typeOfFuel, string specificQOne, string specificQTwo, string id)
        {
            if (orderDescription != String.Empty && vehicle != String.Empty && mechanic != null && mechanic != "Finns inga mekaniker för jobbet." &&
                modellName != String.Empty && regNumber != String.Empty && matare != String.Empty && typeOfFuel != String.Empty && specificQOne != String.Empty && specificQTwo != String.Empty)
            {
                if (ActivClasses.orderDictionary.ContainsKey(id))
                    return false;

                else {return true;}
            }
            else { return false; }
        }

        public bool ValidOrder(string orderDescription, string vehicle, string mechanic,
                             string modellName, string regNumber, string matare, string regDate, string typeOfFuel)
        {
            if (orderDescription != String.Empty && vehicle != String.Empty && mechanic != null && mechanic != "Finns inga mekaniker för jobbet." &&
                modellName != String.Empty && regNumber != String.Empty && matare != String.Empty && typeOfFuel != String.Empty) 
            { return true; }
           
            else { return false; }
        }


        public bool ValidMechanic(string firstName, string lastName, string dateOfBirth, string dateOfEmp, string id)
        {
            if (firstName != String.Empty && lastName != String.Empty && dateOfBirth != String.Empty && dateOfEmp != String.Empty && id != String.Empty)
            {
                if (Regex.IsMatch(dateOfBirth, @"^\d{4}\-(0[1-9]|1[012])\-(0[1-9]|[12][0-9]|3[01])$") && Regex.IsMatch(dateOfEmp, @"^^\d{4}\-(0[1-9]|1[012])\-(0[1-9]|[12][0-9]|3[01])$"))
                {
                    string input = dateOfEmp;
                    DateTime dateTimeOfbirth = DateTime.Parse(input);
                    input = dateOfBirth;
                    DateTime dateTimeOfEmployment = DateTime.Parse(input);
                    if (ActivClasses.mechanicDictionary.ContainsKey(id))
                        return false;

                    return true;
                }
                else { return false; }
            }
            else { return false; }
        }
        public bool ValidMechanic(string firstName, string lastName, string dateOfBirth, string dateOfEmp)
        {
            if (firstName != String.Empty && lastName != String.Empty && dateOfBirth != String.Empty && dateOfEmp != String.Empty)
            {
                if (Regex.IsMatch(dateOfBirth, @"^\d{4}\-(0[1-9]|1[012])\-(0[1-9]|[12][0-9]|3[01])$") && Regex.IsMatch(dateOfEmp, @"^^\d{4}\-(0[1-9]|1[012])\-(0[1-9]|[12][0-9]|3[01])$"))
                {
                    string input = dateOfEmp;
                    DateTime dateTimeOfbirth = DateTime.Parse(input);
                    input = dateOfBirth;
                    DateTime dateTimeOfEmployment = DateTime.Parse(input);
                    return true;
                }
                else { return false; }
            }
            else { return false; }
        }
        public bool ValidMechanicID(string ID)
        {
          return  Regex.IsMatch(ID, "^[0-9]+$", RegexOptions.Compiled);
        }
        public bool ValidMechanicName(string name)
        {
          return Regex.IsMatch(name, @"^[a-öA-Ö]+$");   
        }
        public bool ValidLogin(string username, string password, string password2, string id)
        {
            if (Regex.IsMatch(username, @"^([\w-.]+)@(([[0-9]{1,3}.[0-9]{1,3}.[0-9]{1,3}.)|(([\w-]+.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(]?)$") && password == password2 && id != null)
            {
                if (ActivClasses.loginListUser.Exists(x => x.Username.Equals(username))) { return false; }
                else { return true; }
            }
            else { return false; }
        }

        //Sorterar ut siffror från en string. Dessa siffror är mekanikerns ID.
        public string FindNumber(string str)
        {
            string[] numbers = Regex.Split(str, @"\D+");
            foreach (string value in numbers)
            {
                if (!string.IsNullOrEmpty(value))
                {
                    int i = int.Parse(value);
                    str = i.ToString();
                }
            }
            return str;
        }
    }
}
