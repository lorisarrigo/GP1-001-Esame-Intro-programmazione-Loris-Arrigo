using TMPro;
using UnityEngine;

public class Turrets : MonoBehaviour
{
    [Header("Bullet setting & Stats")]
    [SerializeField] Transform bulletSpawner;
    public GameObject bullets;

    public float bulletRate;
    private float timer;
    public float bSpeed;
    public int damagePerHit;

    [Header("Powerups")]
    [SerializeField] GameObject PowerUpCanva; //The UI used for the prize of the PowerUp
    [SerializeField] TMP_Text powerUpCounter; //the TMP for the prize
    private int currentPrize; //the Current PowerUp Prize used to calculate the next prize
    //public int StartingPU_Prize;

    [Header("Area Settings")]
    public float multiplier; //The PowerUp Multiplier
    public float area;

    private void Start()
    {
        PowerUpCounter(); 
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
    public void PowerUpCounter()
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

    //When the Turret is Clicked, if there are enough money, decreases the money counter by the prize, doubles it & Update the TMP
    public void Multiply_PU()
    {
        if(UIManager.Instance.money >= currentPrize)
        {
            UIManager.Instance.money -= currentPrize;
            UIManager.Instance.UpdateCounter();
            currentPrize *= 2;
            powerUpCounter.text = currentPrize.ToString();
        }
        else
        {
            Debug.Log("sei povero skill issue");
        }
    }
}
