using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class baltaliEngel : MonoBehaviour
{
    [SerializeField] float donmeHizi = 200f;

    float zAngle;

    [SerializeField] float minZangle = 75f;

    [SerializeField] float maxZangle = 75f;

    private void Update()
    {
        zAngle += Time.deltaTime * donmeHizi;

        transform.rotation = Quaternion.AngleAxis(zAngle, Vector3.forward);

        if (zAngle < minZangle)
        {
            donmeHizi = Mathf.Abs(donmeHizi);
        }

        if (zAngle > maxZangle) 
        {
            donmeHizi = -Mathf.Abs(donmeHizi);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GetComponent<EdgeCollider2D>().IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            if (collision.CompareTag("Player") && !collision.GetComponent<PlayerHareketKontroller>().playerOldumu)
            {
                collision.GetComponent<PlayerHareketKontroller>().geriTepki();

                collision.GetComponent<Saglik>().caniAzalt();
            }
        }
    }

}
