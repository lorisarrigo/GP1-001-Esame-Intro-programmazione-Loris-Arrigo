using UnityEngine;

public class AreaBullet : MonoBehaviour
{
    GameObject Colobj;

    Turrets tur;

    //[SerializeField] Collider Area;

    private void OnTriggerEnter(Collider other)
    {
        Colobj = other.gameObject;
        if (other.CompareTag("Enemy") || other.CompareTag("Despawner"))
        {
            AreaDam();
            
        }
    }
    private void Start()
    {
        tur = GetComponentInParent<Turrets>();
    }

    public void AreaDam()
    {
        if (Colobj == null) return;
        Colobj.TryGetComponent(out IDamageable damageable);
        Colobj.TryGetComponent(out Enemy enemy);

        if (damageable == null) return;
        damageable.TakeDamage(tur.DamagePerHit);

        if (enemy == null) return;
        if (enemy.CurrentHealth <= 0)
        {
            damageable.Despawn();
        }
    }
}