using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SokakLambasi : MonoBehaviour
{
    [SerializeField] SpriteRenderer sokakLambasi;
    [SerializeField] Sprite onSokakLambasi, offSokakLambasi;

    private void Awake()
    {
        sokakLambasi.sprite = offSokakLambasi;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name.Equals("Player"))
        {
            sokakLambasi.sprite = onSokakLambasi;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name.Equals("Player"))
        {
            Invoke("feneriKapat", 0.5f);
        }   
    }

    void feneriKapat() 
    {
        sokakLambasi.sprite = offSokakLambasi;
    }
}
