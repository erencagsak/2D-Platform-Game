using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KiliciKapat : MonoBehaviour
{
    public GameObject KilicVurusBox;

    public void SwordKapat() 
    {
        KilicVurusBox.SetActive(false);

        //PlayerHareketKontroller.vurusAcikmi = false;
    }
}
