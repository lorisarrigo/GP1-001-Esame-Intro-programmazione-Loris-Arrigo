using TMPro;
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

    [Header("UI")]
    [SerializeField] GameObject inGameUI; //Gets the UI used when the game is running

    //Gets the Menus and the kill Counter
    [Header("Menus")]
    //public GameObject PauseMenu;
    public GameObject gameOverScreen;
    public int enemyKillCounter;
    [SerializeField] TMP_Text killCounter;


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
            inGameUI.SetActive(false);
        }
        else
        {
            Time.timeScale = 1;
            inGameUI.SetActive(true);
        }

    }
    //When the Play Botton is Clicked, Change the scene to the main level and set the State to Running
    public void StartGame()
    {
        SceneManager.LoadScene("MainLevel"); 
        status = Status.gameRunning;
    }

    //When the Player dies, Set the Status to pause, Update the Kill Counter, and Activate the GameOverScreen
    public void LostGame()
    {
        status = Status.gamePaused; //Set the status to paused
        killCounter.text = "Enemies Killed: " + enemyKillCounter.ToString();
        gameOverScreen.SetActive(true);
    }

    //When the Main menu Botton is Clicked sets the Status to paused and change the Sche to the main menu Scene
    public void BackToMainMenu()
    {
        status = Status.gamePaused;
        SceneManager.LoadScene("Menu");
    }

    //When the Quit Botton is Cliked, exit the Game (only in build)
    public void ExitGame()
    {
        Application.Quit();
    }
}
