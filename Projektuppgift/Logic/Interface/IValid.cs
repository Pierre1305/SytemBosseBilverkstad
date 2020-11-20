using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Interface
{
    public interface IValid
    {
        bool ValidMechanic(string firstName, string lastName, string dateOfBirth, string dateOfEmp, string id);
       bool ValidMechanic(string firstName, string lastName, string dateOfBirth, string dateOfEmp);
        bool ValidOrder(string orderDescription, string vehicle, string mechanic,
                            string modellName, string regNumber, string matare, string regDate, string typeOfFuel, string specificQOne, string specificQTwo, string id);
        bool ValidOrder(string orderDescription, string vehicle, string mechanic,
                           string modellName, string regNumber, string matare, string regDate, string typeOfFuel);

       bool ValidLogin(string username, string password, string password2, string id);

        bool ActivUser(string id);

        bool ActivOrder(string id);
        bool Login(string username, string password);

        bool LoginUser(string username, string password);
        bool ValidMechanicID(string ID);
        bool ValidMechanicName(string name);
        string FindNumber(string str);

    }
}
