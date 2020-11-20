using Logic.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Interface
{
    public interface ILogicUser
    {
        void SetUser(string user);
        List<Mechanic> GetMechanic(string id);
        void Changes
       (
       bool Engine,
       bool Tire,
       bool Brakes,
       bool Kaross,
       bool Window);
        List<string> GetOrder();
        List<string> GetOrder(string id);
       void finishedOrder(string id);
        bool ActivOrder(string id);
        List<string> GetfinishedOrder();
        List<string> GetfinishedOrder(string id);
       
    }
}
