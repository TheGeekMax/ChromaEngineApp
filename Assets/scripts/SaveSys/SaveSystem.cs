using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem{
    public static void SavePlayer(Keep data){
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.fun";
        FileStream stream = new FileStream(path, FileMode.Create);

        SaveData saveData = new SaveData(data);

        formatter.Serialize(stream, saveData);
        stream.Close();
    }

    public static SaveData LoadPlayer(){
        string path = Application.persistentDataPath + "/player.fun";
        if(File.Exists(path)){
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SaveData saveData = formatter.Deserialize(stream) as SaveData;
            stream.Close();

            return saveData;
        }else{
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }

    public static void ResetPlayer(){
        string path = Application.persistentDataPath + "/player.fun";
        if(File.Exists(path)){
            File.Delete(path);
        }
    }
}
