using UnityEngine;

public class Bullets : MonoBehaviour
{
    Rigidbody rb;
    GameObject Colobj;
    Turrets tur;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        tur = GetComponentInParent<Turrets>();
    }

    private void FixedUpdate()
    {
        Move(); 
    }

    private void Move()
    {
        if (tur.transform.rotation.y == 0)
        { rb.linearVelocity = (new Vector3(transform.position.x, transform.position.y, -1) * tur.BSpeed) * Time.fixedDeltaTime; }
        else
        { rb.linearVelocity = (new Vector3(transform.position.x, transform.position.y, 1) * tur.BSpeed) * Time.fixedDeltaTime; }
    }

    private void OnTriggerEnter(Collider other)
    {
        Colobj = other.gameObject;
        if (other.CompareTag("Enemy") || other.CompareTag("Despawner"))
        {
            NormalDam();
            Destroy(gameObject);
        }
    }

    public void NormalDam()
    {
        if(Colobj==null) return;
        Colobj.TryGetComponent(out IDamageable damageable);
        Colobj.TryGetComponent(out Enemy enemy);

        if (damageable == null) return;
        damageable.TakeDamage(tur.DamagePerHit);

        if (enemy == null) return;
        if(enemy.CurrentHealth <= 0)
        {
            damageable.Despawn();
        }
    }
}
