using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject healthPickups;
    public GameObject ammoPickups;
    [Header("Amount of Iterations")]
    public int HeartIterations;
    public int BulletIterations;
    [Header("Set Spawn Range")]
    public float RangeXp;
    public float RangeXn;
    public float RangeYp;
    public float RangeYn;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < HeartIterations; i++)
        {
            float x = Random.Range(RangeXn, RangeXp);
            float y = Random.Range(RangeYn, RangeYp);
            Vector2 position = new Vector2(x, y);

            Instantiate(healthPickups, position, Quaternion.identity);
        }

        for (int i = 0; i < BulletIterations; i++)
        {
            float x = Random.Range(RangeXn, RangeXp);
            float y = Random.Range(RangeYn, RangeYp);
            Vector2 position = new Vector2(x, y);

            Instantiate(ammoPickups, position, Quaternion.identity);
        }
    }
}
