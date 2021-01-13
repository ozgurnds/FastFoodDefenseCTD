using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AnaMenü : MonoBehaviour
{
    public GameObject AnaMenüObje, BölümlerObje;


    public void BölümAc(int bölüm){
        PlayerPrefs.SetInt("SeçiliBölüm", bölüm);
        SceneManager.LoadScene("0", LoadSceneMode.Single);
    }
    public void AnaMenüyeGit()
    {
        BölümlerObje.SetActive(false);
        AnaMenüObje.SetActive(true);
    }
    public void BölümlerGit(){
        AnaMenüObje.SetActive(false);
        BölümlerObje.SetActive(true);
        int BölümSayısı = PlayerPrefs.GetInt("SonSeviye");
        for (int i = 0; i <= BölümSayısı; i++)
        {
            BölümlerObje.transform.GetChild(1).transform.GetChild(i).GetComponent<Button>().interactable = true;
        }
    }
    public void OyunuKapat(){
        Application.Quit();
    }

}
