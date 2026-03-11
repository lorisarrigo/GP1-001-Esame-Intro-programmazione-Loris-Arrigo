using UnityEngine;

public class Bullets : BulletManager
{
    //The class to manage the base bullet Type

    //gets the GameObject so it can take it's collider
    GameObject colobj;

    //When the Bullet hit a Collider that has the tag Enemy,
    //it'll start the Damage Function and destroy the Bullet
    private void OnTriggerEnter(Collider other)
    {
        colobj = other.gameObject;
        if (other.CompareTag("Enemy"))
        {
            DmgOnHit();
            Destroy(gameObject);
        }
    }

    /*When the bullet hit the Collider, recalls the Interface and the "Enemy" script;
     *then comunicate at the Interface the amount of Damage that the Enemy has to take
     *and checks if the Enemy hasn't any life,
     *if it's true, it'll comunicate at the Interface to Recall the Despawn function for the Enemy Collided
    */
    public void DmgOnHit()
    {
        if(colobj==null) return;
        colobj.TryGetComponent(out IDamageable damageable);
        colobj.TryGetComponent(out Enemy enemy);

        if (damageable == null) return;
        damageable.TakeDamage(tur.dmgPerHit);

        if (enemy == null) return;
        if(enemy.currentHP <= 0)
        {
            damageable.Despawn();
        }
    }
}
