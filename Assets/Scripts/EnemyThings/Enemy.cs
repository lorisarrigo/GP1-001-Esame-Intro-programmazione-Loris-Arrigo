using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    [Header("Health settings")]
    [SerializeField] float MaxHealth;
    [SerializeField] float CurrentHealth;

    public static event System.Action<int> OnEnemyKilled;

    [Header("other settings")]
    [SerializeField] float Speed;
    [SerializeField] int DamagePerHit;
    [SerializeField] int Money;

    Rigidbody rb;
    GameObject Colobj;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        CurrentHealth = MaxHealth;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        rb.linearVelocity = new Vector3(-1, transform.position.y, transform.position.z) * Speed; //Move the enemy to the left
    }
    private void OnTriggerEnter(Collider other)
    {
        Colobj = other.gameObject;
        if (other.CompareTag("Player"))
        {
            Hit();
            Destroy(gameObject);
        }
    }

    public void Hit()
    {
        if (Colobj == null) return;
        Colobj.TryGetComponent(out IDamageable damageable);
        Colobj.TryGetComponent(out PlayerBase player);

        if (damageable == null) return;
        damageable.TakeDamage(DamagePerHit);

        if (player == null) return;
        if (player.CurrentHealth <= 0)
        {
            Debug.Log("player is dead");
            damageable.Despawn();
        }
    }
    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
    }

    public void Despawn()
    {
        if (CompareTag("Bullet"))
        { 
            StartCoroutine(TimeBeforeDespawn());
        }
    }
    
    IEnumerator TimeBeforeDespawn()
    {
        yield return new WaitForSeconds(2f);
        OnEnemyKilled?.Invoke(Money);
        Destroy(gameObject);
    }
}
