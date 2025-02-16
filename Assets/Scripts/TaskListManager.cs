using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;
using System;

[System.Serializable]
public class TaskListManager : MonoBehaviour
{
    public Transform content;

    public GameObject taskListItemPrefab;

    private List<TaskListObj> taskListObjects = new List<TaskListObj>();

    public InputField addInputField;

    public Button addButton;
 
    [System.Serializable]
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
        //UserData userData = new UserData();

        UserData userData = FileStorage.LoadData();

        if (userData == null)
        {
            userData = new UserData();
            userData.userId = FileStorage.GenerateUserId();
        }

        if (taskListObjects.Count > 0)
        {
            userData.taskList.Clear();

            foreach (var task in taskListObjects)
            {
                Debug.Log("Saving Task: " + task.objName);
                userData.taskList.Add(new TaskListManager.TasklistItem(task.objName, task.index, task.timestamp));
            }
        }
        else
        {
            Debug.Log("No tasks to save!");
            userData.taskList.Clear();
        }

        string jsonOutput = JsonUtility.ToJson(userData, true);
        
        FileStorage.SaveData(userData);
    }

    void LoadJSONData()
    {
        UserData loadedData = FileStorage.LoadData();

        if (loadedData != null && loadedData.taskList.Count > 0)
        {
            taskListObjects.Clear();
            int index = 0;

            foreach (var task in loadedData.taskList)
            {
                CreateTaskListItem(task.objName, index, true, task.timestamp);
                index++;
            }
        }
    }
}
