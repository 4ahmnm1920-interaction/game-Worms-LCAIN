using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupHealth : MonoBehaviour
{
    public HealthController Wctrl;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Wctrl = collision.gameObject.GetComponent<HealthController>();
            Wctrl.Heal();
            Destroy(this.gameObject);
        }
    }
}
