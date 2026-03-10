using UnityEngine;
using UnityEngine.SceneManagement;

//defines the Status in wich the game run
public enum Status
{
    gamePaused,
    gameRunning
}

public class MenuManager : MonoBehaviour
{
    [Header("Status")]
    public Status status; //Gets the status in the class


    //public static event Action OnPowerUp;

    public static MenuManager Instance;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }
        Instance = this;
    }

    private void Start()
    {       
        //status = Status.GamePaused; //at the start of the game set the status to paused
    }


        //Setting the TimeScales for the game for when is paused and when is running
        //olso deactivate/activate the UI in the corrispective status
    private void Update()
    {
        if (status == Status.gamePaused)
        {
            Time.timeScale = 0;        
        }
        else
        {
            Time.timeScale = 1;
        }
    }
    //When the Play Botton is Clicked, Change the scene to the main level and set the State to Running
    public void StartGame()
    {
        SceneManager.LoadScene("MainLevel"); 
        status = Status.gameRunning;
    }

    //When the Main menu Botton is Clicked sets the Status to paused and change the Sche to the main menu Scene
    public void BackToMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    //When the Quit Botton is Cliked, exit the Game (only in build)
    public void ExitGame()
    {
        Application.Quit();
    }
}
