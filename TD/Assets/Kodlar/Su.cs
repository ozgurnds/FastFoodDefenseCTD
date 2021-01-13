using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Su : MonoBehaviour, IPointerClickHandler
{
    EventSystem ev;
    public void OnPointerClick(PointerEventData eventData)
    {
        if (ev.OyunAktif){
            ev.su += 5;
        }
    }

    public void Start()
    {
        ev = GameObject.FindGameObjectWithTag("EventSystem").GetComponent<EventSystem>();
    }
}
