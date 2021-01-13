using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


public class Dusmanlar : MonoBehaviour
{
    //Veriler
    public int checkpointsayac;
    public Dusman veri;
    private EventSystem ev;
    float sayac, sayac1;
    public bool özellikaktif, slowaktif, zehiraktif;
    int zehirdeğer, zehirhasar, cikolatadeger;
    public float checkpointmesafe;
    //------------------------------

    void Start()
    {
        ev = GameObject.FindGameObjectWithTag("EventSystem").GetComponent<EventSystem>();
        ev.Düsman++;
    }

    void Update()
    {

        if (ev.OyunAktif)
        {
            Hareket();
            CanKontrol();
            sayac += Time.deltaTime;
            if (zehiraktif)
            {
                if (zehirdeğer < 5)
                {
                    Invoke("ZehirCan", zehirdeğer);
                    zehirdeğer++;
                }
                else
                {
                    zehiraktif = false;
                    zehirdeğer = 0;
                }
            }
            if (veri.DusmanYetenek == Dusman.Yetenek.PatatesKızartması && !özellikaktif)
            {
                özellikaktif = true;
                int deger = UnityEngine.Random.Range(2, 6);
                InvokeRepeating("PatatesKızartmasıYetenek", deger, 5);
            }
            if (veri.DusmanYetenek == Dusman.Yetenek.Çikolata && !özellikaktif)
            {
                özellikaktif = true;
                InvokeRepeating("ÇikolataYetenek", 8, 1);
            }
        }
        if (checkpointsayac + 1 >= ev.Bölümler[ev.Bölüm].transform.childCount)
        {
            ev.CanDeğiştir(1);
            ev.Düsman--;
            Destroy(gameObject);
        }
        else
        {
            checkpointmesafe = Vector3.Distance(ev.Bölümler[ev.Bölüm].transform.GetChild(checkpointsayac + 1).transform.position, transform.position);
        }
    }

    public void DusmanSpawn()
    {
        ev = GameObject.FindGameObjectWithTag("EventSystem").GetComponent<EventSystem>();
        gameObject.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("T" + veri.DusmanIsim);
        gameObject.transform.GetChild(1).GetComponent<Image>().sprite = Resources.Load<Sprite>("y" + veri.DusmanSeviye);
    }

    //Canı Varmı-----
    void CanKontrol()
    {
        float cankademe1 = ((ev.DusmanData[veri.Dusmanİd].DusmanCan / 100) * 75);
        float cankademe2 = ((ev.DusmanData[veri.Dusmanİd].DusmanCan / 100) * 25);
        float canbaruzunluk = (90 * (veri.DusmanCan / ev.DusmanData[veri.Dusmanİd].DusmanCan));
        gameObject.transform.GetChild(2).GetComponent<RectTransform>().sizeDelta = new Vector2(canbaruzunluk, 10);
        if (veri.DusmanCan >= cankademe1)
        {
            gameObject.transform.GetChild(2).GetComponent<Image>().color = new Color32(0, 255, 0, 255);
        }
        else if (veri.DusmanCan <= cankademe1 && veri.DusmanCan >= cankademe2)
        {
            gameObject.transform.GetChild(2).GetComponent<Image>().color = new Color32(255, 150, 0, 255);
        }
        else if (veri.DusmanCan <= cankademe2 && veri.DusmanCan > 0)
        {
            gameObject.transform.GetChild(2).GetComponent<Image>().color = new Color32(255, 0, 0, 255);
        }
        else if (veri.DusmanCan <= 0)
        {
            ev.para += veri.DusmanPara;
            ev.Düsman--;
            Ölme();
            Destroy(gameObject);
        }
    }
    //---------------


    void Ölme()
    {

        GameObject ölme = (GameObject)Instantiate(Resources.Load("Ölme"), transform.position, transform.rotation);
        ölme.transform.parent = ev.canvas.transform.GetChild(3).transform;
        ölme.transform.SetAsLastSibling();
        ölme.transform.position = transform.position;
        ölme.GetComponent<ÖlüDüsman>().yazı.text = "+" + veri.DusmanPara;
        ölme.GetComponent<ÖlüDüsman>().yükseklik = ölme.transform.position.y;
        ölme.transform.localScale = new Vector3(1, 1, 1);
    }

    //Dusman Yetenekleri-----------------
    public void CanAzaltma(float hasar, GameObject turret)
    {
        if (veri.DusmanYetenek != Dusman.Yetenek.Donut)
        {
            veri.DusmanCan -= hasar;
        }
        else
        {
            DonutYetenek(hasar);
        }
        if (veri.DusmanYetenek == Dusman.Yetenek.Dondurma)
        {
            DondurmaYetenek(turret);
        }
    }

