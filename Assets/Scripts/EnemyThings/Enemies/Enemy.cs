using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Health settings")]
    [SerializeField] float MaxHealth;
    [SerializeField] float CurrentHealth;

    [Header("other settings")]
    [SerializeField] float Speed;
    [SerializeField] float DamagePerHit;
    [SerializeField] int Money;

}
