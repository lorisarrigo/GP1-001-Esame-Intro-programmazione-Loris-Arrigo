using System.Collections.Generic;
using UnityEngine;

public class OverlapDam : BulletManager
{
    
    //class used to manage the Overlap kimnd of bullet;
    [Header("Damage zone")]
    [SerializeField] Transform Area; //The Transform of thegameobject that needs the Overlap
    Vector3 scale; //the custom Scale that will modify only when it'll PowerUp
    [SerializeField] Mesh box; //a Mesh used to identify the AOD

    [SerializeField] LayerMask layer; //get the Layer that needs to take damage

    List<IDamageable> Enem = new(); //The List used to check if the Enemy has already took the damage

    private void Update()
    {
        AreaDmg();
    }

    public void AreaDmg()
    {
        //sets the scale so that it can be Upgrated
        scale = new(
                Area.lossyScale.x/2 * tur.totMultilier,
                Area.lossyScale.y/2 * tur.totMultilier,
                Area.lossyScale.z * tur.totMultilier
            );
        
        //Create the OverlapBox, with the caratteristics of the Area, in which the Enemies need to enter to take Damage
        Collider[] enemyCol = Physics.OverlapBox(transform.position, scale, Quaternion.identity, layer);

        //Foreach Enemy that enters the OverlapBox 
        foreach (var enemyc in enemyCol)
        {
            //check if there is the Interface & if the Interface isn't already called;
            //then it'll Add the Interface to the List and comunicate the damage to the Interface
            if (enemyc.TryGetComponent(out IDamageable damageable) && !Enem.Contains(damageable))
            {
                Enem.Add(damageable);
                damageable.TakeDamage(tur.dmgPerHit);
            }

            //check if there is an Enemy, if it's health is below or equal to 0 and if it isn't null,
            //so that it can comunicate at the Interface to Recall the Despawn function for the Enemies inside the Overlap
            if (enemyc.TryGetComponent(out Enemy enemy) && enemy.currentHP <= 0 && enemy != null)
            { damageable.Despawn(); }
        }
    }

    //if an Object whit the tag "Enemy" has made contact whit the body, it'll Destroy the Bullet
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }

    //just a WireMesh for cheching how much large is the OverlapBox
    private void OnDrawGizmos()
    {
        scale = new(
                Area.lossyScale.x * tur.totMultilier,
                (Area.lossyScale.y * 2) * tur.totMultilier,
                Area.lossyScale.z * tur.totMultilier
            );
        Gizmos.color = Color.blue;
        Gizmos.DrawWireMesh(box, transform.position, transform.rotation, scale);
    }
}

