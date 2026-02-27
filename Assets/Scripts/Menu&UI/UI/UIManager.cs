using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("In Game UI")]
    [SerializeField] GameObject inGameUI;
    [SerializeField] TMP_Text moneyCounter;

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
    }

    public void UpdateCounter()
    {
        moneyCounter.text = MenuManager.Instance.money.ToString();
    }
}
