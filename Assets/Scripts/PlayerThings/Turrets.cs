using TMPro;
using UnityEngine;

public class Turrets : MonoBehaviour
{
    [Header("Bullet setting & Stats")]
    [SerializeField] Transform bulletSpawner;
    public GameObject bullets;

    public float bulletRate, bSpeed;
    private float timer;
    public int damagePerHit;

    [Header("Powerups")]
    [SerializeField] GameObject PowerUpCanva; //The UI used for the prize of the PowerUp
    [SerializeField] TMP_Text powerUpCounter; //the TMP for the prize
    private int currentPrize; //the Current PowerUp Prize used to calculate the next prize
    //public int StartingPU_Prize;

    [Header("powerUp settings")]
    [SerializeField] float multiplier; //The PowerUp Multiplier

    [Header("MachineGun PU Settings")]
    [SerializeField] float minRate;

    [Header("Area PU Settings")]
    public float totMultilier; 


    private void Start()
    {
        PrizeUpdate(); 
    }

    private void Update()
    {
        //Increase the Timer
        timer += Time.deltaTime;

        //When the timer reaches the Rate, Spawn the Bullet in the Spawner position, rotated in X of 90° and make them a child of the Spawner;
        //then reset the Timer
        if(timer >= bulletRate)
        {
            Instantiate(bullets, bulletSpawner.position, Quaternion.Euler(90, 0, 0), bulletSpawner.transform);
            timer = 0;
        }

        //manteins the powerUp canva in the same rotation even if yoiu rotate the Turret
        PowerUpCanva.transform.rotation = Quaternion.Euler(90,0,0);  
    }

    //Update the prizes of the PowerUps
    private void PrizeUpdate()
    {
        if (this.CompareTag("Normal"))
        { 
            powerUpCounter.text = UIManager.Instance.normalPrize.ToString();
            currentPrize = UIManager.Instance.normalPrize;
        }
        else if (this.CompareTag("MG"))
        { 
            powerUpCounter.text = UIManager.Instance.machinegunPrize.ToString();
            currentPrize = UIManager.Instance.machinegunPrize;
        }
        else if (this.CompareTag("Area"))
        { 
            powerUpCounter.text = UIManager.Instance.areaPrize.ToString();
            currentPrize = UIManager.Instance.areaPrize;
        }
    }

    private void PowerUP()
    {
        if (this.CompareTag("Normal"))
        {
            Debug.Log("danno non potenziato: " + damagePerHit);
            damagePerHit *= 2;
            Debug.Log("torretta normale potenziata, danno:" + damagePerHit);
        }
        else if (this.CompareTag("MG"))
        {
            Debug.Log("Rate non potenziato: " + bulletRate);
            bulletRate -= multiplier;
            Debug.Log("Machine Gun potenziata, rate: " + bulletRate);
        }
        else if (this.CompareTag("Area"))
        {
            Debug.Log("Area non potenziato: " + totMultilier);
            totMultilier *= multiplier;
            Debug.Log("torretta area potenziata, Area: " + totMultilier);
        }
    }

    //When the Turret is Clicked, if there are enough money, decreases the money counter by the prize, doubles it & Update the TMP
    public void Multiply_PU()
    {
        if(UIManager.Instance.money >= currentPrize && bulletRate > minRate)
        {
            UIManager.Instance.money -= currentPrize;
            UIManager.Instance.UpdateCounter();
            currentPrize *= 2;
            powerUpCounter.text = currentPrize.ToString();
            PowerUP();
        }
    }
}
