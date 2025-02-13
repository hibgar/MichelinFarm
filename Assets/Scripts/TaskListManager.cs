using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;

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

        public TasklistItem(string name, int index)
        {
            this.objName = name;
            this.index = index;
        }
    } 

    private void Start()
    {
        filePath = Application.persistentDataPath + "/tasklist.txt";
        Debug.Log(filePath);
        LoadJSONData();
        addButton.onClick.AddListener(delegate { CreateTaskListItem(addInputField.text); });
    }

    void CreateTaskListItem(string name, int loadIndex = 0, bool loading = false) 
    {
        if (string.IsNullOrWhiteSpace(name)) return;

        GameObject item = Instantiate(taskListItemPrefab);

        item.transform.SetParent(content, false);
        item.SetActive(true);


        TaskListObj itemObject = item.GetComponent<TaskListObj>();
        
        int index;
        if (loading) 
        {
            index = loadIndex; // Use the saved index
        } 
        else 
        {
            index = taskListObjects.Count; // Assign a new unique index
        }

        itemObject.SetObjectInfo(name, index);
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
        SaveJSONData();
    }

    void SaveJSONData()
    {
        string contents = "";

        for (int i = 0; i < taskListObjects.Count; i++) 
        {
            TasklistItem temp = new TasklistItem(taskListObjects[i].name, taskListObjects[i].index);
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
            
            foreach(string content in splitContents)
            {
                if (string.IsNullOrWhiteSpace(content)) continue; // Skip empty lines
                TasklistItem temp = JsonUtility.FromJson<TasklistItem>(content.Trim());
                CreateTaskListItem(temp.objName, temp.index, true);
            }
        } else
        {
            Debug.Log("No file!");
        }
    }
}
