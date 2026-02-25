using System.Collections.Generic;
using UnityEngine;

public class OverlapDam : MonoBehaviour
{
    Rigidbody rb;
    GameObject Colobj;
    Turrets tur;


    [SerializeField] Transform Top;
    [SerializeField] Transform Bottom;
    [SerializeField] float radius;
    [SerializeField] LayerMask layer;
    [SerializeField] Vector3 TopPos;
    [SerializeField] Vector3 BottomPos;

    [SerializeField] Mesh capsule;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        tur = GetComponentInParent<Turrets>();
    }

    private void FixedUpdate()
    {
        Move();
    }
    private void Update()
    {
        TopPos = new Vector3(Top.transform.position.x, Top.transform.position.y, Top.transform.position.z);
        BottomPos = new Vector3(Top.transform.position.x, Top.transform.position.y, Top.transform.position.z);
        AreaDam();
    }
    private void Move()
    {
        rb.linearVelocity = (new Vector3(transform.position.x, transform.position.y, 1) * tur.BSpeed) * Time.fixedDeltaTime; //Move the enemy to the left
    }
    //private void OnTriggerEnter(Collider other)
    //{
    //    Colobj = other.gameObject;
    //    if (other.CompareTag("Enemy") || other.CompareTag("Despawner"))
    //    {
    //        AreaDam();
    //        Destroy(gameObject);
    //    }
    //}
    //private void OnCollisionEnter(Collision collision)
    //{
    //    Debug.Log("Enemy Colliso");
    //    AreaDam();
    //}
    public void AreaDam()
    {
        Collider[] enemyCol = Physics.OverlapCapsule(TopPos, BottomPos, radius, layer/*, trigger*/);
        List<IDamageable> damEnem = new();

        foreach (var enemyc in enemyCol)
        {
            if(enemyc.TryGetComponent(out IDamageable damageable) && !damEnem.Contains(damageable))
            {
                damEnem.Add(damageable);
                damageable.TakeDamage(tur.DamagePerHit);
                Debug.Log("Enemy colpito");
            }
            if(enemyc.TryGetComponent(out Enemy enemy) && enemy.CurrentHealth <= 0 && enemy != null)
            { damageable.Despawn(); }
            //if (Colobj == null) return;
            //Colobj.TryGetComponent(out IDamageable damageable);
            //Colobj.TryGetComponent(out Enemy enemies);
            //if (damageable == null) return;
            //damageable.TakeDamage(tur.DamagePerHit);

            //if (enemies == null) return;
            //if (enemies.CurrentHealth <= 0)
            //{
            //    damageable.Despawn();
            //}
        }
    }
    //disegna l'Area d'attacco
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireMesh(capsule, transform.position, transform.rotation, transform.localScale);
    }

}

