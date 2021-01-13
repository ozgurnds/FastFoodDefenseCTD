using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AltMenuButonlar : MonoBehaviour
{
    public GameObject AraMenü, AraAyarlar,AraDön,AraYeniden,AraAnamenü,AraBaşlat,AraKitap;
    EventSystem ev;
    bool yeniden, anamenü;

    void Start()
    {
        ev = GameObject.FindGameObjectWithTag("EventSystem").GetComponent<EventSystem>();
    }

    public void Başlat(){
        ev.OyunAktif = true;
        ev.OyunHız = 1;
        Destroy(AraBaşlat);
    }
    public void AraMenüAç(){
        AraMenü.SetActive(true);
        AraAyarlar.SetActive(false);
        AraDön.SetActive(false);
        AraYeniden.SetActive(false);
        AraKitap.SetActive(false);
        ev.OyunHız = 0;
        gameObject.transform.GetChild(1).GetComponent<Image>().raycastTarget = true;
    }
    public void AraMenüKapat(){
        AraMenü.SetActive(false);
        AraAyarlar.SetActive(false);
        AraDön.SetActive(false);
        AraYeniden.SetActive(false);
        if (ev.OyunAktif)
        {
            ev.OyunHız = 1;
        }
        gameObject.transform.GetChild(1).GetComponent<Image>().raycastTarget = false;
    }
    public void AraAyarlarAc(){
        AraMenü.SetActive(false);
        AraAyarlar.SetActive(true);
    }
    public void YenidenBaşlat(){
        if(yeniden){
            SceneManager.LoadScene("0", LoadSceneMode.Single);

        }
        else
        {
            yeniden = true;
            AraYeniden.SetActive(true);
            AraMenü.SetActive(false);
        }
    }
    public void YenidenBaşlatİptal(){
        yeniden = false;
        AraYeniden.SetActive(false);
        AraMenü.SetActive(true);
    }
    public void AnaMenu(){
        if (anamenü)
        {
            SceneManager.LoadScene("AnaMenü", LoadSceneMode.Single);
        }
        else
        {
            anamenü = true;
            AraAnamenü.SetActive(true);
            AraMenü.SetActive(false);
        }
    }
    public void AnaMenüİptal(){
        anamenü = false;
        AraAnamenü.SetActive(false);
        AraMenü.SetActive(true);
    }
    public void AraKitapAç(){
        AraMenü.SetActive(false);
        AraKitap.SetActive(true);
    }

    public void YenidenBaşlat1(){
        SceneManager.LoadScene("0", LoadSceneMode.Single);
    }
    public void AnaMenü1(){
        SceneManager.LoadScene("AnaMenü", LoadSceneMode.Single);
    }
}
