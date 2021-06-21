using System;
using UnityEngine;
using GoogleMobileAds.Api;

public class ReklamManager : MonoBehaviour
{
    private string bannerId = "";
    private string InterId = "";
    public bool goster = false;

    private BannerView bannerView;
    private InterstitialAd interView;

    void Start()
    {
        MobileAds.Initialize(initStatus => { });
        this.bannerHazirla();
        this.interHazirla();
    }

    private void FixedUpdate()
    {
        if (this.goster && interView != null && interView.IsLoaded())
        {
            interView.Show();
            this.goster = false;
            
        }
    }

    public void interHazirla()
    {
        if (this.InterId == "")
        {
            this.InterId = "*********************"; //Test Id
        }
        Debug.Log(this.InterId);
        this.interView = new InterstitialAd(this.InterId);
        this.interView.LoadAd(new AdRequest.Builder().Build());

        this.interView.OnAdClosed += HandleOnAdClosed;
    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        Application.LoadLevel(Application.loadedLevel);
        interHazirla();
    }

    private void bannerHazirla()
    {
        if (this.bannerId == "")
        {
            this.bannerId = "*********************"; //Test Id

        }

        AdSize adaptiveSize = AdSize.GetCurrentOrientationAnchoredAdaptiveBannerAdSizeWithWidth(AdSize.FullWidth);

        this.bannerView = new BannerView(this.bannerId, AdSize.SmartBanner, AdPosition.Bottom);
        this.bannerView.LoadAd(new AdRequest.Builder().Build());
    }
    
}
