/* using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class UserProgressManager : MonoBehaviour
{

    string filePath;

    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        filePath = Application.persistentDataPath + "/progress.txt";
        Debug.Log(filePath);
        LoadJSONData();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SaveJSONData()
    {
        string contents = "";

        File.WriteAllText(filePath, contents);
    }

    void LoadJSONData()
    {
        if (File.Exists(filePath))
        {
            string contents = File.ReadAllText(filePath);

            string[] splitContents = contents.Split('\n');
            
            foreach(string content in splitContents)
            {
                
            }
        } else
        {
            Debug.Log("No file!");
        }
    }
}
 */