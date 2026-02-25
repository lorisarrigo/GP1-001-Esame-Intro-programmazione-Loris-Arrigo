using System.Drawing;
using UnityEngine;

public class AreaBullets : MonoBehaviour
{
    [SerializeField] GameObject Bullet;
    //[SerializeField] Collider Center;
    [SerializeField] Rigidbody rbBullet;
    GameObject Colobj;

    //Rigidbody rb;
    Turrets tur;

    private void Start()
    {
        rbBullet = GetComponentInParent<Rigidbody>();
        tur = GetComponentInParent<Turrets>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void Move()
    {
        rbBullet.linearVelocity = (new Vector3(rbBullet.transform.position.x, rbBullet.transform.position.y, 1) * tur.BSpeed) * Time.fixedDeltaTime; //Move the enemy to the left
    }

    private void OnTriggerEnter(Collider other)
    {
        Colobj = other.gameObject;
        if (other.CompareTag("Enemy") || other.CompareTag("Despawner"))
        {
            Debug.Log("entrato in contatto");
            Destroy(Bullet);
        }
    }

    //private void OnCollisionEnter(Collision col)
    //{
    //    if (col.gameObject.CompareTag("Enemy") || col.gameObject.CompareTag("Despawner"))
    //    {
    //        Debug.Log("Entrato in contatto");
    //        Destroy(Bullet);
    //    }
    //}
}
