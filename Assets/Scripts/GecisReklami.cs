using GoogleMobileAds.Api;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GecisReklami : MonoBehaviour
{
#if UNITY_EDITOR
    string _adUnitID = "ca-app-pub-4239141501894599/1616733418";
#elif UNITY_IPHONE
    string _adUnitID = "ca-app-pub-3940256099942544/4411468910";
#else
    string _adUnitID = "unused";
#endif

    InterstitialAd _GecisReklami;
    int sonrakiSahne;

    void Start()
    {
        MobileAds.Initialize(initStatus => { });
        GecisReklamiOlustur();
    }

    void GecisReklamiOlustur()
    {
        if (_GecisReklami != null)
        {
            Debug.Log("Reklam zaten var, tekrar oluþturulmadý.");
            return;
        }

        var _AdRequest = new AdRequest.Builder().Build();
        InterstitialAd.Load(_adUnitID, _AdRequest, (InterstitialAd Ad, LoadAdError error) =>
        {
            if (error != null || Ad == null)
            {
                Debug.LogError("Reklam yüklenirken hata oluþtu: " + error);
                return;
            }
            _GecisReklami = Ad;
            ReklamOlaylariniDinle(_GecisReklami);
        });
    }

    void ReklamOlaylariniDinle(InterstitialAd ad)
    {
        ad.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("Reklam kapandý. Sahne deðiþtirilecek.");
            SceneManager.LoadScene(sonrakiSahne);
        };
    }

    public void GecisReklamiGoster(int sahneIndex)
    {
        sonrakiSahne = sahneIndex;

        if (_GecisReklami != null && _GecisReklami.CanShowAd())
        {
            _GecisReklami.Show();
            Debug.Log("Reklam gösterildi.");
            Time.timeScale = 1;
        }
        else
        {
            Debug.Log("Geçiþ reklamý hazýr deðil, direkt sahne deðiþtir.");
            SceneManager.LoadScene(sonrakiSahne);
            Time.timeScale = 1;
        }
    }
}
