using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveLoad
{
    public static void Save(SaveContents objectToSave, string key){
        Debug.Log(objectToSave);
        string path = Application.persistentDataPath + "/saves/";
        Directory.CreateDirectory(path);
        BinaryFormatter formatter= new BinaryFormatter();
        using (FileStream fileStream = new FileStream(path + key + ".txt", FileMode.Create)){
            Debug.Log(objectToSave);
            Debug.Log(((SaveContents)objectToSave).player);
            Debug.Log(objectToSave.team);
            formatter.Serialize(fileStream, (SaveContents)objectToSave);
        }
        Debug.Log("Saved");
    }
    public static T Load<T>(string key){
        string path = Application.persistentDataPath + "/saves/";
        BinaryFormatter formatter= new BinaryFormatter();
        T returnValue = default(T);
        using (FileStream fileStream = new FileStream(path + key + ".txt", FileMode.Open)){
            returnValue=(T)formatter.Deserialize(fileStream);
        }
        Debug.Log("Loaded");
        return returnValue;
    }

    public static bool SaveExists(string key){
        string path = Application.persistentDataPath + "/saves/"+key+".txt";
        return File.Exists(path);
    }
    public static void SeriouslyDeleteAllSaveFile(){
        string path = Application.persistentDataPath + "/saves/";
        DirectoryInfo directory = new DirectoryInfo(path);
        directory.Delete();
        Directory.CreateDirectory(path);
    }
}
