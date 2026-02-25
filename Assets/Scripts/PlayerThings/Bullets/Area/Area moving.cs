using UnityEngine;

public class Areamoving : MonoBehaviour
{
    Rigidbody rb;
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

    public void Move()
    {
        rb.linearVelocity = (new Vector3(transform.position.x, transform.position.y, 1) * tur.BSpeed) * Time.fixedDeltaTime; //Move the enemy to the left
    }

}
