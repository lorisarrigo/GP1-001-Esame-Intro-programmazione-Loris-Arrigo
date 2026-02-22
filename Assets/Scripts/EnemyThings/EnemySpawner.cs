using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject[] Enemies;
    [SerializeField] float SpawnRate;
    float timer;

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= SpawnRate)
        {
            Instantiate(Enemies[Random.Range(0, Enemies.Length)], transform.position, transform.rotation, transform);
            timer = 0;
        }
    }
}
