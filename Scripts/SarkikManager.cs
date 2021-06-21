using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#pragma warning disable 0649

public class SarkikManager : MonoBehaviour
{
    public Object sarkik;
    private float xSpawn = 0, yolUzunlugu = 25 ;
    public List<Sarkik> aktifSarkiklar;
    public GameObject bestScoreText, deathlineBottom, baslangicHinge;
    private int skor =0, engelIndex = 0;
    public Text skorText;
    private Rigidbody2D bilekManageSag, bilekManageSol;
    public Rigidbody2D vucutRD, Adam;
    public bool hayat, basladi, hizlandirilmali, intro;
    public CameraController myCam;
    public JointManager jointManage;
    public ReklamManager reklamManager;

    class sarkiklarim
    {
        public GameObject engel;
        public bool gecildi;
    }
    void Start()
    {
        bilekManageSag = GameObject.FindGameObjectWithTag("sag_bilek").GetComponent<Rigidbody2D>();
        bilekManageSol = GameObject.FindGameObjectWithTag("sol_bilek").GetComponent<Rigidbody2D>();
        vucutRD = GameObject.FindGameObjectWithTag("adamTag").GetComponent<Rigidbody2D>();
        aktifSarkiklar = new List<Sarkik>();
        intro = true;
        vucutRD.constraints = RigidbodyConstraints2D.FreezePositionY;
        basladi = false;

        bestScoreText.GetComponent<UnityEngine.UI.Text>().text = "Best Score: " + PlayerPrefs.GetInt("best");
    }
    void Update()
    {
        deathlineBottom.transform.position = new Vector3(vucutRD.position.x, deathlineBottom.transform.position.y);
        if (intro == false)
        {
            bestScoreText.SetActive(false);
            vucutRD.constraints = RigidbodyConstraints2D.None;
            
        }
        if (basladi) 
        {
            skorAl();
            if (aktifSarkiklar.Count < 8)
            {
                aktifSarkiklar.Add(sarkikOlustur());
            }
            if (vucutRD.position.x > aktifSarkiklar[0].engel.transform.position.x + 20)
            {
                Destroy(aktifSarkiklar[0].engel);
                aktifSarkiklar.RemoveAt(0);
            }
            skorText.text = "Skor : " + skor.ToString();
            if (!hayat)
            {
                kill();
            } 
        }
    }
    public Sarkik sarkikOlustur()
    {
        GameObject sa = (GameObject)Instantiate(sarkik, new Vector3(transform.position.x + (yolUzunlugu * xSpawn++), 10 ,0), transform.rotation);
        sa.name = (engelIndex++).ToString();
        Sarkik mySarkik = new Sarkik(sa, false);
        return mySarkik;
    }
    public void skorAl()
    {
        foreach(Sarkik s in aktifSarkiklar)
        {
            if (!s.gecildi && vucutRD.transform.position.x > s.engel.transform.position.x)
            {
                skor += 10;
                s.gecildi = true;
            }
        }
    }
    public GameObject enYakin()
    {
        GameObject enYakınMesafe = null;
        for(int i=0;i< aktifSarkiklar.Count; i++)
        {
            if(vucutRD.transform.position.x < aktifSarkiklar[i].engel.transform.position.x)
            {
                float mesafe = Vector3.Distance(vucutRD.transform.position, aktifSarkiklar[i].engel.transform.position);
                if(enYakınMesafe == null || mesafe < Vector3.Distance(vucutRD.transform.position, enYakınMesafe.transform.position))
                {
                    enYakınMesafe = aktifSarkiklar[i].engel;
                }
            }
        }
        return enYakınMesafe;
    }
    public Rigidbody2D ondeKol()
    {
        if (bilekManageSag.transform.position.x < bilekManageSol.transform.position.x)
        {
            return bilekManageSol;
        }
        else
        {
            return bilekManageSag;  
        }
    }
    public void hizlandir()
    {
        if (hizlandirilmali)
        {
            SoundManagerScript.PlayJumpSound();
            Vector2 myForce = new Vector2(15f, 55f);
            vucutRD.AddForce(myForce, ForceMode2D.Impulse);
        }
    }
    public void kill()
    {
        if (basladi)
        {
            hayat = true;
            basladi = false;

            if(PlayerPrefs.GetInt("best") < skor)
            {
                PlayerPrefs.SetInt("best", skor);
            }

            reklamManager.goster = true;
        }
    }

    public void adjustDeathLineBottom()
    {
            deathlineBottom.transform.position = new Vector2(deathlineBottom.transform.position.x, -35);
    }

}

//ARKAPLANDA BİRŞEYLER??? AKAN EVLER
//ALTTAN GELEN ENGELLER
//HARİTADA RANDOM PUAN TOPLAMA? KALP ALMA FALAN
//BELKİ ÖDÜL REKLAMI
//HIZ GÖSTERGESİ
//MÜZİK SESİ - MENÜYE
//oyun başlamadan önce animasyon fikri - portakal sesi gibi fıççık