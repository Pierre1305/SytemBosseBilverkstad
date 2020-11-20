using Logic.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Interface
{
  public interface ILogic
    {

       
        //---------------------------------USER METHODS
        void DeletLogin(string id);

        void NewUser(string username, string password, string id);
         List<string> GetKey();
        List<string> GetActivUser();


        //---------------------------------MECHANIC METHODS
        void NewMechanic(string id, List<Mechanic> listOfMechanic);
        List<Mechanic> GetMechanic(string id);
        void ChangeMechanic(string firstName, string lastName, DateTime birthDay, DateTime dateOfEmp, bool engine, bool tire, bool window, bool brakes, bool kaross, string id);
        void DeleteMechanic(string id);
        List<string> GetMechanicForTheJob(string value);


        //---------------------------------ORDER METHODS
        public List<string> GetVehicles();
       
        void NewOrder(string id, List<Orders> newOrder);

        List<Orders> GetOrder(string id);

        List<string>GetKeyForOrder();
        void GiveMechanicOrder(string valueOfMechanic, List<Orders> newOrder);

        List<string> GetfinishedOrder();

        void DeleteOrder(string id);
        List<Orders> finishedOrder(string id);
        void MoveFinishedOrder(List<Orders> finished, string id);
       





    }
}
