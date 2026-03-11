using UnityEngine;

public class HealthManager : MonoBehaviour, IDamageable
{
    //Parent class used hereditated in Enemy.cs & Playerbase.cs to manage the Helth systems

    //gets the HP Values for the Player and the Enemy
    [Header("Health Variables")]
    public float currentHP;
    public int maxHP;
    public virtual void TakeDamage(float dmg)
    { currentHP -= dmg; } //When the Entity Takes damage it'll substract the damage given 
    public virtual void Despawn()
    { Debug.Log("morto"); } //When the currentHealth variable is equal to 0 the Entity will despawn
}
