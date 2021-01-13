using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Turretler
{
    [Header("Turret Bilgiler")]
    public string Turretİsim;
    public int TurretHasar,TurretSeviye,Turretİd;
    public float TurretAteş;
    public Yetenek TurretYetenek;

    [Header("Satın Alma")]
    public int para;
    public int su;
    public int iade;
    public int sonrakiid;

    public enum Yetenek
    {
        Yok,
        Patates,
        AcıBiber,
        Nar

    }


}
