using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toplananlar : MonoBehaviour
{
    [SerializeField] bool coinmi, iksirmi;

    bool toplandimi;

    [SerializeField] GameObject Efekt;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name.Equals("Player") && toplandimi == false)
        {
            if (coinmi) 
            {
                toplandimi = true;

                GameManager.instance.toplananCoinAdet += 1;

                UIScript.Instance.coinAdet();

                Destroy(this.gameObject);

                Instantiate(Efekt, transform.position, Quaternion.identity);
            }
            if (iksirmi)
            {
                toplandimi = true;

                Saglik.instance.caniArttir();

                Destroy(this.gameObject);

                Instantiate(Efekt, transform.position, Quaternion.identity);
            }
        }        
    }
}
