using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;
using System;

public class TaskListManager : MonoBehaviour
{
    public Transform content;

    public GameObject taskListItemPrefab;

    string filePath;

    private List<TaskListObj> taskListObjects = new List<TaskListObj>();

    public InputField addInputField;

    public Button addButton;
 
    public class TasklistItem
    {
        public string objName;
        public int index;
        public string timestamp;

        public TasklistItem(string name, int index, string timestamp)
        {
            this.objName = name;
            this.index = index;
            this.timestamp = timestamp;
        }
    } 

    private void Start()
    {
        filePath = Application.persistentDataPath + "/tasklist.txt";
        Debug.Log(filePath);
        LoadJSONData();
        addButton.onClick.AddListener(delegate { CreateTaskListItem(addInputField.text); });
    }

    void CreateTaskListItem(string name, int loadIndex = 0, bool loading = false, string timestamp = "") 
    {
        if (string.IsNullOrWhiteSpace(name)) return;

        GameObject item = Instantiate(taskListItemPrefab);

        item.transform.SetParent(content, false);
        item.SetActive(true);


        TaskListObj itemObject = item.GetComponent<TaskListObj>();
        
        int index;

        if (timestamp == "")
            timestamp = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"); // Get current timestamp
        
        if (loading) 
        {
            index = loadIndex; // Use the saved index
        } 
        else 
        {
            index = taskListObjects.Count; // Assign a new unique index
        }

        itemObject.SetObjectInfo(name, index, timestamp);
        taskListObjects.Add(itemObject);
        TaskListObj temp = itemObject;
        itemObject.GetComponent<Toggle>().onValueChanged.AddListener(delegate {CheckItem(temp); });
        addInputField.text = "";

        if (!loading)
            SaveJSONData();
    }

    void CheckItem(TaskListObj item)
    {
        taskListObjects.Remove(item);
        Destroy(item.gameObject);

        for (int i = 0; i < taskListObjects.Count; i++)
        {
            taskListObjects[i].index = i;
        }

        SaveJSONData();
    }

    void SaveJSONData()
    {
        string contents = "";

        for (int i = 0; i < taskListObjects.Count; i++) 
        {
            taskListObjects[i].index = i;
            TasklistItem temp = new TasklistItem(taskListObjects[i].name, taskListObjects[i].index, System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            contents += JsonUtility.ToJson(taskListObjects[i]) + "\n";
        }

        File.WriteAllText(filePath, contents);
    }

    void LoadJSONData()
    {
        if (File.Exists(filePath))
        {
            string contents = File.ReadAllText(filePath);

            string[] splitContents = contents.Split('\n');

            int index = 0; // Start fresh indexing

            foreach (string content in splitContents)
            {
                if (string.IsNullOrWhiteSpace(content)) continue; // Skip empty lines

                TasklistItem temp = JsonUtility.FromJson<TasklistItem>(content.Trim());

                // Assign a fresh sequential index
                CreateTaskListItem(temp.objName, index, true, temp.timestamp);
                index++;
            }
        }
        else
        {
            Debug.Log("No file!");
        }
    }
}
