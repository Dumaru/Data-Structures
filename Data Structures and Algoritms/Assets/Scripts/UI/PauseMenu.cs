using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void HandleResumeButtonClickedEvent()
    {
        Time.timeScale = 1;
        Destroy(this.gameObject);
    }
    public void HandleQuitButtonClickedEvent()
    {
        Application.Quit();
    }
}
