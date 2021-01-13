using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ÖlüDüsman : MonoBehaviour
{
    public Text yazı;
    public float yükseklik;
    
    public void YokOl(){
        Destroy(gameObject);
    }
    void Update()
    {
        transform.Translate(new Vector3(0,1 * Time.deltaTime,0));
        Invoke("YokOl",0.5f);
    }
}
