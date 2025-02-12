using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class TaskListManager : MonoBehaviour
{
    public Transform content;

    public GameObject taskListItemPrefab;

    string filePath;

    private List<TaskListObj> taskListObjects = new List<TaskListObj>();

    public InputField addInputField;

    public Button addButton;

    private void Start()
    {
        filePath = Application.persistentDataPath + "/tasklist.txt";

        addButton.onClick.AddListener(delegate { CreateTaskListItem(addInputField.text); });
    }

    void CreateTaskListItem(string name) 
    {
        GameObject item = Instantiate(taskListItemPrefab);

        item.transform.SetParent(content, false);
        item.SetActive(true);


        TaskListObj itemObject = item.GetComponent<TaskListObj>();
        int index = 0;
        if (taskListObjects.Count > 0)
            index = taskListObjects.Count - 1;
        itemObject.SetObjectInfo(name, index);
        taskListObjects.Add(itemObject);
        TaskListObj temp = itemObject;
        itemObject.GetComponent<Toggle>().onValueChanged.AddListener(delegate {CheckItem(temp); });
    }

    void CheckItem(TaskListObj item)
    {
        taskListObjects.Remove(item);
        Destroy(item.gameObject);
    }
}
