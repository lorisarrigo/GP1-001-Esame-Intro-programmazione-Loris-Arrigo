using UnityEngine;

public class Turrets : MonoBehaviour
{
    [Header("Turret value")]
    [SerializeField] int Prize;
    
    [Header("Bullet setting & Stats")]
    [SerializeField] Transform Bspawner;
    public GameObject Bullets;

    public float BulletRate;
    private float Timer;
    public float BSpeed;
    public int DamagePerHit;


    [Header("Area Settings")]
    public float Multiplier;
    public float Area;


    private void Update()
    {
        Timer += Time.deltaTime;
        if(Timer >= BulletRate)
        {
            Instantiate(Bullets, Bspawner.position, Quaternion.Euler(90, 0, 0), Bspawner.transform);
            Timer = 0;
        }
    }
}
