using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JointManager : MonoBehaviour
{
    public GameObject sarkik;
    public Rigidbody2D vucutRD;
    public HingeJoint2D sarkikHinge;
    SarkikManager sarkikmanage;
    bool tikladi = false;
    public GameObject aktifSarkik;
    HingeJoint2D baslangicHinge;
    public LineRenderer lineRenderer;
    Rigidbody2D kol;
    private Rigidbody2D bilekManageSol;
    public Rigidbody2D baslangic;
    GameObject fakeman, headingMan, headingHook;
    //UnityEngine.BoxCollider2D ;
    float timer;


    void Start()
    {
        headingHook = GameObject.Find("Hook");
        headingMan  = GameObject.Find("Man");
        //headingMan.isTrigger = true;
        fakeman = GameObject.FindGameObjectWithTag("fake");
        sarkikmanage = GameObject.Find("SarkikManager").GetComponent<SarkikManager>();
        baslangicHinge = baslangic.GetComponent<HingeJoint2D>();
        bilekManageSol = GameObject.FindGameObjectWithTag("sol_bilek").GetComponent<Rigidbody2D>();
        timer = 0;
    }

    void Update()
    {
        if (!sarkikmanage.intro)
        {
            tutunma();
            getLine(kol);
        }
        if (sarkikmanage.intro)
        {
            introAnim();
        }
    }

    void FixedUpdate()
    {
        adjustMenu();
    }

    private void tutunma()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Ended && sarkikHinge != null)
            //if(Input.GetMouseButtonUp(0) && sarkikHinge != null)
            {
                sarkikHinge.connectedBody = null;
                sarkikmanage.hizlandir();
                lineRenderer.gameObject.SetActive(false);
                tikladi = false;
            }



            if (touch.phase == TouchPhase.Began)
            //if(Input.GetMouseButtonDown(0))
            {
                SoundManagerScript.PlayWebSound();
                aktifSarkik = sarkikmanage.enYakin();
                sarkikHinge = aktifSarkik.GetComponent<HingeJoint2D>();
                if (!tikladi)
                {
                    kol = sarkikmanage.ondeKol();
                }
                sarkikHinge.connectedBody = kol;
                tikladi = true;
                lineRenderer.gameObject.SetActive(true);
            }

        }
    }

    public void getLine(Rigidbody2D kol)
    {
        if(sarkikHinge != null)
        {
                lineRenderer.SetPosition(0, kol.transform.position);
                lineRenderer.SetPosition(1, aktifSarkik.transform.position);
        }
        else
        {
            lineRenderer.gameObject.SetActive(false);
        }
    }

    public void introAnim()
    {
        if(Input.touchCount > 0)
        {
            Touch intro = Input.GetTouch(0);
            if (intro.phase == TouchPhase.Began)
            //if (Input.GetMouseButton(0))
            {
                //SoundManagerScript.PlaySound();
                intro.phase = TouchPhase.Canceled;
                tikladi = true;
                baslangicHinge.connectedBody = bilekManageSol;//BAŞLANGIÇ HİNGE İLK TIK DA ORJİNAL GELEN ADAM İÇİN, ENYAKIN FONK KULLANMA DİREK SAĞ EL İLE AL...
                sarkikmanage.vucutRD.constraints = RigidbodyConstraints2D.None;
            }

        }
        if (tikladi)
        {
            lineRenderer.SetPosition(0, bilekManageSol.transform.position);
            lineRenderer.SetPosition(1, baslangic.transform.position);
            lineRenderer.gameObject.SetActive(true);
            timer += Time.deltaTime * 1;
        }
        if (timer >= 2.5)
        {
            sarkikmanage.adjustDeathLineBottom();
            sarkikmanage.intro = false;
            sarkikmanage.basladi = true;
            sarkikmanage.hayat = true;
            fakeman.SetActive(false);
            GameObject.Find("tapToPlay").SetActive(false);

            baslangicHinge.connectedBody = null;
            sarkikmanage.hizlandir();

            lineRenderer.gameObject.SetActive(false);

            tikladi = false;

            //hookman simüle kapanacak

            //headingMan.GetComponent<RectTransform>().anchoredPosition.Set(0,0);

            //headingMan.transform.position = new Vector2(142, 4);

            
            
            //headingHook.transform.position = new Vector2(0, 0);

        }
    }

    public void adjustMenu()
    {
        if (!sarkikmanage.intro && sarkikmanage.basladi)
        {
            headingHook.GetComponent<BoxCollider2D>().isTrigger = true;
            headingMan.GetComponent<BoxCollider2D>().isTrigger = true;
            headingHook.GetComponent<Rigidbody2D>().simulated = false;
            headingMan.GetComponent<Rigidbody2D>().simulated = false;

            Vector2 hookAnchored = headingHook.GetComponent<RectTransform>().anchoredPosition;
            Vector2 manAnchored = headingMan.GetComponent<RectTransform>().anchoredPosition;

            if (Mathf.Abs(headingHook.transform.position.y - 550f) > 0.05)
            {
                //this.baslik.GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(pos, new Vector2(pos.x, (!this.bitti ? baslik_ust : baslik_alt)), 0.05f);
                headingHook.GetComponent<RectTransform>().anchoredPosition   = Vector2.Lerp(hookAnchored, new Vector2(-130f, 675f), 0.05f);
                headingMan.GetComponent<RectTransform>().anchoredPosition    = Vector2.Lerp(manAnchored, new Vector2(200f, 675f), 0.05f);
            }
        }
    }

}

