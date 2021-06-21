using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    //public Vector3 offset ;
    public RectTransform çerçeve;
    public float width, height;
    public float movingSpeed;
    public Rigidbody2D vucutRD;
    SarkikManager sarkikmanager;
    public GameObject deathlineBack;
    float timer;
    bool camHizlan = false;
    bool camHizlan2 = false;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (sarkikmanager.basladi == true && sarkikmanager.intro == false)
        {

            if ((vucutRD.transform.position.x+3.5 > transform.position.x +10.7) || camHizlan)
            {
                camHizlan = true;
                if (transform.position.x-3 > vucutRD.transform.position.x)
                {
                    camHizlan = false;
                }
                if (vucutRD.transform.position.x > transform.position.x + 10.7 ||camHizlan2)
                {
                    camHizlan2 = true;
                    if (transform.position.x-2 > vucutRD.transform.position.x)
                    {
                        camHizlan2 = false;
                    }
                        transform.position = new Vector3(transform.position.x + 0.6f, transform.position.y, transform.position.z);    
                }
                else
                {
                    transform.position = new Vector3(transform.position.x+0.4f, transform.position.y, transform.position.z);       //SABİT HIZLA İLERLEYEN KAMERA

                }
            }
            else
            {
                transform.position = new Vector3(transform.position.x + 0.2f, transform.position.y, transform.position.z);
                //transform.position += Vector3.right * 0.2f;     //SABİT HIZLA İLERLEYEN KAMERA
                sarkikmanager.hizlandirilmali = true;
            }
            if(deathlineBack.transform.position.x < vucutRD.transform.position.x)
            {
                if(deathlineBack.transform.position.x+50 < vucutRD.transform.position.x)
                {
                    killBack(0.48f);
                }
                if(deathlineBack.transform.position.x + 15 < vucutRD.transform.position.x)
                {
                    killBack(0.001f); // deneme yerleri var
                }
                else
                {
                    killBack(0.33f);
                }
            }
             //main menü düzgün görünecek
        }
        if (!sarkikmanager.hayat)
        {
            deathlineBack.transform.position = new Vector3(-32, 2,2);
        }
        //kamera açısı x pozisyonunda 10,7 SAĞA DOĞRU 10.7 SOLA DOĞRU 21.4 TOPLAM ACI
    }

    private void Start()
    {
        sarkikmanager = GameObject.Find("SarkikManager").GetComponent<SarkikManager>();
        width = Camera.main.WorldToScreenPoint(transform.position).x*2;
        height = Camera.main.WorldToScreenPoint(transform.position).y/5;
        çerçeve.sizeDelta = new Vector2(width, height);
    }

    public void killBack(float speed)
    {
        deathlineBack.transform.position = new Vector3(deathlineBack.transform.position.x + speed, deathlineBack.transform.position.y, deathlineBack.transform.position.z);
    }
}