using UnityEngine;
using UnityEngine.SceneManagement;

public enum Status
{
    GamePaused,
    GameRunning
}

public class MenuManager : MonoBehaviour
{
    [Header("Status")]
    public Status status;


    private void Start()
    {
        status = Status.GamePaused; //at the start of the game set the status to paused
    }

    private void Update()
    {
        //Setting the TimeScales for the game for when is paused and when is running
        if (status == Status.GamePaused)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
    public void StartGame()
    {
        SceneManager.LoadScene("MainLevel"); //Change the scene to the main level
        status = Status.GameRunning; //Set the status to running
    }

    //exit the Game
    public void ExitGame()
    {
        Application.Quit();
    }
}
