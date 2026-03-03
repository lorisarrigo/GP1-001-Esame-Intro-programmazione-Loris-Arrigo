using System.Collections;
using UnityEngine;

public class PlayerBase : MonoBehaviour, IDamageable
{
    [Header("Player Health")]
    public int maxHealth;
    public float currentHealth;

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

    //set the health of the Enemy to the Max
    private void Start()
    {
        currentHealth = maxHealth;
    }

    //takes the damage comunicated by the Interface
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
    }

    //Opens the GameOver Screen
    public void Despawn()
    {
        MenuManager.Instance.LostGame();
    }
}