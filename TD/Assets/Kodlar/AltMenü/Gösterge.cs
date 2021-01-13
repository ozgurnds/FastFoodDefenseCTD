using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gösterge : MonoBehaviour
{
    public Text can, su, para;
    EventSystem ev;

    void Start()
    {
        ev = GameObject.FindGameObjectWithTag("EventSystem").GetComponent<EventSystem>();
    }  
    void Update()
    {
        can.text = ev.can + "";
        su.text = ev.su + "";
        para.text = ev.para + "";
    }
}
