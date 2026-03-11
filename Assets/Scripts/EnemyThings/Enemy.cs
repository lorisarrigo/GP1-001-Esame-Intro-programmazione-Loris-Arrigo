using System;
using UnityEngine;

public class Enemy : HealthManager
{
    //The class used to manage the Health and the Attack system of the Enemies

    //Gets the stats and the components needed


    [Header("Other Enemy stats")]
    [SerializeField] float speed;
    public float dmg;

    [Header("Money stats")]
    [SerializeField] int Money;
    public static event Action <int> OnEnemyKilled; //event Action used to Add Money when the Enemy is Killed

    Rigidbody rb;
    EnemySpawner enemSpawn;
    GameObject colobj;
    
    private void Awake()
    {
        enemSpawn = GetComponentInParent<EnemySpawner>();
        rb = GetComponent<Rigidbody>();
    }
    //When spawned Sets the Enemy HP & add the damage so it can be changed when Upgeraded
    private void OnEnable()
    {
        currentHP = maxHP;
        dmg += enemSpawn.totalDmg; 
    }

    private void FixedUpdate()
    {
        ChargeToPlayer();
    }

    //Move the enemy to the left
    private void ChargeToPlayer()
    {
        rb.linearVelocity = (new Vector3(-1, transform.position.y, transform.position.z) * speed) * Time.fixedDeltaTime;
    }

    //When it touch the Player Hitbox it'll damage it and then Destory itself
    private void OnTriggerEnter(Collider other)
    {
        colobj = other.gameObject;
        if (other.CompareTag("Player"))
        {
            EnemyAttack();
            Destroy(gameObject);
        }
    }

    /*When the Enemy hit the PlayerBase:
     * recalls the Interface and the "Player" script;
     * comunicate the amount of damage that the Player has to take;
     * check if the Player had finished it's life,
     * if it's true it'll comunicate at the Interface to recall the Despawn function for the Player
     */
    public void EnemyAttack()
    {
        if (colobj == null) return;
        colobj.TryGetComponent(out IDamageable damageable);
        colobj.TryGetComponent(out PlayerBase player);
        
        if (damageable == null) return;
        damageable.TakeDamage(dmg);


        if (player == null) return;
        if (player.currentHP <= 0)
        {
            damageable.Despawn();
        }
    }
    /*When the bullet finds out that the Enemy has no life:
     * Invoke the Event to Add Money to the Counter;
     * add 1 to the kill Counter;
     * Destroy the Enemy.
    */
    public override void Despawn()
    {
        OnEnemyKilled?.Invoke(Money);
        UIManager.Instance.killCounter++;
        Destroy(gameObject);
    }    
}
