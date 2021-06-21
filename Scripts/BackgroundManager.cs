using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    GameObject myCam;
    bool yeniImage = false;
    GameObject newImage, defaultImage;

    // Start is called before the first frame update
    void Start()
    {
        myCam = GameObject.Find("Main Camera");
        //newImage = (GameObject)Instantiate(this.gameObject, new Vector2(transform.position.x+22 , transform.position.y), transform.rotation);
        newImage = GameObject.Find("BackgroundManager2");
        defaultImage = GameObject.Find("BackgroundManager");
    }

    // Update is called once per frame
    void Update()
    {
        createBackground();
    }

    void createBackground()
    {
        if(myCam.transform.position.x > defaultImage.transform.position.x + 50 && !yeniImage)
        {
            yeniImage = true;
            newImage.transform.position = new Vector3(defaultImage.transform.position.x +75, defaultImage.transform.position.y, defaultImage.transform.position.z);
        }
        if(myCam.transform.position.x > newImage.transform.position.x + 50 && yeniImage)
        {
            yeniImage = false;
            defaultImage.transform.position = new Vector3(myCam.transform.position.x +25, myCam.transform.position.y, defaultImage.transform.position.z);
        }
        
    }
}
