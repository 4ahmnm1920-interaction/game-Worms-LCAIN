using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    public GameObject[] heartObj;
    public int CurrentHealth;
    public int MaxHealth = 5;

    // Start is called before the first frame update
    void Start()
    {
        CurrentHealth = MaxHealth;
    }

    public void Damage()
    {
        CurrentHealth = CurrentHealth - 1;
    }

    public void Heal()
    {
        CurrentHealth = CurrentHealth + 1;
    }

    private void Update()
    {
        if (CurrentHealth <= 0)
        {
            Destroy(this.gameObject);
        }

        if (CurrentHealth > MaxHealth)
        {
            CurrentHealth = MaxHealth;
        }
    }
}
