using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class Restart : MonoBehaviour {
    public Button restart;
    private void Start()
    {
        restart.onClick.AddListener(TaskOnClick);
    }
    void TaskOnClick()
    {
        Debug.Log("res");
        Application.LoadLevel("SampleScene");
    }
}
