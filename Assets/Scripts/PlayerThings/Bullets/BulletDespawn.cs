using System.Drawing;
using UnityEngine;

public class BulletDespawn : MonoBehaviour
{
    [SerializeField] GameObject Bullet;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("Despawner"))
        {
            Debug.Log("Entrato in contatto");
            Destroy(Bullet);
        }
    }
}
