using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Jobs;

public class Kamera : MonoBehaviour
{
    PlayerHareketKontroller player;

    [SerializeField] private Collider2D boundsBox;

    [SerializeField] Transform Background;

    private void Awake()
    {
        player = GameObject.FindObjectOfType<PlayerHareketKontroller>();
    }
    private void Update()
    {
        if (player != null)
        {
            transform.position = new Vector3(
                Mathf.Clamp(player.transform.position.x,boundsBox.bounds.min.x + 12.5f, boundsBox.bounds.max.x - 12.5f),
                Mathf.Clamp(player.transform.position.y,boundsBox.bounds.min.y + 12f, boundsBox.bounds.max.y - 12f),
                transform.position.z);
        }

        BackgroundHareket();
    }

    void BackgroundHareket() 
    {
        Background.position = new Vector3(transform.position.x,transform.position.y,0f);
    }
}
