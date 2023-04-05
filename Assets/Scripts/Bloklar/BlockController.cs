using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    public GameObject coinPrefab;
    Vector3 coinPos;

    public Transform altPoint;

    Animator Anim;

    Vector3 hareketYonu = Vector3.up;
    Vector3 orijinalPos;
    Vector3 animPos;

    bool animasyonBaslasinmi;
    bool hareketEtsinmi = true;

    public LayerMask playerLayer;

    private void Awake()
    {
        Anim = GetComponent<Animator>();
    }

    private void Start()
    {
        orijinalPos = transform.position;
        animPos = transform.position;
        animPos.y += 0.15f;
        coinPos = transform.position;
        coinPos.y += 1f;
    }

    private void Update()
    {
        carpismayiKontrolEt();
        animasyonuBaslat();
    }

    void carpismayiKontrolEt()
    {
        if (hareketEtsinmi)
        {
            RaycastHit2D raycastHit2D = Physics2D.Raycast(altPoint.position, Vector2.down, 0.1f, playerLayer);

            if (raycastHit2D && raycastHit2D.collider.gameObject.tag == "Player")
            {
                Anim.Play("Mat");
                animasyonBaslasinmi = true;
                hareketEtsinmi =false;

                Instantiate(coinPrefab,coinPos,Quaternion.identity);
            }
        }
    }

    void animasyonuBaslat()
    {
        if (animasyonBaslasinmi)
        {
            transform.Translate(hareketYonu * Time.smoothDeltaTime);

            if (transform.position.y >= animPos.y)
            {
                hareketYonu = Vector3.down;
            }
            else if (transform.position.y <= orijinalPos.y) 
            {
                animasyonBaslasinmi = false;
            }
        }
    }
}
