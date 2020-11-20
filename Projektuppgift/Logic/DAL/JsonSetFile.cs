using Logic.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace Logic.DAL
{
   public class JsonSetFile: JsonFile
    {

        //Innan programmet stängs ner så sparas alla listor och dictionarys i JsonFiler.
        public void SetJson()
        {
            SaveList(pathAdmin, ActivClasses.loginListAdmin);
            SaveList(pathUser, ActivClasses.loginListUser);
            SaveList(pathVehicles, ActivClasses.ListOfVehicles);
            SaveList(finishedOrder, ActivClasses.AllFinishedOrder);
            SaveDictionary(pathMechanic, ActivClasses.mechanicDictionary);
            SaveDictionary(pathOrder, ActivClasses.orderDictionary);
        }

        //Sparar alla listor med en sökväg i JsonFiler
        private void SaveDictionary<T>(string path, Dictionary<string,T> dictonary  )
        {
            FileStream fileStream = File.OpenWrite(path);
            fileStream.SetLength(0);
            fileStream.Close();
            if (dictonary.Count != 0)
            {
                string Json = JsonSerializer.Serialize(dictonary);
                fileStream = File.OpenWrite(path);
                using (StreamWriter streamWriter = new StreamWriter(fileStream))
                {
                    streamWriter.WriteLine(Json);

                }
            }
            else{ File.Delete(path);}
        }

        //Sparar alla Dictionarys med en sökväg i JsonFiler.
        private void SaveList<T>(string path,List<T> list )
        {
            FileStream fileStream = File.OpenWrite(path);
            fileStream.SetLength(0);
            fileStream.Close();

            if (list.Count != 0)
            {
                string Json = JsonSerializer.Serialize(list);
                fileStream = File.OpenWrite(path);
                using (StreamWriter streamWriter = new StreamWriter(fileStream))
                {
                    streamWriter.WriteLine(Json);
                }
            }
            else {File.Delete(path); }
        } 
    }
}

