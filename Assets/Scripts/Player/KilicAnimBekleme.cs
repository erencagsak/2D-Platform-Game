using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KilicAnimBekleme : MonoBehaviour
{
    public void kilicVurmaBeklet()
    {
        PlayerHareketKontroller.instance.vurusAcikmi = true;
    }
}
