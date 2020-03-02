using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnCall : MonoBehaviour
{
    public void DestroyThis()
    {
        Destroy(this.gameObject);
    }
}
