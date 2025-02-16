using System.IO;
using UnityEngine;

public static class FileStorage
{
    private static string filePath = Path.Combine(Application.persistentDataPath, "userData.json");

    public static void SaveData(UserData data)
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
    }
}
