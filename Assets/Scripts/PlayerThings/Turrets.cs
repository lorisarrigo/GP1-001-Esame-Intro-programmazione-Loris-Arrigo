using System;
using TMPro;
using UnityEngine;

public class Turrets : MonoBehaviour
{
    [Header("Bullet setting & Stats")]
    [SerializeField] Transform Bspawner;
    public GameObject Bullets;

    public float BulletRate;
    private float Timer;
    public float BSpeed;
    public int DamagePerHit;

    [Header("Powerups")]
    [SerializeField] TMP_Text powerUpCounter;
    [SerializeField] GameObject PowerUpCanva; //prendo il canva per mantenerlo fermo anche se ruotato
    public int CurrentPrize; //the Current PowerUp Prize used to calculate the next prize
    //public int StartingPU_Prize;

    [Header("Area Settings")]
    public float Multiplier; //Multiplier 
    public float Area;

    private void Start()
    {
        PowerUpCounter();
    }

    private void Update()
    {
        Timer += Time.deltaTime;
        if(Timer >= BulletRate)
        {
            Instantiate(Bullets, Bspawner.position, Quaternion.Euler(90, 0, 0), Bspawner.transform);
            Timer = 0;
        }

        PowerUpCanva.transform.rotation = Quaternion.Euler(90,0,0);
    }

    public void PowerUpCounter()
    {
        if (this.CompareTag("Normal"))
        { 
            powerUpCounter.text = UIManager.Instance.NormalPrize.ToString();
            CurrentPrize = UIManager.Instance.NormalPrize;
        }
        else if (this.CompareTag("MG"))
        { 
            powerUpCounter.text = UIManager.Instance.MGPrize.ToString();
            CurrentPrize = UIManager.Instance.MGPrize;
        }
        else if (this.CompareTag("Area"))
        { 
            powerUpCounter.text = UIManager.Instance.AreaPrize.ToString();
            CurrentPrize = UIManager.Instance.AreaPrize;
        }
    }
}
