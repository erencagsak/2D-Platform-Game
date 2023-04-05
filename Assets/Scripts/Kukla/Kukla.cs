using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kukla : MonoBehaviour
{
    int kacinciVurus;

    [SerializeField] GameObject parlamaEfekti;

    Vector2 patlamaMiktari = new Vector2(1, 4);

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("KilicVurusBox"))
        {
            Instantiate(parlamaEfekti, transform.position, transform.rotation);
        }
    }
}
