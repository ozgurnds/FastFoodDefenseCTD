using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dusman
{
    public string DusmanIsim;
    public int DusmanSeviye,Dusmanİd,DusmanPara;
    public float DusmanCan,DusmanHiz;
    public Yetenek DusmanYetenek;

    public enum Yetenek {
    
        Yok,
        PatatesKızartması,
        Dondurma,
        Donut,
        Çikolata
    }
}
