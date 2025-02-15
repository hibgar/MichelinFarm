using UnityEngine;

public class UserDataManager : MonoBehaviour
{
    private UserData userData;

    void Start()
    {
        //userData = FileStorage.LoadData(); // Load user data when the game starts
        //Debug.Log("Loaded Task List: " + string.Join(", ", userData.taskList));
    }

    public void AddItem(string item)
    {
        //userData.taskList.Add(item);
        //FileStorage.SaveData(userData); // Save after adding
        Debug.Log("Added: " + item);
    }
}
