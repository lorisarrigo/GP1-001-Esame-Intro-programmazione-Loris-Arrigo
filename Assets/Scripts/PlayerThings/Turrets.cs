using TMPro;
using UnityEngine;

public class Turrets : MonoBehaviour
{
    //class used to manage the Turrets used by the Player.

    //Geys the Stats to assign to the bullets
    [Header("Bullet setting & Stats")]
    [SerializeField] Transform bulletSpawner;
    public GameObject bullet;

    public float bRate, bSpeed;
    private float timer;
    public float dmgPerHit;

    [Header("Powerups")]
    [SerializeField] TMP_Text powerUpCounter; //the TMP for the prize
    private int currentPrice; //the Current PowerUp Prize used to calculate the next prize

    //The Multiplier used in the Rate & Area PowerUps
    [Header("powerUp settings")]
    [SerializeField] float multiplier; 

    [Header("MachineGun PU Settings")]
    [SerializeField] float minRate;

    [Header("Area PU Settings")]
    public float totMultilier = 1; 

    //sets the Scale of the turrets so that they will spawn with this scale
    private void Start()
    {
        PrizeUpdate();
        transform.localScale = new Vector3(
            1f / transform.parent.transform.localScale.x,
            1f / transform.parent.transform.localScale.y,
            1f / transform.parent.transform.localScale.z);
    }

    private void Update()
    {
        //Increase the Timer
        timer += Time.deltaTime;

        //When it reaches the Rate, Spawn the Bullet in the Spawner position,
        //rotated in X of 90° and make them a child of the Spawner;
        //then reset the Timer
        if(timer >= bRate)
        {
            Instantiate(bullet, bulletSpawner.position, Quaternion.Euler(90, 0, 0), bulletSpawner.transform);
            timer = 0;
        }
    }

    //Update the prizes of the PowerUps based on their tags
    //(recalled at the start and Every time a PowerUp get purchased)
    private void PrizeUpdate()
    {
        if (this.CompareTag("Normal"))
        { 
            powerUpCounter.text = UIManager.Instance.normalPrice.ToString();
            currentPrice = UIManager.Instance.normalPrice;
        }
        else if (this.CompareTag("MG"))
        { 
            powerUpCounter.text = UIManager.Instance.machinegunPrice.ToString();
            currentPrice = UIManager.Instance.machinegunPrice;
        }
        else if (this.CompareTag("Area"))
        { 
            powerUpCounter.text = UIManager.Instance.areaPrice.ToString();
            currentPrice = UIManager.Instance.areaPrice;
        }
    }

    /*PowerUp function:
     * for the normal Turret, Doubles the damage;
     * for the MachineGun, subtract a custom rate to the Rate of the Tower;
     * for the Area, multiply the area so that it can calculate how much big needs to be.*/
    private void PowerUP()
    {
        if (this.CompareTag("Normal"))
        {
            dmgPerHit *= multiplier;
        }
        else if (this.CompareTag("MG"))
        {
            bRate -= multiplier;
        }
        else if (this.CompareTag("Area"))
        {
            totMultilier *= multiplier;
        }
    }

    /*When the Turret is Clicked, if there are enough money, 
     * decreases the money counter by the price, doubles the price & Update the TMP,
     * and activate the PowerUp*/
    public void PowerUp()
    {
        if(UIManager.Instance.money >= currentPrice && bRate > minRate)
        {
            UIManager.Instance.money -= currentPrice;
            UIManager.Instance.UpdateCounter();
            currentPrice *= 2;
            powerUpCounter.text = currentPrice.ToString();
            PowerUP();
        }
    }
}
