using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sarkik
{
    public GameObject engel { get; set; }
    public bool gecildi { get; set; }
    public Sarkik(GameObject engel, bool gecildi)
    {
        this.engel = engel;
        this.gecildi = gecildi;
    }

    
}
