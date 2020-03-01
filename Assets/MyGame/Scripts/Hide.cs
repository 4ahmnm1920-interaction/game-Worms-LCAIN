using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hide : MonoBehaviour
{
    bool IsShown = true;
    public SpriteRenderer boopty;

    // Update is called once per frame
    void Update()
    {
        if (IsShown)
        {
            boopty.enabled = true;
        }

        else
        {
            boopty.enabled = false;
        }

        if (Input.GetKeyUp(KeyCode.H))
        {
            IsShown = !IsShown;
        }
    }
}
