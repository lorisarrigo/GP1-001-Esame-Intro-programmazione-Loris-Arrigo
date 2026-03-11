using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    //Gets the Menus and the kill Counter
    [Header("Menus")]
    public GameObject inGameUI; //Gets the UI used when the game is running
    public GameObject pauseScreen;
    public GameObject gameOverScreen;
    public int killCounter;
    [SerializeField] TMP_Text killCounterTxt;

    //Gets the HelthBar
    [Header("Health")]
    [SerializeField] Image playerHpBar; 

    //Gets the money counter and money stats to update the counter 
    [Header("Money")]
    [SerializeField] TMP_Text moneyCounter;
    public int startingMoney;
    public int money;

    //Gets the prices of the Turrets Buttons
    [Header("Bottons prizes")]
    [SerializeField] Button nBTN;
    [SerializeField] TMP_Text normalTur; 
    public int normalPrice;
    [Header("")]
    [SerializeField] Button mgBTN;
    [SerializeField] TMP_Text machineGunTur;
    public int machinegunPrice;
    [Header("")]
    [SerializeField] Button aBTN;
    [SerializeField] TMP_Text areaTur;
    public int areaPrice;
    [Header("")]
    public int selectedPrice;

    //gets the Prefab and store it so the Game knows what to spawn
    [Header("Turrets")]
    [SerializeField] GameObject normalPrefab;
    [SerializeField] GameObject machineGunPrefab;
    [SerializeField] GameObject areaPrefab;

    public GameObject selectedTur;

    //Stats screen things that appear when the Player clicks the turrets buttons
    [Header("Stats Scrren")]
    [SerializeField] GameObject statsScreen;
    [Header("")]
    [SerializeField] TMP_Text turName;
    [SerializeField] TMP_Text turDmg;
    [SerializeField] TMP_Text turRate;
    [SerializeField] TMP_Text turPU;
    [Header("")]
    [SerializeField] GameObject normalImg;
    [SerializeField] GameObject machinegunImg;
    [SerializeField] GameObject areaImg;

    //Slot variables that the game use to store how many slot are available
    [Header("Slots")]
    [SerializeField] int availableSlots;
    public int usedSlots;
    public bool buildMode = false; //Build mode that activate when a turret it's selected, it deactivate when a slot is clicked

    //Variables used to manage the timer
    [Header("Timer")]
    [SerializeField] TMP_Text timerTxt;
    [SerializeField] TMP_Text EndTimer;
    float timer, minutes, seconds;
    //[SerializeField] Color uninteractable;

    /*Singleton used in:
     * Enemy.cs: for the KillCounter
     * Placement.cs: for the OnPointerClick 
     * Turrets.cs: for Updating the powerUps prices
    */
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
    //when the Enemy gets Killed, add money to the Counter
    private void OnEnable()
    {
        Enemy.OnEnemyKilled += AddMoney;
    }
    private void OnDisable()
    {
        Enemy.OnEnemyKilled -= AddMoney;
    }


    //At the Start of the Game, Gets the MenuManager, Set the money & Updates the Counter
    private void Start()
    {
        money = startingMoney;
        UpdateCounter();
    }

    /*in order of reading:
     *1- calculate how much needs to deffil the bar of the HPBar,
     *2- calculate the flowing of time while the Game is Running
     *3- deactivate the Stats Screen when the Left Mouse Button is Cliccked
    */
    private void Update()
    {
        //1:
        playerHpBar.fillAmount = PlayerBase.Instance.currentHP / (float)PlayerBase.Instance.maxHP;
        
        //2:
        timer += Time.deltaTime;

        minutes = Mathf.Floor(timer / 60);
        seconds = Mathf.FloorToInt(timer % 60);
        timerTxt.text = string.Format("{0:0}:{1:00}", minutes, seconds);

        //3:
        if (Input.GetMouseButton(0))
            statsScreen.SetActive(false);
    }
    //Update the Counters in the start, and the Money every time an Enemy is killed
    public void UpdateCounter()
    {
        moneyCounter.text = "Money: " + money.ToString();
        normalTur.text = normalPrice.ToString();
        machineGunTur.text = machinegunPrice.ToString();
        areaTur.text = areaPrice.ToString();
    }

    //Adds Money to the Counter when an Enemy gets killed, based on the Money given by the Enemy and Updates the Counter
    public void AddMoney(int Money)
    {
        money += Money;
        UpdateCounter();
    }

    /*The next 3 Functions are used in the Buttons to select the Turret:
     *1- opens the Stats Screen & Update the stats TMP;
     *2- activate the corresponding turret Image;
     *3- sets the informations needed to place the selected Turret & enters the Builder mode
     */

    public void NormalTurret()
    {
        //1:
        statsScreen.SetActive(true);
        turName.text = "Normal";
        turDmg.text = "Damage: 3";
        turRate.text = "Rate: 5";
        turPU.text = "PowerUP: Damage X2";

        //2:
        normalImg.SetActive(true);
        machinegunImg.SetActive(false);
        areaImg.SetActive(false);

        //3:
        selectedPrice = normalPrice;
        selectedTur = normalPrefab;
        if (money >= selectedPrice && usedSlots < availableSlots)
        {
            //nBTN.interactable = true;
            buildMode = true;
        }
    }
    public void MachineGun()
    {
        //1:
        statsScreen.SetActive(true);
        turName.text = "Machine Gun";
        turDmg.text = "Damage: 6";
        turRate.text = "Rate: 3";
        turPU.text = "PowerUP: Rate - 0,5";

        //2:
        machinegunImg.SetActive(true);
        normalImg.SetActive(false);
        areaImg.SetActive(false);

        //3:
        selectedPrice = machinegunPrice;
        selectedTur = machineGunPrefab;
        if (money >= selectedPrice && usedSlots < availableSlots)
        {
            //mgBTN.interactable = true;
            buildMode = true;
        }
    }
    public void AreaTurret()
    {
        //1:
        statsScreen.SetActive(true);
        turName.text = "Area";
        turDmg.text = "Damage: 9";
        turRate.text = "Rate: 9";
        turPU.text = "PowerUP: Area x 1,5";

        //2:
        areaImg.SetActive(true);
        normalImg.SetActive(false);
        machinegunImg.SetActive(false);

        //3:
        selectedPrice = areaPrice;
        selectedTur = areaPrefab;
        if (money >= selectedPrice && usedSlots < availableSlots)
        {
            //aBTN.interactable = true;
            buildMode = true;
        }
    }


    //When the Player dies, Pause the game, Update the Kill Counter and Activate the GameOver Screen
    public void LostGame()
    {
        GameManager.Instance.status = Status.gamePaused;
        gameOverScreen.SetActive(true);
        killCounterTxt.text = "Enemies Killed: " + killCounter.ToString();
        EndTimer.text = timerTxt.text;
        if(killCounter == 0)
        {
            killCounterTxt.text = "Enemies Killed: " + 0;
        }
    }
}
