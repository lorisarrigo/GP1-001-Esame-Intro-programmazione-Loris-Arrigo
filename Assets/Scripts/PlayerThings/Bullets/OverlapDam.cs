using System.Collections.Generic;
using UnityEngine;

public class OverlapDam : MonoBehaviour
{
    //Gets the components needed and set the Layer that needs to take damage
    Rigidbody rb;
    Turrets tur;
    [SerializeField] Mesh box;
    [SerializeField] LayerMask layer;
      
    List<IDamageable> Enem = new(); //The List used to check if the Enemy has already took the damage

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

    //same thing in the Bullet script
    private void Move()
    {
        if (tur.transform.rotation.y == 0)
        { rb.linearVelocity = (new Vector3(transform.position.x, transform.position.y, -1) * tur.bSpeed) * Time.fixedDeltaTime; }
        else
        { rb.linearVelocity = (new Vector3(transform.position.x, transform.position.y, 1) * tur.bSpeed) * Time.fixedDeltaTime; }
    }
    public void AreaDam()
    {
        //Create the OverlapBox, whit the caratteristics of the Area, in which the Enemies need to enter to take Damage
        Collider[] enemyCol = Physics.OverlapBox(transform.position, transform.lossyScale, Quaternion.identity, layer);

        //Foreach Enemy that enters the OverlapBox 
        foreach (var enemyc in enemyCol)
        {
            //check if there is the Interface & if the Interface isn't already called;
            //then it'll Add th Interface to the List and comunicate the damage to the Interface
            if(enemyc.TryGetComponent(out IDamageable damageable) && !Enem.Contains(damageable))
            {
                Enem.Add(damageable);
                damageable.TakeDamage(tur.damagePerHit);
                //Debug.Log("Enemy colpito");
            }
            //check if there is an Enemy, if it's health is below or equal to 0 and if it isn't null,
            //so that it can comunicate at the Interface to Recall the Despawn function for the Enemies inside the Overlap
            if (enemyc.TryGetComponent(out Enemy enemy) && enemy.currentHealth <= 0 && enemy != null)
            { damageable.Despawn(); }
        }
    }

    //if an Object whit the tag "Enemy" has made contact whit the body, it'll Destroy the Bullet
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("Despawner"))
        {
            Destroy(gameObject);
        }
    }

    //just a WireMesh for cheching how much large is the OverlapBox
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireMesh(box, transform.position, transform.rotation, transform.lossyScale);
    }
}

