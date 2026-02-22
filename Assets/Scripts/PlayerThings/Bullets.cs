using UnityEngine;

public class Bullets : MonoBehaviour
{
    bool canMove;
    Rigidbody rb;
    Turrets tur;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        tur = GetComponentInParent<Turrets>();
        canMove = true;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (canMove)
        {
            rb.linearVelocity = new Vector3(-1, transform.position.y, transform.position.z) * tur.BSpeed; //Move the enemy to the left
        }
        else
        {
            rb.linearVelocity = Vector3.zero; //Stop the enemy from moving
        }
    }
}
