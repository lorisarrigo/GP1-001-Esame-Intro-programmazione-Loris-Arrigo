using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    float timer;

    Turrets tur;

    private void Start()
    {
        tur = GetComponentInParent<Turrets>();
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if(timer >= tur.BulletRate)
        {
            Instantiate(tur.Bullets, transform.position, transform.rotation, transform);
            timer = 0;
        }
    }
}
