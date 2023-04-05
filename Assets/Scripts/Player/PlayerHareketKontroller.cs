using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHareketKontroller : MonoBehaviour
{
    public static PlayerHareketKontroller instance;

    // Deðiþkenler

    private Rigidbody2D rb;
    [SerializeField] GameObject KilicVurusBox;
    [SerializeField] GameObject normalPlayer, kilicPlayer;
    [SerializeField] Transform zeminKontrolNoktasi;
    [SerializeField] Animator NormalAnim, KilicAnim;
    [SerializeField] float geriTepkiSuresi, geriTepkiGucu;
    [SerializeField] SpriteRenderer normalSprite;
    [SerializeField] SpriteRenderer kilicSprite;
    public bool vurusAcikmi;

    public LayerMask zeminMaske;

    public float hareketHizi;
    public float ziplamaGucu;
    float geriTepkiSayaci;

    bool zemindemi;
    bool ikinceKezZiplasinmi;
    bool yonSagdami;
    public bool playerOldumu;
    bool kiliciVurdumu;

    private void Awake()
    {
        instance = this;

        rb = GetComponent<Rigidbody2D>();

        playerOldumu = false;

        KilicVurusBox.SetActive(false);
    }

    private void Start()
    {
        vurusAcikmi = true;
    }

    private void Update()
    {
        if (playerOldumu == true) 
        {
            return;
        }

        if (geriTepkiSayaci <= 0)
        {
            hareketEt();
            zipla();
            yonuDegistir();

            if (normalPlayer.activeSelf)
            {
                normalSprite.color = new Color(normalSprite.color.r, normalSprite.color.g, normalSprite.color.b, 1f);
            }
            if (kilicPlayer.activeSelf) 
            {
                kilicSprite.color = new Color(normalSprite.color.r, normalSprite.color.g, normalSprite.color.b, 1f);
            }
        }
        else
        {
            geriTepkiSayaci -= Time.deltaTime;

            if (yonSagdami)
            {
                rb.velocity = new Vector2(-geriTepkiGucu,rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(geriTepkiGucu,rb.velocity.y);
            }
        }
        if (Input.GetMouseButtonDown(0) && kilicPlayer.activeSelf)
        {
            if (vurusAcikmi == true)
            {
                
                    KilicVurusBox.SetActive(true);

                    kiliciVurdumu = true;

                    vurusAcikmi = false;  
            }
        }
        else
        {

            kiliciVurdumu = false;

        }


        // Methodlar



        // Animasyonlar

        if (normalPlayer.activeSelf)
        {
            NormalAnim.SetBool("zemindemi", zemindemi);
            NormalAnim.SetFloat("hareketHizi", Mathf.Abs(rb.velocity.x));
            NormalAnim.SetBool("ikinceKezZiplasinmi", ikinceKezZiplasinmi);
        }
        if (kilicPlayer.activeSelf)
        {
            KilicAnim.SetBool("zemindemi",zemindemi);
            KilicAnim.SetFloat("hareketHizi", Mathf.Abs(rb.velocity.x));
            KilicAnim.SetBool("ikinceKezZiplasinmi", ikinceKezZiplasinmi);
        }
        if (kiliciVurdumu && kilicPlayer.activeSelf)
        {
            KilicAnim.SetTrigger("kilicVurdu");
        }
        
    }
    void hareketEt() 
    {
        // Yürüme Kodlarý

        float h = Input.GetAxis("Horizontal");

        rb.velocity = new Vector2(h * hareketHizi, rb.velocity.y);


    }
    void yonuDegistir() 
    {
        // Arkaya dönme komutu

        if (rb.velocity.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);

            yonSagdami = false;
        }
        else if (rb.velocity.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);

            yonSagdami = true;
        }
    }

    void zipla() 
    {
        // Zýplama

        zemindemi = Physics2D.OverlapCircle(zeminKontrolNoktasi.position, 0.2f, zeminMaske);

        if (Input.GetKeyDown(KeyCode.W) && (zemindemi || ikinceKezZiplasinmi))
        {
            if (zemindemi)
            {
                ikinceKezZiplasinmi = true;
            }
            else 
            {
                ikinceKezZiplasinmi = false;
            }

            rb.velocity = new Vector2(rb.velocity.x, ziplamaGucu);
        }
    }
    public void geriTepki() 
    {
        geriTepkiSayaci = geriTepkiSuresi;

        if (normalPlayer.activeSelf)
        {
            normalSprite.color = new Color(normalSprite.color.r, normalSprite.color.g, normalSprite.color.b, .5f);
        }
        if (kilicPlayer.activeSelf)
        {
            kilicSprite.color = new Color(kilicSprite.color.r, kilicSprite.color.g, kilicSprite.color.b, .5f);
        }

        rb.velocity = new Vector2(0,rb.velocity.y);
    }

    public void playerOldu() 
    {
        rb.velocity = Vector2.zero;
        playerOldumu = true;

        if (normalPlayer.activeSelf)
        {
            NormalAnim.SetTrigger("canVerdi");

            Invoke("sahneyiYenile", 3f);
        }
        if (kilicPlayer.activeSelf)
        {
            KilicAnim.SetTrigger("canverdi");

            Invoke("sahneyiYenile", 3f);
        }



    }

    public void sahneyiYenile() 
    {
        SceneManager.LoadScene(0);
    }

    public void spriteGecis() 
    {
        normalPlayer.SetActive(false);
        kilicPlayer.SetActive(true);
    }
}
