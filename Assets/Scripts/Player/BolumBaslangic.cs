using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class BolumBaslangic : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            FadeScript.instance.seffafToMat();

            StartCoroutine(sahneyiYukle());
        }
    }

    IEnumerator sahneyiYukle() 
    {
        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(1);
    }
}
