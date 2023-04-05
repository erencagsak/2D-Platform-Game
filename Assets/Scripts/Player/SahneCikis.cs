using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SahneCikis : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            FadeScript.instance.seffafToMat();

            StartCoroutine(DigerSahneyeGec());
        }
    }

    IEnumerator DigerSahneyeGec() 
    {
        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(0);
    }
}
