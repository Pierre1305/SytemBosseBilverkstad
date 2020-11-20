using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Logic.MyExceptions
{
    /// <summary>
    /// En egenskapad Exception som kontrollerar ifall användaren har tagit bort DAL mappen, ifall detta har hänt går det inte att spara till Json
    /// Detta exception vågar problemet och meddelar användaren vad som har hänt och hur de kan lösa problemet. 
    /// </summary>
    public class ErrorException : Exception
    {
        public ErrorException() : base() { }
        public ErrorException(string message) : base(message) { }
        public ErrorException(string message, Exception inner) : base(message, inner) { }
        protected ErrorException(SerializationInfo info, StreamingContext context) : base(info, context) { }

     

        public override string Message
        {
            get
            {
                return"Somthing wrong, you missing a folder!" +
                    "\n" +
                    @"Go to: C:\SystemBosseBilverkstad\Projektuppgift\GUI\bin\Debug\netcoreapp3.1" +
                    "\nAnd and a folder with the name: DAL." +
                    "\n Now you ready to restart the program!";
            }

        }
     










    }

}

