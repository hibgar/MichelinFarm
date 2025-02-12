using UnityEngine;
using UnityEngine.UI;

public class TaskListObj : MonoBehaviour
{

    public string objName;
    public int index;

    private Text itemText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        itemText = GetComponentInChildren<Text>();
        itemText.text = objName;
    }

    public void SetObjectInfo(string name, int index)
    {
        this.objName = name;
        this.index = index;
    }
}
