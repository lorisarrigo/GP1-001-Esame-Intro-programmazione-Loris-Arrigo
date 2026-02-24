using UnityEngine;

public class AreaBullet : MonoBehaviour
{
    GameObject Colobj;
    Rigidbody rb;
    
    Turrets tur;

    //[SerializeField] Collider Area;

    private void OnTriggerEnter(Collider other)
    {
        Colobj = other.gameObject;
        if (other.CompareTag("Enemy") )
        {
            AreaDam();
        }
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        tur = GetComponentInParent<Turrets>();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        rb.linearVelocity = new Vector3(transform.position.x, transform.position.y, 1) * tur.BSpeed; //Move the enemy to the left
    }
    public void AreaDam()
    {
        if (Colobj == null) return;
        Colobj.TryGetComponent(out IDamageable damageable);
        Colobj.TryGetComponent(out Enemy enemy);

        if (damageable == null) return;
        damageable.TakeDamage(tur.DamagePerHit);

        if (enemy == null) return;
        if (enemy.CurrentHealth <= 0)
        {
            damageable.Despawn();
        }
    }
}