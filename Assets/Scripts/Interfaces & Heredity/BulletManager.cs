using UnityEngine;

public class BulletManager : MonoBehaviour
{
    /*class used to manage the movement of the two types of bullets:
     * the base bullet (that compriends the Normal & the Machinegun ones that deals damage by touching the Enemy);
     * the Overlap one (that deals damage by it's Area)
    */

    //Gets the components needed 
    Rigidbody rb;
    protected Turrets tur;

    public virtual void Start()
    {
        rb = GetComponent<Rigidbody>();
        tur = GetComponentInParent<Turrets>();
    }
    public virtual void FixedUpdate()
    {
        Trajectory();
    }

    //If the Turret is rotated in y of 0°, move the Bullet in Z axis by multiplying the meters per seconds;
    //in any other rotation, it'll move it in the opposite direction
    public virtual void Trajectory()
    {
        if (tur.transform.rotation.y == 0)
        { rb.linearVelocity = (new Vector3(transform.position.x, transform.position.y, -1) * tur.bSpeed) * Time.fixedDeltaTime; }
        else
        { rb.linearVelocity = (new Vector3(transform.position.x, transform.position.y, 1) * tur.bSpeed) * Time.fixedDeltaTime; }
    }
}
