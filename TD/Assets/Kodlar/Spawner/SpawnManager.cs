using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnManager : MonoBehaviour
{
    public List<Bölüm> Bölümler;
    float sayac;
    bool SpawnAktif, DalgaAktif, DalgaGöster, DalgaGöster1;
    EventSystem ev;
    float dalgagörünüm, baslangiczamani;
    int spawnadet;



    void Start()
    {
        ev = GameObject.FindGameObjectWithTag("EventSystem").GetComponent<EventSystem>();
        ev.Bölümler[ev.Bölüm].SetActive(true);
        for (int i = 0; i < ev.Bölüm; i++)
        {
            ev.Bölümler[i].SetActive(false);
        }
        for (int i = ev.Bölümler.Length - 1; i > ev.Bölüm; i--)
        {
            ev.Bölümler[i].SetActive(false);
        }
        for (int i = 0; i < Bölümler[ev.Bölüm].Dalgalar.Count; i++)
        {
            for (int a = 1; a < Bölümler[ev.Bölüm].Dalgalar[i].carpan; a++)
            {
                if (Bölümler[ev.Bölüm].Dalgalar[i].carpan != 1)
                {
                    for (int b = 0; b < Bölümler[ev.Bölüm].Dalgalar[i].adet; b++)
                    {
                        Bölümler[ev.Bölüm].Dalgalar[i].Dusmanlar.Add(new Spawn());
                        Bölümler[ev.Bölüm].Dalgalar[i].Dusmanlar[Bölümler[ev.Bölüm].Dalgalar[i].Dusmanlar.Count - 1].İd = Bölümler[ev.Bölüm].Dalgalar[i].Dusmanlar[b].İd;
                        Bölümler[ev.Bölüm].Dalgalar[i].Dusmanlar[Bölümler[ev.Bölüm].Dalgalar[i].Dusmanlar.Count - 1].Adet = Bölümler[ev.Bölüm].Dalgalar[i].Dusmanlar[b].Adet;
                    }
                }
            }
        }
        for (int i = 0; i < Bölümler[ev.Bölüm].Dalgalar.Count; i++)
        {
            for (int a = 0; a < Bölümler[ev.Bölüm].Dalgalar[i].Dusmanlar.Count; a++)
            {
                Bölümler[ev.Bölüm].Dalgalar[i].Dusmanlar[a].BaslangicZamani = baslangiczamani;
                baslangiczamani += Bölümler[ev.Bölüm].Dalgalar[i].Dusmanlar[a].Adet * 2;
            }
            baslangiczamani = 0;
        }
        ev.para = Bölümler[ev.Bölüm].BaslangicPara;
        ev.su = Bölümler[ev.Bölüm].BaslangicSu;
        ev.can = 10;
    }
    void Update()
    {
        if (ev.OyunAktif)
        {
            if (ev.DalgaSira + 1 == Bölümler[ev.Bölüm].Dalgalar.Count && ev.Düsman == 0 && Bölümler[ev.Bölüm].Dalgalar[ev.DalgaSira].Dusmanlar[ev.DüsmanSira].Bitti)
            {
                ev.Kazan();
            }
            DalgaSpawn();
            if (DalgaGöster && dalgagörünüm < 255)
            {
                dalgagörünüm += Time.deltaTime * 100;
                Color32 renk = new Color(0, 0, 0, dalgagörünüm);
                ev.canvas.transform.GetChild(5).GetChild(0).GetComponent<Text>().color = new Color32(0, 0, 0, (byte)(dalgagörünüm));
            }
            else if (dalgagörünüm >= 255)
            {
                DalgaGöster = false;
                DalgaGöster1 = true;
            }
            if (DalgaGöster1 && dalgagörünüm > 0)
            {
                dalgagörünüm -= Time.deltaTime * 100;
                ev.canvas.transform.GetChild(5).GetChild(0).GetComponent<Text>().color = new Color32(0, 0, 0, (byte)(dalgagörünüm));
            }
            else if (dalgagörünüm <= 0)
            {
                DalgaGöster1 = false;
                ev.canvas.transform.GetChild(5).GetChild(0).GetComponent<Text>().enabled = false;

            }
        }
    }


    public void DusmanSpawn()
    {
        if (ev.OyunHız != 0 && ev.OyunAktif)
        {
            int id = Bölümler[ev.Bölüm].Dalgalar[ev.DalgaSira].Dusmanlar[ev.DüsmanSira].İd;
            GameObject dusman = (GameObject)Instantiate(Resources.Load<GameObject>("Düşman"), ev.Bölümler[ev.Bölüm].transform.GetChild(0).transform.position, new Quaternion(0, 0, 0, 0));
            dusman.transform.SetParent(ev.canvas.transform.GetChild(3).transform);
            dusman.transform.position = ev.Bölümler[ev.Bölüm].transform.GetChild(0).transform.position;
            dusman.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            dusman.GetComponent<Dusmanlar>().veri.DusmanIsim = ev.DusmanData[id].DusmanIsim;
            dusman.GetComponent<Dusmanlar>().veri.DusmanCan = ev.DusmanData[id].DusmanCan;
            dusman.GetComponent<Dusmanlar>().veri.DusmanSeviye = ev.DusmanData[id].DusmanSeviye;
            dusman.GetComponent<Dusmanlar>().veri.Dusmanİd = ev.DusmanData[id].Dusmanİd;
            dusman.GetComponent<Dusmanlar>().veri.DusmanPara = ev.DusmanData[id].DusmanPara;
            dusman.GetComponent<Dusmanlar>().veri.DusmanHiz = ev.DusmanData[id].DusmanHiz;
            dusman.GetComponent<Dusmanlar>().veri.DusmanYetenek = ev.DusmanData[id].DusmanYetenek;
            dusman.GetComponent<Dusmanlar>().DusmanSpawn();
            spawnadet++;
            if (spawnadet >= Bölümler[ev.Bölüm].Dalgalar[ev.DalgaSira].Dusmanlar[ev.DüsmanSira].Adet) { Bölümler[ev.Bölüm].Dalgalar[ev.DalgaSira].Dusmanlar[ev.DüsmanSira].Bitti = true; SpawnAktif = false; spawnadet = 0; CancelInvoke("DusmanSpawn"); }
        }
    }
    public void DalgaSpawn()
    {
        if (ev.DüsmanSira + 1 != Bölümler[ev.Bölüm].Dalgalar[ev.DalgaSira].Dusmanlar.Count && Bölümler[ev.Bölüm].Dalgalar[ev.DalgaSira].Dusmanlar[ev.DüsmanSira].Bitti)
        {
            ev.DüsmanSira++;
        }
        if (ev.Düsman == 0 && Bölümler[ev.Bölüm].Dalgalar[ev.DalgaSira].Dusmanlar.Count == ev.DüsmanSira + 1 && !DalgaAktif && Bölümler[ev.Bölüm].Dalgalar.Count != ev.DalgaSira + 1 && Bölümler[ev.Bölüm].Dalgalar[ev.DalgaSira].Dusmanlar[ev.DüsmanSira].Bitti)
        {
            Invoke("SonrakiDalgayıBaşlat", 5);
            ev.canvas.transform.GetChild(5).GetChild(0).GetComponent<Text>().text = "Dalga " + (ev.DalgaSira + 2);
            ev.canvas.transform.GetChild(5).GetChild(0).GetComponent<Text>().enabled = true;
            DalgaGöster = true;
            DalgaAktif = true;
        }
        sayac = sayac + Time.deltaTime * ev.OyunHız;
        if (Bölümler[ev.Bölüm].Dalgalar[ev.DalgaSira].Dusmanlar[ev.DüsmanSira].BaslangicZamani < sayac && Bölümler[ev.Bölüm].Dalgalar[ev.DalgaSira].Dusmanlar.Count != ev.DüsmanSira && !Bölümler[ev.Bölüm].Dalgalar[ev.DalgaSira].Dusmanlar[ev.DüsmanSira].Bitti && !SpawnAktif)
        {

            InvokeRepeating("DusmanSpawn", 0, 2 / ev.OyunHız);
            SpawnAktif = true;
        }
    }
    public void SonrakiDalgayıBaşlat()
    {
        ev.DalgaSira += 1;
        ev.DüsmanSira = 0;
        sayac = 0;
        DalgaAktif = false;
    }
}

