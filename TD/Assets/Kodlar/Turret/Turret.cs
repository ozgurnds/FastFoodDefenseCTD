using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Turret : MonoBehaviour
{
    //Turret Veriler
    public Turretler veri;
    float sayac;
    public List<GameObject> dusmanlar;
    GameObject hedef;
    EventSystem ev;
    int nar;
    public bool dondurma;
    float hedefmesafe;
    int hedefcheckpoint;
    float hedefdeğer = 1000;
    int hedefcheckpointsayac;
    public float dondurmadeger;
    void Start()
    {
        ev = GameObject.FindGameObjectWithTag("EventSystem").GetComponent<EventSystem>();
        TurretSpawn();
    }

    void Update()
    {
        if (ev.OyunAktif)
        {
            //Hedef Kitlenme----------
            if (dusmanlar.Count > 0)
            {
                for (int i = 0; i < dusmanlar.Count; i++)
                {
                    if (dusmanlar[i] == null)
                    {
                        dusmanlar.RemoveAt(i);
                    }
                    hedefmesafe = dusmanlar[i].GetComponent<Dusmanlar>().checkpointmesafe;
                    hedefcheckpoint = dusmanlar[i].GetComponent<Dusmanlar>().checkpointsayac;
                    if (i != 0)
                    {
                        if (hedefcheckpointsayac < hedefcheckpoint)
                        {
                            if (hedefdeğer > hedefmesafe)
                            {
                                hedef = dusmanlar[i];
                                hedefdeğer = hedefmesafe;
                                hedefcheckpointsayac = hedefcheckpoint;
                            }
                        }
                        else if(hedefcheckpointsayac == hedefcheckpoint)
                        {
                            if (hedefdeğer > hedefmesafe)
                            {
                                hedef = dusmanlar[i];
                                hedefdeğer = hedefmesafe;
                                hedefcheckpointsayac = hedefcheckpoint;
                            }
                        }
                    }
                    else
                    {
                        hedef = dusmanlar[0];
                    }
                }
            }
            //-----------------------
            //AteşEtme-----------------
            sayac += Time.deltaTime * ev.OyunHız;
            if (sayac > veri.TurretAteş && hedef != null && !dondurma)
            {
                AteşEtme();
                sayac = 0;
            }
            else if (sayac > veri.TurretAteş + dondurmadeger && dondurma && hedef != null)
            {
                AteşEtme();
                Invoke("Donma", 1);
                sayac = 0;
            }
            //---------------------------
        }
    }


    //TurretSpawn BirKez Çalışır
    public void TurretSpawn()
    {
        gameObject.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("T" + veri.Turretİsim);
        gameObject.transform.GetChild(2).GetComponent<Image>().sprite = Resources.Load<Sprite>("y" + veri.TurretSeviye);
    }
    //--------------------------

    //Ateş Etme------------
    private void AteşEtme()
    {
        GameObject Mermi = (GameObject)Instantiate(Resources.Load<GameObject>("Mermi"), transform.position, transform.rotation);
        Mermi.GetComponent<Mermi>().hasar = veri.TurretHasar;
        Mermi.GetComponent<Image>().sprite = Resources.Load<Sprite>("T" + veri.Turretİsim);
        Mermi.GetComponent<Mermi>().hedef = hedef;
        Mermi.GetComponent<Mermi>().turret = this.gameObject;
        Mermi.transform.SetParent(ev.canvas.transform);
        Mermi.transform.localScale = new Vector3(1, 1, 1);
        if (veri.TurretYetenek == Turretler.Yetenek.Patates)
        {
            if (veri.TurretSeviye == 1) { Mermi.GetComponent<Mermi>().slow = 0.2f; }
            else if (veri.TurretSeviye == 2) { Mermi.GetComponent<Mermi>().slow = 0.3f; }
            else if (veri.TurretSeviye == 3) { Mermi.GetComponent<Mermi>().slow = 0.4f; }
        }
        if (veri.TurretYetenek == Turretler.Yetenek.AcıBiber)
        {
            Mermi.GetComponent<Mermi>().zehir = 10 * veri.TurretSeviye;
        }
        if (veri.TurretYetenek == Turretler.Yetenek.Nar)
        {
            if (veri.TurretSeviye == 1) { Mermi.GetComponent<Mermi>().hasar += (nar * 3); }
            else if (veri.TurretSeviye == 2) { Mermi.GetComponent<Mermi>().hasar += (nar * 4); }
            else if (veri.TurretSeviye == 3) { Mermi.GetComponent<Mermi>().hasar += (nar * 5); }
            nar++;
        }
    }
    //---------------------

    private void Donma()
    {
        float degisken = ev.TurretData[veri.Turretİd].TurretAteş;
        veri.TurretAteş = degisken;
        dondurma = false;
    }

    //Dusman Algılama------------------------------
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.tag == "Dusman")
        {
            dusmanlar.Add(other.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Dusman")
        {
            nar = 0;
            dusmanlar.Remove(other.gameObject);
            if (dusmanlar.Count > 0)
            {
                hedef = dusmanlar[0];
            }
            else
            {
                hedef = null;
            }
        }
    }
    //---------------------------------------------
}
