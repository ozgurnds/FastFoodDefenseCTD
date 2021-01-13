using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Yağ : MonoBehaviour
{
    public float hız;
    public EventSystem ev;
    bool yokolma;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Dusman")
        {
            collision.GetComponent<Dusmanlar>().veri.DusmanHiz = collision.GetComponent<Dusmanlar>().veri.DusmanHiz * hız;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Dusman"){
            collision.GetComponent<Dusmanlar>().veri.DusmanHiz = ev.DusmanData[collision.GetComponent<Dusmanlar>().veri.Dusmanİd].DusmanHiz;
        }
    }
    public void Update()
    {
        if(!yokolma){
            Invoke("YokOlma",5);
            yokolma = true;
        }
    }
    public void YokOlma(){
        Destroy(gameObject);
    }
}
