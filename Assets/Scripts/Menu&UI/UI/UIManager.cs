using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("UI")]
    [SerializeField] GameObject inGameUI;

    [Header("Health")]
    [SerializeField] Image PlayerHpbar;

    [Header("Money")]
    [SerializeField] TMP_Text moneyCounter;
    public int StartingMoney;

    [Header("Tur prize")]
    [SerializeField] TMP_Text NormalTur;
    public int NormalPrize;
    [SerializeField] TMP_Text MachineGunTur;
    public int MGPrize;
    [SerializeField] TMP_Text AreaTur;
    public int AreaPrize;

    private void OnEnable()
    {

        MenuManager.OnMoneyAdded += UpdateCounter;
    }
    private void OnDisable()
    {
        MenuManager.OnMoneyAdded -= UpdateCounter;
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
        MenuManager.Instance.money = StartingMoney;
        UpdateCounter();
    }

    private void Update()
    {
        if (MenuManager.Instance.status == Status.GameRunning)
            inGameUI.SetActive(true);
        else
            inGameUI.SetActive(false);

        PlayerHpbar.fillAmount = (float)PlayerBase.Instance.CurrentHealth / (float)PlayerBase.Instance.MaxHealth;

    }

    public void UpdateCounter()
    {
        moneyCounter.text = MenuManager.Instance.money.ToString();
        NormalTur.text = NormalPrize.ToString();
        MachineGunTur.text = MGPrize.ToString();
        AreaTur.text = AreaPrize.ToString();
    }
}
