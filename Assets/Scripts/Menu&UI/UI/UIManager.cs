using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    //Gets the Menus and the kill Counter
    [Header("Menus")]
    //public GameObject PauseMenu;
    public GameObject gameOverScreen;
    public int enemyKillCounter;
    [SerializeField] TMP_Text killCounter;

    [Header("UI")]
    [SerializeField] GameObject inGameUI; //Gets the UI used when the game is running

    [Header("Health")]
    [SerializeField] Image playerHpbar; //Gets the HelthBar

    [Header("Money")]
    [SerializeField] TMP_Text moneyCounter; //Gets the Money counter
    public int startingMoney;
    public int money;

    [Header("turrets")]
    [SerializeField] GameObject normalPrefab;
    [SerializeField] GameObject machineGunPrefab;
    [SerializeField] GameObject areaPrefab;

    public GameObject selectedTur; 

    //Gets the prizes of the Turrets Buttons
    [Header("Tur prizes")]
    [SerializeField] TMP_Text normalTur; 
    public int normalPrize;
    [SerializeField] TMP_Text machineGunTur;
    public int machinegunPrize;
    [SerializeField] TMP_Text areaTur;
    public int areaPrize;

    //when the Enemy gets Killed Add money to the Counter
    private void OnEnable()
    {
        Enemy.OnEnemyKilled += AddMoney;
    }
    private void OnDisable()
    {
        Enemy.OnEnemyKilled -= AddMoney;
    }

    public static UIManager Instance;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }
        Instance = this;
    }

    //At the Start of the Game, Set the money to the Starting Money and Updates the Counter
    private void Start()
    {
        money = startingMoney;
        UpdateCounter();
    }

    //In every frame Updates the HealthBar of the Player by dividing the Current by the MaxHealth
    private void Update()
    {
        playerHpbar.fillAmount = PlayerBase.Instance.currentHealth / (float)PlayerBase.Instance.maxHealth; //the max is translated to float so that the bar can be defilled properly
        if (MenuManager.Instance.status == Status.gamePaused)
        {
            inGameUI.SetActive(false);
        }
        else
        {
            inGameUI.SetActive(true);
        }
    }

    //Adds Money to the Counter when an Enemy gets killed, based on the Money given by the Enemy and Updates the Counter
    public void AddMoney(int Money)
    {
        money += Money;
        UpdateCounter();
    }
    //Update the Counters in the start, and the Money Every time an Enemy is killed
    public void NormalTurret()
    {
        selectedTur = normalPrefab;
        Debug.Log(selectedTur);
    }
    public void MachineGun()
    {
        selectedTur = machineGunPrefab;
        Debug.Log(selectedTur);
    }
    public void AreaTurret()
    {
        selectedTur = areaPrefab;
        Debug.Log(selectedTur);
    }

    public void UpdateCounter()
    {
        moneyCounter.text = "Money: " + money.ToString();
        normalTur.text = normalPrize.ToString();
        machineGunTur.text = machinegunPrize.ToString();
        areaTur.text = areaPrize.ToString();
    }

    //When the Player dies, Set the Status to pause, Update the Kill Counter, and Activate the GameOverScreen
    public void LostGame()
    {
        MenuManager.Instance.status = Status.gamePaused; //Set the status to paused
        killCounter.text = "Enemies Killed: " + enemyKillCounter.ToString();
        gameOverScreen.SetActive(true);
    }

}
