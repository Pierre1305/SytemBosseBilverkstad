using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.DAL
{
    //De olika sökvägarna. 
  public abstract class JsonFile
    {
        protected const string pathUser = @"DAL\User.json";
        protected const string pathAdmin = @"DAL\UserAdmin.json";
        protected const string pathMechanic = @"DAL\Mechanic.json";
        protected const string pathOrder= @"DAL\Orders.json";
        protected const string pathVehicles = @"DAL\Vehicles.json";
        protected const string finishedOrder = @"DAL\finishedOrder.json";
    }
}
