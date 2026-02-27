using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Status
{
    GamePaused,
    GameRunning
}

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;
    [Header("Status")]
    public Status status;

    [Header("Money")]
    public float StartingMoney;
    public float money;
    public static event Action OnMoneyAdded;

    private void OnEnable()
    {
        Enemy.OnEnemyKilled += AddMoney;
    }

    private void OnDisable()
    {
        Enemy.OnEnemyKilled -= AddMoney;
    }

    private void AddMoney(int Money)
    {
        money += Money;
        OnMoneyAdded?.Invoke();
    }

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
        money = StartingMoney;
        //status = Status.GamePaused; //at the start of the game set the status to paused
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

    public void LostGame()
    {
        status = Status.GamePaused; //Set the status to paused
    }

    //exit the Game
    public void ExitGame()
    {
        Application.Quit();
    }
}
