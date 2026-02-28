using System.Collections.Generic;
using UnityEngine;

public class OverlapDam : MonoBehaviour
{
    Rigidbody rb;
    Turrets tur;
    [SerializeField] Mesh box;
    [SerializeField] LayerMask layer;
      
    List<IDamageable> damEnem = new();

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
        AreaDam();
    }
    private void Move()
    {
        if (tur.transform.rotation.y == 0)
        { rb.linearVelocity = (new Vector3(transform.position.x, transform.position.y, -1) * tur.BSpeed) * Time.fixedDeltaTime; }
        else
        { rb.linearVelocity = (new Vector3(transform.position.x, transform.position.y, 1) * tur.BSpeed) * Time.fixedDeltaTime; }
    }
    public void AreaDam()
    {
        
        Collider[] enemyCol = Physics.OverlapBox(transform.position, transform.lossyScale, Quaternion.identity, layer);

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
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("Despawner"))
        {
            Debug.Log("Entrato in contatto");
            Destroy(gameObject);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireMesh(box, transform.position, transform.rotation, transform.lossyScale);
    }
}

