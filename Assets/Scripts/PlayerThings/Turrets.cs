using UnityEngine;

public class Turrets : MonoBehaviour
{
    [SerializeField] Transform Bspawner;
    public GameObject Bullets;
    public float BulletRate;
    private float Timer;
    public float BSpeed;
    public float Area;

    public int DamagePerHit;
    [SerializeField] int Prize;

    private void Update()
    {
        Timer += Time.deltaTime;
        if(Timer >= BulletRate)
        {
            Instantiate(Bullets, Bspawner.position, Bspawner.rotation, Bspawner.transform);
            Timer = 0;
        }
    }
}
