using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    [SerializeField] Transform[] pozisyonlar;

    public float birdSpeed;
    public float beklemeSuresi;
    float beklemeSayaci;

    int kacinciPozisyon;

    Animator Anim;

    Vector2 kusYonu;

    private void Awake()
    {
        Anim = GetComponent<Animator>();

        foreach (Transform pos in pozisyonlar)
        {
            pos.parent = null;
        }
    }

    private void Start()
    {
        kacinciPozisyon = 0;

        transform.position = pozisyonlar[kacinciPozisyon].position;
    }

    private void Update()
    {
        if (beklemeSayaci > 0)
        {
            Anim.SetBool("ucsunmu", false);

            beklemeSayaci -= Time.deltaTime;
        }
        else
        {
            kusYonu = new Vector2(pozisyonlar[kacinciPozisyon].position.x - transform.position.x, pozisyonlar[kacinciPozisyon].position.y - transform.position.y);

            float angle = Mathf.Atan2(kusYonu.y, kusYonu.x) * Mathf.Rad2Deg;

            if (transform.position.x > pozisyonlar[kacinciPozisyon].position.x)
            {
                transform.localScale = new Vector3(1, -1, 1);
            }
            else
            {
                transform.localScale = Vector3.one;
            }

            transform.rotation = Quaternion.Euler(0,0,angle);

            transform.position = Vector3.MoveTowards(transform.position, pozisyonlar[kacinciPozisyon].position, birdSpeed * Time.deltaTime);

            Anim.SetBool("ucsunmu", true);

            if (Vector3.Distance(transform.position, pozisyonlar[kacinciPozisyon].position) < 0.1f)
            {
                pozisyonDegistir();

                beklemeSayaci = beklemeSuresi;
            }
        }
    }

    void pozisyonDegistir() 
    {
        kacinciPozisyon++;

        if (kacinciPozisyon >= pozisyonlar.Length)
        {
            kacinciPozisyon = 0;
        }
    }
}
