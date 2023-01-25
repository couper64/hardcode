using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[System.Serializable]
public struct PlayerData
{
    public const int MAX_LEVELS = 10;

    public uint balance;
    public int levelsOpened;
    public int levelLast;
}

public static class PlayerSaver
{
    private static string saveFile = "/playerData.bin";

    public static void Save(PlayerData playerData)
    {
        // Create formatter,
        BinaryFormatter formatter = new BinaryFormatter();

        // path and 
        string path = Application.persistentDataPath + saveFile;

        // file stream.
        FileStream stream;

        // Create file.
        stream = new FileStream(path, FileMode.Create);

        // Serializing.
        formatter.Serialize(stream, playerData);

        // Closing file.
        stream.Close();
    }

    public static PlayerData Load()
    {
        // Create formatter,
        BinaryFormatter formatter = new BinaryFormatter();

        // path and 
        string path = Application.persistentDataPath + saveFile;

        // file stream.
        FileStream stream;

        // File checking.
        if (File.Exists(path))
        {
            // Allocating and reading if success.
            stream = new FileStream(path, FileMode.Open);

            // Deserialising as UserData.
            PlayerData data = (PlayerData)formatter.Deserialize(stream);

            // Close stream.
            stream.Close();

            // And, return data.
            return data;
        }
        else
        {
            // Notify.
            Debug.Log("Could not find " + path + ". Creating new one...");

            // Create new one and terminate here.
            return new PlayerData();
        }
    }
}