    public void DonutYetenek(float hasar)
    {
        int değer = UnityEngine.Random.Range(0, 100);
        if (veri.DusmanSeviye == 1) { if (değer <= 30) { } else { veri.DusmanCan -= hasar; } }
        else if (veri.DusmanSeviye == 2) { if (değer <= 40) { } else { veri.DusmanCan -= hasar; } }
        else if (veri.DusmanSeviye == 3) { if (değer <= 50) { } else { veri.DusmanCan -= hasar; } }
    }
    public void PatatesKızartmasıYetenek()
    {
        GameObject yağ = (GameObject)Instantiate(Resources.Load("Yağ"), transform.position, new Quaternion(0, 0, 0, 0));
        yağ.transform.parent = ev.canvas.transform.GetChild(3).transform;
        yağ.transform.SetSiblingIndex(0);
        yağ.GetComponent<Yağ>().ev = ev;
        yağ.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        if (veri.DusmanSeviye == 1) { yağ.GetComponent<Yağ>().hız = 1.5f; }
        else if (veri.DusmanSeviye == 2) { yağ.GetComponent<Yağ>().hız = 1.75f; }
        else if (veri.DusmanSeviye == 3) { yağ.GetComponent<Yağ>().hız = 2f; }
    }
    public void DondurmaYetenek(GameObject turret)
    {
        if (!turret.GetComponent<Turret>().dondurma)
        {
            turret.GetComponent<Turret>().dondurmadeger = veri.DusmanSeviye * 0.5f;
            turret.GetComponent<Turret>().dondurma = true;
        }
    }
    public void ÇikolataYetenek()
    {
        veri.DusmanHiz = 0;
        if (ev.OyunHız != 0)
        {
            if (cikolatadeger != 3)
            {
                int id = 20 + veri.DusmanSeviye;
                GameObject dusman = (GameObject)Instantiate(Resources.Load<GameObject>("Düşman"), transform.position, new Quaternion(0, 0, 0, 0));
                dusman.transform.SetParent(ev.canvas.transform.GetChild(3).transform);
                dusman.transform.position = transform.position;
                dusman.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
                dusman.GetComponent<Dusmanlar>().veri.DusmanIsim = ev.DusmanData[id].DusmanIsim;
                dusman.GetComponent<Dusmanlar>().veri.DusmanCan = ev.DusmanData[id].DusmanCan;
                dusman.GetComponent<Dusmanlar>().veri.DusmanSeviye = ev.DusmanData[id].DusmanSeviye;
                dusman.GetComponent<Dusmanlar>().veri.Dusmanİd = ev.DusmanData[id].Dusmanİd;
                dusman.GetComponent<Dusmanlar>().veri.DusmanPara = ev.DusmanData[id].DusmanPara;
                dusman.GetComponent<Dusmanlar>().veri.DusmanHiz = ev.DusmanData[id].DusmanHiz;
                dusman.GetComponent<Dusmanlar>().veri.DusmanYetenek = ev.DusmanData[id].DusmanYetenek;
                dusman.GetComponent<Dusmanlar>().checkpointsayac = checkpointsayac;
                dusman.GetComponent<Dusmanlar>().DusmanSpawn();
                cikolatadeger++;
            }
            else
            {
                cikolatadeger = 0;
                CancelInvoke("ÇikolataYetenek");
                özellikaktif = false;
                veri.DusmanHiz = ev.DusmanData[veri.Dusmanİd].DusmanHiz;
            }
        }
    }
    //------------------------------------

    //Turret Yetenek Eklenti-------
    public void Slow(float değer)
    {
        if (!slowaktif)
        {
            if (veri.DusmanHiz - değer <= 0)
            {
                veri.DusmanHiz = 0;
                slowaktif = true;
                Invoke("Slowlama", 3);
            }
            else
            {
                veri.DusmanHiz -= değer;
                slowaktif = true;
                Invoke("Slowlama", 3);
            }
        }
    }
    void Slowlama()
    {
        slowaktif = false;
        veri.DusmanHiz = ev.DusmanData[veri.Dusmanİd].DusmanHiz;
    }
    public void Zehir(int değer)
    {
        zehirhasar = değer;
        sayac1 = sayac + 5;
        zehiraktif = true;
    }
    void ZehirCan()
    {
        CanAzaltma(zehirhasar, null);
        zehirdeğer++;
    }
    //-----------------------------

    //Hareket Etme
    void Hareket()
    {
        if (checkpointsayac + 1 < ev.Bölümler[ev.Bölüm].transform.childCount && checkpointsayac > -1 && ev.Bölümler[ev.Bölüm].transform.childCount > 0)
        {
            transform.position = Vector2.MoveTowards(transform.position, ev.Bölümler[ev.Bölüm].transform.GetChild(checkpointsayac + 1).transform.position, veri.DusmanHiz * Time.deltaTime * ev.OyunHız);
        }
    }
    //------------
}
