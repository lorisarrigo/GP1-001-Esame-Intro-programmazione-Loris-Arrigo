using System.Collections;
using UnityEngine;

public class PlayerBase : MonoBehaviour, IDamageable
{
    [SerializeField] int MaxHealth;
    public int CurrentHealth;

    public static PlayerBase Instance;
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
        CurrentHealth = MaxHealth;
    }
    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
    }
    public void Despawn()
    {
        Debug.Log("son stato richiamato");
        Kill();
    }
    public void Kill()
    {
        StartCoroutine(Death());
    }

    IEnumerator Death()
    {
        yield return new WaitForSeconds(0.2f);
        MenuManager.Instance.LostGame();
    }
}
