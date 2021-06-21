using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetect : MonoBehaviour
{
    SarkikManager sarkikmanage;
    bool tek = true;

    private void Start()
    {
        sarkikmanage = GameObject.Find("SarkikManager").GetComponent<SarkikManager>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!sarkikmanage.intro)
        {
            SoundManagerScript.PlayKillSound();
            sarkikmanage.hayat = false;
        }
        if (sarkikmanage.intro && collision.gameObject.name == "sag_on_ayak" && tek)
        {
            tek = false;
            SoundManagerScript.PlayKillSound();
        }

        if (sarkikmanage.intro && collision.gameObject.name == "Hook")
        {
            SoundManagerScript.PlayMetalSound();
        }

        if (sarkikmanage.intro && collision.gameObject.name == "Man")
        {
            SoundManagerScript.PlayMetalSound();
        }
    }

}
