using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Orumcek : MonoBehaviour
{
    [SerializeField] Transform[] pozisyonlar;
    [SerializeField] Slider orumcekSlider;

    public float orumcekHizi;
    public float beklemeSuresi;
    public float takipMesafesi = 5;
    float beklemeSayac;
    bool atakYaptimi;

    [SerializeField] GameObject PrefabIksir;

    public int gecerliSaglik;

    Animator Anim;

    int kacinciPozisyon = 0;

    Transform hedefPlayer;

    BoxCollider2D orumcekCollider;

    Rigidbody2D rb;

    private void Awake()
    {
        gecerliSaglik = 5;

        orumcekSlider.maxValue = gecerliSaglik;

        Anim = GetComponent<Animator>();

        rb = GetComponent<Rigidbody2D>();

        orumcekCollider = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        

        atakYaptimi = true;

        hedefPlayer = GameObject.Find("Player").transform;

        foreach (Transform pos in pozisyonlar)
        {
            pos.parent = null;
        }
    }
    private void Update()
    {
        if (beklemeSayac > 0)
        {

            Anim.SetBool("hareketEtsinmi", false);

            beklemeSayac -= Time.deltaTime;

        }
        else
        {
            if (gecerliSaglik > 0)
            {
                if (hedefPlayer.position.x > pozisyonlar[0].position.x && hedefPlayer.position.x < pozisyonlar[1].position.x)
                {
                    transform.position = Vector3.MoveTowards(transform.position, hedefPlayer.position, orumcekHizi * Time.deltaTime);

                    Anim.SetBool("hareketEtsinmi", true);


                    if (transform.position.x > hedefPlayer.position.x)
                    {
                        transform.localScale = new Vector3(-1, 1, 1);
                    }
                    else if (transform.position.x < hedefPlayer.position.x)
                    {
                        transform.localScale = Vector3.one;
                    }
                }
                else
                {
                    Anim.SetBool("hareketEtsinmi", true);

                    transform.position = Vector3.MoveTowards(transform.position, pozisyonlar[kacinciPozisyon].position, orumcekHizi * Time.deltaTime);

                    if (transform.position.x > pozisyonlar[kacinciPozisyon].position.x)
                    {
                        transform.localScale = new Vector3(-1, 1, 1);
                    }
                    else if (transform.position.x < pozisyonlar[kacinciPozisyon].position.x)
                    {
                        transform.localScale = Vector3.one;
                    }

                    if (Vector3.Distance(transform.position, pozisyonlar[kacinciPozisyon].position) < 0.1f)
                    {
                        posDegistir();

                        beklemeSayac = beklemeSuresi;

                    }
                }
            }
            
        }
    }

    void posDegistir() 
    {
        kacinciPozisyon++;

        if (kacinciPozisyon >= pozisyonlar.Length)
            kacinciPozisyon = 0;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(transform.position, takipMesafesi);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (orumcekCollider.IsTouchingLayers(LayerMask.GetMask("Player")) && atakYaptimi)
        {
            atakYaptimi = false;

            Anim.SetTrigger("saldirdi");

            collision.GetComponent<PlayerHareketKontroller>().geriTepki();
            collision.GetComponent<Saglik>().caniAzalt();

            StartCoroutine(yenidenSaldir());
        }
    }

    public IEnumerator yenidenSaldir() 
    {
        yield return new WaitForSeconds(1f);

        if (gecerliSaglik > 0)
        {
            atakYaptimi = true;
        }

    }

    public IEnumerator geriTepki() 
    {
        //atakYaptimi = false;

        rb.velocity = Vector2.zero;

        yield return new WaitForSeconds(0.1f);

        gecerliSaglik--;

        sliderGuncelle();

        if (gecerliSaglik <= 0)
        {
            atakYaptimi = false;
            gecerliSaglik = 0;

            Instantiate(PrefabIksir, new Vector3(transform.position.x, transform.position.y + 2, transform.position.z), Quaternion.identity);

            Anim.SetTrigger("canVerdi");
            Anim.SetBool("hareketEtsinmi",false);
            orumcekSlider.gameObject.SetActive(false);
            orumcekCollider.enabled = false;

            Destroy(gameObject, 2f);
        }
        else
        {
            for (int i = 0; i < 5; i++)
            {
                rb.velocity = new Vector2(-transform.localScale.x + i, rb.velocity.y);

                yield return new WaitForSeconds(0.05f);
            }

            Anim.SetBool("hareketEtsinmi",true);

            yield return new WaitForSeconds(0.25f);

            rb.velocity = Vector2.zero;
            atakYaptimi = true;
        }
    }

    void sliderGuncelle() 
    {
        orumcekSlider.value = gecerliSaglik;
    }
}
