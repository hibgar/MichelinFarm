using System.Collections.Generic;

[System.Serializable]
public class UserData
{
    public string userId;
    public List<TaskListManager.TasklistItem> taskList = new List<TaskListManager.TasklistItem>();

    public UserData()
    {
        taskList = new List<TaskListManager.TasklistItem>(); // ✅ Ensure list is initialized
    }
}
