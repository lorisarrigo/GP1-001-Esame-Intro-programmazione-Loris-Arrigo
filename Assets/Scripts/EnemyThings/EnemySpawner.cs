using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    //the class needed to manage the Enemy Spawner

    //gets the Components & the stats needed
    [Header("Enemies")]
    [SerializeField] GameObject[] Enemies;

    [Header("Rate settings")]
    public int spawnCounter;
    [SerializeField] float spawnRate, minRate, subtract; //The spawn Rate, minimum Rate, and how much needs to be detracted
    [SerializeField] int changeAt; //The gap that the Counter needs to reach to change the settings


    [Header("Enemy Damage Upgrade")]
    [SerializeField] float addDmg; //How much Damage needs to be Added
    public float totalDmg; //the total damage that will be set to the Enemy

    float timer;

    private void Update()
    {
        //Increase the Timer
        timer += Time.deltaTime;

        //if the Timer reach the Spawn Rate, Spawn a random Enemy in the "enemies" List, whit the position, the rotation of the spawner;
        //olso makes the enemies a child of it, add 1 to the Counter and reset the timer
        if (timer >= spawnRate)
        {
            Instantiate(Enemies[Random.Range(0, Enemies.Length)], transform.position, transform.rotation, transform);
            spawnCounter++;
            timer = 0;
        }
        //if the division between the Counter and the gap doesn't have the rest, the Counter isn't Empty and the Spawn Rate is Higher then the minimum 
        //subtract to the Spawn Rate to a custom float, add the damage to the total then reset the Counter
        if (spawnCounter % changeAt == 0 && spawnCounter != 0 && spawnRate > minRate)
        {
            spawnRate -= subtract;
            totalDmg += addDmg;
            spawnCounter = 0;
        }
    }
}
