using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEditor.Build.Content;
using UnityEngine;

public class Saglik : MonoBehaviour
{
    public static Saglik instance;

    public int maxSaglik;
    public int gecerliSaglik;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        gecerliSaglik = maxSaglik;

        if (UIScript.Instance != null)
        {
            UIScript.Instance.UpdateSlider(gecerliSaglik, maxSaglik);
        }
    }

    public void caniAzalt() 
    {
        gecerliSaglik -= 5;

        UIScript.Instance.UpdateSlider(gecerliSaglik, maxSaglik);

        if (gecerliSaglik <= 0)
        {
            gecerliSaglik = 0;

            //gameObject.SetActive(false);

            PlayerHareketKontroller.instance.playerOldu();
        }
    }

    public void caniArttir() 
    {
        gecerliSaglik += 5;

        if (gecerliSaglik >= maxSaglik)
        {
            gecerliSaglik = maxSaglik;
        }

        UIScript.Instance.UpdateSlider(gecerliSaglik, maxSaglik);
    }

}
