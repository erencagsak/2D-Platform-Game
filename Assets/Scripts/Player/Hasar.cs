using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Hasar : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name.Equals("Player"))
        {
            Saglik.instance.caniAzalt();

            PlayerHareketKontroller.instance.geriTepki();
        }
    }
}
