using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TurretSpawn : MonoBehaviour, IPointerDownHandler
{
    public bool TurretAktif,basıldı,iade,geliştir,geliştir1;
    Turret Turret;
    float uzunluk;
    EventSystem ev;

    //Basma Tetik----------------------------------------
    public void OnPointerDown(PointerEventData eventData)
    {
        
            if (!TurretAktif)//Slotta Turret Var mı?
            {
                TurretÖzellikleriKapat();
                ev.AltMenu.transform.GetChild(1).gameObject.SetActive(true);
                ev.AltMenu.transform.GetChild(1).GetComponent<TurretSatınAl>().TurretKonum = this.gameObject;
                ev.Kapama.MenüAktif = true;
                ev.TurretSpawn = this.gameObject;
                basıldı = true;
            }
            else
            {
                TurretÖzelliklerAç();
            ev.AltMenu.transform.GetChild(1).gameObject.SetActive(false);
            ev.Kapama.MenüAktif = true;
                ev.TurretSpawn = this.gameObject;
                basıldı = true;
                iade = false;
                ev.AltMenu.transform.GetChild(0).GetChild(7).gameObject.SetActive(false);
            }
    }
    //---------------------------------------------------
    
    //TurretÖzellikleri Aç Kapat--------
    public void TurretÖzelliklerAç(){
        Turret.transform.GetChild(1).gameObject.SetActive(true);

        //Turret Özellikleri Menü Aktif Et
        ev.AltMenu.transform.GetChild(0).gameObject.SetActive(true);
        ev.AltMenu.transform.GetChild(0).GetComponent<TurretÖzellikiler>().Isim.text = "İsim: " + Turret.veri.Turretİsim;
        ev.AltMenu.transform.GetChild(0).GetComponent<TurretÖzellikiler>().Ateş.text = "Ateş Hızı: " + Turret.veri.TurretAteş + " Saniye";
        ev.AltMenu.transform.GetChild(0).GetComponent<TurretÖzellikiler>().Hasar.text = "Hasar: " + Turret.veri.TurretHasar;
        ev.AltMenu.transform.GetChild(0).GetComponent<TurretÖzellikiler>().Resim.sprite = Resources.Load<Sprite>("T" + Turret.veri.Turretİsim);
        ev.AltMenu.transform.GetChild(0).GetComponent<TurretÖzellikiler>().Yıldız.sprite = Resources.Load<Sprite>("y"+Turret.veri.TurretSeviye);
        if (Turret.veri.sonrakiid != -1){
            ev.AltMenu.transform.GetChild(0).GetComponent<TurretÖzellikiler>().GeliştirmePara.text = "Para:" + ev.TurretData[Turret.veri.sonrakiid].para;
            ev.AltMenu.transform.GetChild(0).GetComponent<TurretÖzellikiler>().GeliştirmeSu.text = "Su:" + ev.TurretData[Turret.veri.sonrakiid].su;
            ev.AltMenu.transform.GetChild(0).GetComponent<TurretÖzellikiler>().MaxLevel.enabled = false;
        }
        else { ev.AltMenu.transform.GetChild(0).GetComponent<TurretÖzellikiler>().MaxLevel.enabled = true; }
        ev.AltMenu.transform.GetChild(0).GetComponent<TurretÖzellikiler>().GeliştirmeButonu.sprite = Resources.Load<Sprite>("1");
        geliştir = false;
        geliştir1 = false;
        uzunluk = 0;
        ev.AltMenu.transform.GetChild(0).GetComponent<TurretÖzellikiler>().GeliştirmeArkaPlan.rectTransform.sizeDelta = new Vector3(0, 75);
        ev.AltMenu.transform.GetChild(0).GetComponent<TurretÖzellikiler>().GeliştirmeYazıları.SetActive(false);
    }
    public void TurretÖzellikleriKapat(){

        if (TurretAktif == true)
        {
            Turret.transform.GetChild(1).gameObject.SetActive(false);
        }
        ev.AltMenu.transform.GetChild(0).gameObject.SetActive(false);
        ev.AltMenu.transform.GetChild(0).GetComponent<TurretÖzellikiler>().Isim.text = null;
        ev.AltMenu.transform.GetChild(0).GetComponent<TurretÖzellikiler>().Ateş.text = null;
        ev.AltMenu.transform.GetChild(0).GetComponent<TurretÖzellikiler>().Hasar.text = null;
        ev.AltMenu.transform.GetChild(0).GetComponent<TurretÖzellikiler>().Resim.sprite = null;
        ev.TurretSpawn = null;
        geliştir = false;
        geliştir1 = false;
        uzunluk = 0;
        ev.AltMenu.transform.GetChild(0).GetComponent<TurretÖzellikiler>().GeliştirmeArkaPlan.rectTransform.sizeDelta = new Vector3(0, 75);
        ev.AltMenu.transform.GetChild(0).GetComponent<TurretÖzellikiler>().GeliştirmeYazıları.SetActive(false);
    }
    //----------------------------------

    void Start()
    {
        ev = GameObject.FindGameObjectWithTag("EventSystem").GetComponent<EventSystem>();
    }

    
    void Update()
    {
            if (ev.Kapama.TurretAktif && basıldı)
            {
                TurretÖzellikleriKapat();
                ev.AltMenu.transform.GetChild(1).gameObject.SetActive(false);
                ev.Kapama.TurretAktif = false;
                basıldı = false;
            }
            if (geliştir1 && uzunluk < 400 && geliştir)
            {
                uzunluk += 750 * Time.deltaTime;
                ev.AltMenu.transform.GetChild(0).GetComponent<TurretÖzellikiler>().GeliştirmeArkaPlan.rectTransform.sizeDelta = new Vector2(uzunluk, 75);
                if (uzunluk > 390) { ev.AltMenu.transform.GetChild(0).GetComponent<TurretÖzellikiler>().GeliştirmeYazıları.SetActive(true); }
            }
            else { geliştir1 = false; }
    }
    
    //Turret Oluşturma ve YokEtme----
    public void TurretOluştur(int id){
       GameObject turret = (GameObject)Instantiate(Resources.Load<GameObject>("Turret"), this.gameObject.transform.position, new Quaternion(0, 0, 0,0));
        turret.transform.SetParent(ev.canvas.transform.GetChild(4).transform);
        turret.transform.localScale = new Vector3(1, 1, 1);
        Turret = turret.GetComponent<Turret>();
        turret.GetComponent<Turret>().veri.Turretİsim = ev.TurretData[id].Turretİsim;
        turret.GetComponent<Turret>().veri.TurretHasar = ev.TurretData[id].TurretHasar;
        turret.GetComponent<Turret>().veri.TurretSeviye = ev.TurretData[id].TurretSeviye;
        turret.GetComponent<Turret>().veri.Turretİd = ev.TurretData[id].Turretİd;
        turret.GetComponent<Turret>().veri.TurretAteş = ev.TurretData[id].TurretAteş;
        turret.GetComponent<Turret>().veri.TurretYetenek = ev.TurretData[id].TurretYetenek;
        turret.GetComponent<Turret>().veri.para = ev.TurretData[id].para;
        turret.GetComponent<Turret>().veri.su = ev.TurretData[id].su;
        turret.GetComponent<Turret>().veri.iade = ev.TurretData[id].iade;
        turret.GetComponent<Turret>().veri.sonrakiid = ev.TurretData[id].sonrakiid;
        TurretAktif = true;
        ev.AltMenu.transform.GetChild(1).gameObject.SetActive(false);
        TurretÖzelliklerAç();
        ev.Kapama.MenüAktif = true;
    }
    public void TurretSil(){
        if (iade)
        {
            iade = false;
            ev.AltMenu.transform.GetChild(0).GetChild(7).gameObject.SetActive(false);
            ev.para += Turret.veri.iade;
            TurretAktif = false;
            Destroy(Turret.gameObject);
            basıldı = false;
            ev.Kapama.MenüAktif = false;
            TurretÖzellikleriKapat();
        }
        else
        {
            iade = true;
            ev.AltMenu.transform.GetChild(0).GetChild(7).gameObject.SetActive(true);
            ev.AltMenu.transform.GetChild(0).GetChild(7).gameObject.transform.GetChild(0).GetComponent<Text>().text = "İade:" + Turret.veri.iade;
        }
    }
    public void TurretSilİptal(){
        iade = false;
        ev.AltMenu.transform.GetChild(0).GetChild(7).gameObject.SetActive(false);
    }
    public void TurretGeliştir()
    {
        if (Turret.veri.sonrakiid > 0)
        {
            if (ev.TurretData[Turret.veri.sonrakiid].para <= ev.para && ev.TurretData[Turret.veri.sonrakiid].su <= ev.su && geliştir)
            {
                ev.para -= ev.TurretData[Turret.veri.sonrakiid].para;
                ev.su -= ev.TurretData[Turret.veri.sonrakiid].su;
                Turret.veri = ev.TurretData[Turret.veri.sonrakiid];
                Turret.transform.GetChild(2).GetComponent<Image>().sprite = Resources.Load<Sprite>("y"+Turret.veri.TurretSeviye);
                TurretÖzelliklerAç();
                geliştir = false;
                geliştir1 = false;
            }
            else {
                if (!geliştir)
                {
                    geliştir1 = true;
                }
                geliştir = true;
                ev.AltMenu.transform.GetChild(0).GetComponent<TurretÖzellikiler>().GeliştirmeButonu.sprite = Resources.Load<Sprite>("2");
                ev.AltMenu.transform.GetChild(0).GetComponent<TurretÖzellikiler>().GeliştirmePara.text = "Para" + ev.TurretData[Turret.veri.sonrakiid].para;
                ev.AltMenu.transform.GetChild(0).GetComponent<TurretÖzellikiler>().GeliştirmeSu.text = "Su" + ev.TurretData[Turret.veri.sonrakiid].su;
            }
        }
    }
    //-------------------------------
}
