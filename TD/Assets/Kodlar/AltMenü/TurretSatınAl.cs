using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretSatınAl : MonoBehaviour
{
    EventSystem ev;
    public GameObject TurretKonum;


    void Start()
    {
        ev = GameObject.FindGameObjectWithTag("EventSystem").GetComponent<EventSystem>();
    }
    public void SatınAl(int id){
        if(ev.TurretData[id].para <= ev.para && ev.TurretData[id].su <= ev.su)
        {
            TurretKonum.GetComponent<TurretSpawn>().TurretOluştur(id);
            ev.para -= ev.TurretData[id].para;
            ev.su -= ev.TurretData[id].su;
        }
    }

}
