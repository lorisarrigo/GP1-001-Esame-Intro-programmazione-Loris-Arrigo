using UnityEngine;

public class Bullets : MonoBehaviour
{
    //gats the components needed
    Rigidbody rb;
    Turrets tur;
    GameObject Colobj;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        tur = GetComponentInParent<Turrets>();
    }

    private void FixedUpdate()
    {
        Move(); 
    }

    //If the Turret is rotated in y of 0°, move the Bullet in Z axis by multiplying the meters per seconds;
    //in any other rotation, it'll move it in the opposite direction
    private void Move()
    {
        if (tur.transform.rotation.y == 0)
        { rb.linearVelocity = (new Vector3(transform.position.x, transform.position.y, -1) * tur.bSpeed) * Time.fixedDeltaTime; }
        else
        { rb.linearVelocity = (new Vector3(transform.position.x, transform.position.y, 1) * tur.bSpeed) * Time.fixedDeltaTime; }
    }

    //When the Bullet hit a Collider that has the tag Enemy, it'll start the Damage Function and destroy the Bullet
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
        //When the bullet hit the Collider recalls the Interface and the "Enemy" script
        if(Colobj==null) return;
        Colobj.TryGetComponent(out IDamageable damageable);
        Colobj.TryGetComponent(out Enemy enemy);

        //then it'll comunicate at the Interface the amount of Damage that the Enemy has to take
        if (damageable == null) return;
        damageable.TakeDamage(tur.damagePerHit);

        //and checks if the Enemy hasn't any life,
        //if it's true, it'll comunicate at the Interface to Recall the Despawn function for the Enemy Collided
        if (enemy == null) return;
        if(enemy.currentHealth <= 0)
        {
            damageable.Despawn();
        }
    }
}
