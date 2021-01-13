using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenüKapama : MonoBehaviour, IPointerDownHandler
{
    public bool MenüAktif,TurretAktif;

    //Basma Tetik----------------------------------------
    public void OnPointerDown(PointerEventData eventData)
    {
        if (MenüAktif)
        {
            TurretAktif = true;
            MenüAktif = false;
        }
    }
    //---------------------------------------------------

    void Update()
    {
        if (MenüAktif)
        {
            GetComponent<Image>().raycastTarget = true;
        }
        else
        {
            GetComponent<Image>().raycastTarget = false;
        }
    }
}
