using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSaldir : MonoBehaviour
{
    [SerializeField] BoxCollider2D kilicVurusBox;
    [SerializeField] GameObject parlamaEfekti;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (kilicVurusBox.IsTouchingLayers(LayerMask.GetMask("Enemy")))
        {
            if (collision.CompareTag("Orumcek"))
            {
                if (parlamaEfekti)
                {
                    Instantiate(parlamaEfekti, collision.transform.position, Quaternion.identity);
                }

                StartCoroutine(collision.GetComponent<Orumcek>().geriTepki());
            }
        }

        if (kilicVurusBox.IsTouchingLayers(LayerMask.GetMask("Enemy")))
        {
            if (collision.CompareTag("Yarasa"))
            {
                if (parlamaEfekti)
                {
                    Instantiate(parlamaEfekti, collision.transform.position, Quaternion.identity);
                }

                collision.GetComponent<Yarasa>().caniAzalt();
            }
        }
    }
}
