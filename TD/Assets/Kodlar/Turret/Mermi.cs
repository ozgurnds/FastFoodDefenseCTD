using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mermi : MonoBehaviour
{
    public GameObject hedef,turret;
    public int hasar,zehir;
    public float slow;
    EventSystem ev;
    private void Start()
    {
        ev = GameObject.FindGameObjectWithTag("EventSystem").GetComponent<EventSystem>();
    }
    void Update()
    {
        if(ev.OyunAktif){
            if (hedef == null) { Destroy(gameObject); } else
            {
                transform.position = Vector2.MoveTowards(transform.position, hedef.transform.position, 10 * Time.deltaTime * ev.OyunHız);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == hedef)
        {
            collision.GetComponent<Dusmanlar>().CanAzaltma(hasar,turret);
            if(zehir != 0 && !collision.GetComponent<Dusmanlar>().zehiraktif){
                collision.GetComponent<Dusmanlar>().Zehir(zehir);
            }
            if(slow != 0 && !collision.GetComponent<Dusmanlar>().slowaktif){
                collision.GetComponent<Dusmanlar>().Slow(slow);
            }
            if(hedef.GetComponent<Dusmanlar>().veri.DusmanYetenek == Dusman.Yetenek.Dondurma && !turret.GetComponent<Turret>().dondurma){
                turret.GetComponent<Turret>().veri.TurretAteş += hedef.GetComponent<Dusmanlar>().veri.DusmanSeviye * 0.5f;
                turret.GetComponent<Turret>().dondurma = true;
            }
            Destroy(gameObject);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject == hedef)
        {
            //Destroy(gameObject);
        }
    }
}
