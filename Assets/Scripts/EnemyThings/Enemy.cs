using System;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    //Gets the stats and the components needed
    [Header("Enemy Health")]
    [SerializeField] int maxHealth;
    public float currentHealth;

    [Header("Other Enemy stats")]
    [SerializeField] float speed;
    public float damagePerHit;

    [Header("Money stats")]
    [SerializeField] int enemyMoney;
    public static event Action <int> OnEnemyKilled; //event Action used to Add Money when the Enemy is Killed

    Rigidbody rb;
    EnemySpawner enemSpawn;
    GameObject colobj;
    
    private void Awake()
    {
        enemSpawn = GetComponentInParent<EnemySpawner>();
    }
    //Whene spawned add the total Damage to the base Damage
    private void OnEnable()
    {
        damagePerHit += enemSpawn.totalDam;
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentHealth = maxHealth;
    }

    private void FixedUpdate()
    {
        Move();
    }

    //Move the enemy to the left
    private void Move()
    {
        rb.linearVelocity = (new Vector3(-1, transform.position.y, transform.position.z) * speed) * Time.fixedDeltaTime;
    }

    //When it touch the Player Hitbox it'll damage it and then Destory itself
    private void OnTriggerEnter(Collider other)
    {
        colobj = other.gameObject;
        if (other.CompareTag("Player"))
        {
            Hit();
            Destroy(gameObject);
        }
    }

    public void Hit()
    {
        //When the Enemy it the Player recalls the Interface and the "Player" script
        if (colobj == null) return;
        colobj.TryGetComponent(out IDamageable damageable);
        colobj.TryGetComponent(out PlayerBase player);

        //then it'll comunicate the amount of damage that the Player has to take
        if (damageable == null) return;
        damageable.TakeDamage(damagePerHit);

        //and check if the Player had finished it's life,
        //if it's true it'll comunicate at the Interface to start the Despawn function for the Player
        if (player == null) return;
        if (player.currentHealth <= 0)
        {
            damageable.Despawn();
        }
    }

    //subtract the damage, that the Interface gets from the Bullet collided, to the Health of the Enemy
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
    }

    //When the bullet finds out that the Enemy has no life,
    //Invoke the Event to Add Money to the Counter, add 1 to the kill Counter and the Enemy will Destroy itself
    public void Despawn()
    {
        OnEnemyKilled?.Invoke(enemyMoney);
        UIManager.Instance.enemyKillCounter++;
        Destroy(gameObject);
    }    
}
