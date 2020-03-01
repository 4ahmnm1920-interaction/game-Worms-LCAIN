using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillzoneScript : MonoBehaviour
{
    public HealthController Wctrl;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Wctrl = collision.gameObject.GetComponent<HealthController>();
            Wctrl.CurrentHealth = 0;
        }
    }
}
