using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("In Game UI")]
    [SerializeField] GameObject inGameUI;
    [SerializeField] TMP_Text moneyCounter;
    [SerializeField] Image PlayerHpbar;

    private void OnEnable()
    {
        MenuManager.OnMoneyAdded += UpdateCounter;
    }
    private void OnDisable()
    {
        MenuManager.OnMoneyAdded -= UpdateCounter;
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
    }
}
