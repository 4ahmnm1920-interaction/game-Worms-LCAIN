using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    public GameObject[] heartObj;
    public Animator heartObjAnim;
    public int CurrentHealth;
    public int MaxHealth = 5;

    //Death
    [Header("Death Settings")]
    public Animator deathAnim;

    // Start is called before the first frame update
    void Start()
    {
        MaxHealth = heartObj.Length;
        CurrentHealth = MaxHealth;
        
    }

    public void Damage()
    {
        CurrentHealth = CurrentHealth - 1;
        heartObjAnim = heartObj[CurrentHealth].gameObject.GetComponent<Animator>();
        heartObjAnim.SetTrigger("subtractHeart");
    }

    public void Heal()
    {
        CurrentHealth = CurrentHealth + 1;
        heartObjAnim = heartObj[CurrentHealth-1].gameObject.GetComponent<Animator>();
        heartObjAnim.SetTrigger("addHeart");
    }

    private void Update()
    {
        if (CurrentHealth <= 0)
        {
            deathAnim.SetTrigger("win");

            for (int i = 0; i < MaxHealth; i++)
            {
                heartObjAnim = heartObj[i].gameObject.GetComponent<Animator>();
                heartObjAnim.SetTrigger("subtractHeart");
            }
            Destroy(this.gameObject);
        }

        if (CurrentHealth > MaxHealth)
        {
            CurrentHealth = MaxHealth;
        }
    }
}
