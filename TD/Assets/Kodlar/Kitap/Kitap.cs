using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kitap : MonoBehaviour
{
    public GameObject Dusmanlar, Turretler,KitapMenü;
    public void DusmanMenüAç(){
        Dusmanlar.SetActive(true);
        Turretler.SetActive(false);
        KitapMenü.SetActive(false);
    }
    public void DusmanMenüKapat(){
        Dusmanlar.SetActive(false);
        Turretler.SetActive(false);
        KitapMenü.SetActive(true);
    }

    public void TurretMenüAç()
    {
        Dusmanlar.SetActive(false);
        Turretler.SetActive(true);
        KitapMenü.SetActive(false);
    }
    public void TurretMenüKapat()
    {
        Dusmanlar.SetActive(false);
        Turretler.SetActive(false);
        KitapMenü.SetActive(true);
    }
}
