using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EventSystem : MonoBehaviour
{
    // Bölümler = Checkpointler BölümObjeler= ekrandaki kareler
    public GameObject[] Bölümler,BölümObjeler;
    // Düşman verilerini tutar
    public List<Dusman> DusmanData;
    // Turret verilerini tutar
    public List<Turretler> TurretData;
    // Kullanıcı Arayüzü
    public Canvas canvas;
    // Arkaplan dokunmayı kapatır
    public MenüKapama Kapama;
    // Son oynanan seviye
    int kayıtlıseviye;

    // Seçilen kare ve turret market menüsü
    public GameObject TurretSpawn,AltMenu;
    public int can,su,para,OyunHız,Bölüm,DalgaSira,DüsmanSira,Düsman;
    // Oyunu durdurmak için
    public bool OyunAktif;

    void Awake()
    {
        // Sahneler arasi veri geçişi
        Bölüm = PlayerPrefs.GetInt("SeçiliBölüm");
        kayıtlıseviye = PlayerPrefs.GetInt("SonSeviye");
    }


    void Start()
    {
        //Bölüm Spawn Etme---------------------------------
        GameObject bölüm = (GameObject)Instantiate(BölümObjeler[Bölüm], canvas.transform.position, new Quaternion(0, 0, 0, 0));
        bölüm.transform.SetParent(canvas.transform);
        bölüm.transform.SetSiblingIndex(1);
        bölüm.transform.localScale = new Vector3(1, 1, 1);
        Bölümler[Bölüm].SetActive(true);
        bölüm.transform.localPosition = new Vector3(0, 0, 0);
        //---------------------------------------------------
    }

    public void CanDeğiştir(int hasar){
        // Can azaltma
        can -= hasar;
        if(can <= 0){
            Kaybet();
        }
    }
    public void Kazan(){
        canvas.transform.GetChild(5).GetChild(3).GetChild(0).gameObject.SetActive(true);
        canvas.transform.GetChild(5).GetChild(3).GetChild(1).gameObject.SetActive(true);
        if (Bölüm == 9)
        {
            // Son Bölüm için devam et butonunu kapatır
            canvas.transform.GetChild(5).GetChild(3).GetChild(1).GetChild(4).GetComponent<Button>().interactable = false;
        }
        // Yıldız verme
        if (can >= 8){
            canvas.transform.GetChild(5).GetChild(3).GetChild(0).gameObject.SetActive(true);
            canvas.transform.GetChild(5).GetChild(3).GetChild(1).transform.GetChild(1).GetChild(0).gameObject.SetActive(true);
            canvas.transform.GetChild(5).GetChild(3).GetChild(1).transform.GetChild(2).GetChild(0).gameObject.SetActive(true);
            canvas.transform.GetChild(5).GetChild(3).GetChild(1).transform.GetChild(3).GetChild(0).gameObject.SetActive(true);
        }
        else if (can < 8 && can >= 5){
            canvas.transform.GetChild(5).GetChild(2).GetChild(1).transform.GetChild(1).GetChild(0).gameObject.SetActive(true);
            canvas.transform.GetChild(5).GetChild(2).GetChild(1).transform.GetChild(2).GetChild(0).gameObject.SetActive(true);
            canvas.transform.GetChild(5).GetChild(2).GetChild(1).transform.GetChild(3).GetChild(0).gameObject.SetActive(false);
        }
        else
        {
            canvas.transform.GetChild(5).GetChild(2).GetChild(0).transform.GetChild(1).GetChild(0).gameObject.SetActive(true);
            canvas.transform.GetChild(5).GetChild(2).GetChild(0).transform.GetChild(2).GetChild(0).gameObject.SetActive(false);
            canvas.transform.GetChild(5).GetChild(2).GetChild(0).transform.GetChild(3).GetChild(0).gameObject.SetActive(false);
        }
        // Oynadığımız bölümlerin kapanmaması için
        if (kayıtlıseviye <= Bölüm)
        {
            kayıtlıseviye = Bölüm +1;
            PlayerPrefs.SetInt("SonSeviye", kayıtlıseviye);
        }
        OyunHız = 0;
        OyunAktif = false;
    }
    public void Kaybet()
    {
        canvas.transform.GetChild(5).GetChild(3).GetChild(0).gameObject.SetActive(true);
        canvas.transform.GetChild(5).GetChild(3).GetChild(2).gameObject.SetActive(true);
        OyunHız = 0;
        OyunAktif = false;

    }

    // Butonlar
    public void SonrakiBölüm(){
        PlayerPrefs.SetInt("SeçiliBölüm",kayıtlıseviye);
        PlayerPrefs.SetInt("SonSeviye", kayıtlıseviye);
        SceneManager.LoadScene("0", LoadSceneMode.Single);
    }
    public void AnaMenü(){
        SceneManager.LoadScene("AnaMenü", LoadSceneMode.Single);
    }
    public void Tekrarla(){
        SceneManager.LoadScene("0", LoadSceneMode.Single);
    }

    public void TurretİadeEtme(){
        TurretSpawn.GetComponent<TurretSpawn>().TurretSil();
    }
    public void Turretİadeİptal(){
        TurretSpawn.GetComponent<TurretSpawn>().TurretSilİptal();
    }
    public void TurretGeliştir(){
        TurretSpawn.GetComponent<TurretSpawn>().TurretGeliştir();
    }
}
