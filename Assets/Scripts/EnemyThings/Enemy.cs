using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Health settings")]
    [SerializeField] float MaxHealth;
    [SerializeField] float CurrentHealth;

    [Header("other settings")]
    [SerializeField] float Speed;
    [SerializeField] float DamagePerHit;
    [SerializeField] int Money;

    Rigidbody rb;
    bool canMove;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        canMove = true;

        transform.localScale = new Vector3(
            1f / transform.parent.transform.localScale.x,
            1f / transform.parent.transform.localScale.y,
            1f / transform.parent.transform.localScale.z);
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (canMove)
        {
            rb.linearVelocity = new Vector3(-1, transform.position.y, transform.position.z) * Speed; //Move the enemy to the left
        }
        else
        {
            rb.linearVelocity = Vector3.zero; //Stop the enemy from moving
        }
    }
}
