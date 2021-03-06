﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupBullet : MonoBehaviour
{
    public GameObject Electricity;

    public float AmmoMultiplier;
    public WormsController Wctrl;
    private bool CountdownBool = false;
    public float Countdown = 3.0f;
    float AmmoOrig;

    private void Update()
    {
        if (CountdownBool)
        {
            Electricity.SetActive(true);
            Countdown -= Time.deltaTime;
            if (Countdown <= 0)
            {
                CountdownBool = false;
                Wctrl.AmmoForce = Wctrl.AmmoForceControl;
                Electricity.SetActive(false);
                Destroy(this.gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Electricity = collision.gameObject.transform.GetChild(0).gameObject;
            Wctrl = collision.gameObject.GetComponent<WormsController>();
            CountdownBool = true;
            if (CountdownBool)
            {
                Wctrl.AmmoForce = Wctrl.AmmoForce * AmmoMultiplier;
            }
            Destroy(this.gameObject.GetComponent<SphereCollider>());
            Destroy(this.gameObject.GetComponent<SpriteRenderer>());
        }
    }
}
