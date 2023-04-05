using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Yarasa : MonoBehaviour
{
    [SerializeField] float takipMesafesi = 8f;

    [SerializeField] float ucusHizi;

    [SerializeField] Transform hedefPlayer;

    [SerializeField] GameObject iksirPrefab;

    Animator Anim;

    Rigidbody2D rb;

    BoxCollider2D batCollider;

    public float atakSuresi;
    float atakSayac;
    float mesafe;

    Vector2 hareketYonu;

    public int maxSaglik;
    int gecerliSaglik;

    private void Start()
    {
        gecerliSaglik = maxSaglik;
    }

    private void Awake()
    {
        hedefPlayer = GameObject.Find("Player").transform;

        Anim = GetComponent<Animator>();

        rb = GetComponent<Rigidbody2D>(); 

        batCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (atakSayac < 0)
        {
            if (hedefPlayer && gecerliSaglik > 0 && !PlayerHareketKontroller.instance.playerOldumu)
            {
                mesafe = Vector2.Distance(transform.position, hedefPlayer.position);

                if (mesafe < takipMesafesi)
                {
                    Anim.SetTrigger("ucusaGecti");

                    hareketYonu = hedefPlayer.position - transform.position;

                    if (transform.position.x > hedefPlayer.position.x)
                    {
                        transform.localScale = new Vector3(-1, 1, 1);
                    }
                    else if (transform.position.x < hedefPlayer.position.x)
                    {
                        transform.localScale = Vector3.one;
                    }

                    rb.velocity = hareketYonu * ucusHizi;
                }
            }
        }
        else 
        {
            atakSayac -= Time.deltaTime;
        }
    }

    public void caniAzalt() 
    {
        gecerliSaglik--;
        atakSayac = atakSuresi;

        rb.velocity = Vector2.zero;

        if (gecerliSaglik <= 0)
        {
            gecerliSaglik = 0;

            Instantiate(iksirPrefab, new Vector3(transform.position.x,transform.position.y + 2,transform.position.z), Quaternion.identity);

            batCollider.enabled = false;

            Anim.SetTrigger("oldu");

            Destroy(gameObject, 3f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (batCollider.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            if (collision.CompareTag("Player"))
            {
                rb.velocity = Vector2.zero;
                atakSayac = atakSuresi;
                Anim.SetTrigger("saldirdi");

                collision.GetComponent<PlayerHareketKontroller>().geriTepki();
                collision.GetComponent<Saglik>().caniAzalt();
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawWireSphere(transform.position, takipMesafesi);
    }
}
