using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScript : MonoBehaviour
{
    static TutorialScript instance = null;

    bool IsShown;
    public SpriteRenderer SRender;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            GameObject.DontDestroyOnLoad(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (IsShown)
        {
            SRender.enabled = true;
        }

        else
        {
            SRender.enabled = false;
        }

        if (Input.GetKeyUp(KeyCode.H))
        {
            IsShown = !IsShown;
        }
    }
}
