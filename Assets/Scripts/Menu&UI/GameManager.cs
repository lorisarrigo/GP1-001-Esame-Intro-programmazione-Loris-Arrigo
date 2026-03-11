using UnityEngine;
using UnityEngine.SceneManagement;

//Defines the Status in which the game run
public enum Status
{
    gamePaused,
    gameRunning
}

public class GameManager : MonoBehaviour
{
    //Gets the status in the class
    [Header("Status")]
    public Status status;

    //Singleton used only in UIManager because Getting the component doesn't work
    public static GameManager Instance;
    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(this);
            return;
        }
        Instance = this;
    }
    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "Menu")
        {
            status = Status.gamePaused;
        }
        if (SceneManager.GetActiveScene().name == "MainLevel")
        {
            status = Status.gameRunning;
        }
    }
    /*in order of reading:
     *1- Setting the TimeScales for the game for when is paused and when is running & deactivate/activate the UI in the corrispective status
     *2- Open/Close the Pause Screen
    */

    private void Update()
    {
        //1:
        if (status == Status.gamePaused)
        {
            Time.timeScale = 0;
            UIManager.Instance.inGameUI.SetActive(false);
        }
        else
        {
            Time.timeScale = 1;
            UIManager.Instance.inGameUI.SetActive(true);
        }

        //2:
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (status == Status.gameRunning)
                OpenPause();
            else
                ClosePause();
        }
    }
    //Open & Close Pause Screen function
    public void OpenPause()
    {
        status = Status.gamePaused;
        UIManager.Instance.pauseScreen.SetActive(true);
    }
    public void ClosePause()
    {
        status = Status.gameRunning;
        UIManager.Instance.pauseScreen.SetActive(false);
    }
    //When the Play Botton is Clicked, Change the scene to the main level and set the State to Running (olso used in the Replay Button to reload the Scene)
    public void StartGame()
    {
        SceneManager.LoadScene("MainLevel");
    }
    //When the Main menu Botton is Clicked sets the Status to paused and change the Scene to the main menu Scene
    public void BackToMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    //When the Quit Botton is Cliked, exit the Game (only in build)
    public void Exit()
    {
        Application.Quit();
    }
}
