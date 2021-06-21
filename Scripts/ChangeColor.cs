using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    //public Color setColor = Color.white;
    SpriteRenderer rend;
    void Start()
    {
        rend = this.GetComponent<SpriteRenderer>();
        rend.material.color = new Color(1,0,0,1);
    }

    // Update is called once per frame
    void Update()
    {
        rend.material.color = new Color(1,0,0,1);
    }
}
