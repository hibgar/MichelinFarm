using System;
using System.IO;
using UnityEngine;

public static class FileStorage
{
    private static string filePath = Path.Combine(Application.persistentDataPath, "userData.json");

    /* public static void SaveData(UserData data)
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(filePath, json);
        //Debug.Log("Data saved to: " + filePath);
    }

    public static UserData LoadData()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            return JsonUtility.FromJson<UserData>(json);
        }
        return new UserData(); // Return an empty list if no file exists
    } */

    public static UserData LoadData()
    {
        if (System.IO.File.Exists(filePath))
        {
            string json = System.IO.File.ReadAllText(filePath);
            return JsonUtility.FromJson<UserData>(json);
        }
        else
        {
            // First time opening the app: generate a unique ID
            UserData newUserData = new UserData();
            newUserData.userId = GenerateUserId();
            SaveData(newUserData);
            return newUserData;
        }
    }

    public static void SaveData(UserData data)
    {
        string json = JsonUtility.ToJson(data, true);
        System.IO.File.WriteAllText(filePath, json);
    }

    public static string GenerateUserId()
    {
        return "user_" + Guid.NewGuid().ToString(); // Generates a unique ID
    }
}
