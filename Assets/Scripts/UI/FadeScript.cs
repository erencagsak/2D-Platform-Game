using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FadeScript : MonoBehaviour
{
    public static FadeScript instance;

    [SerializeField] GameObject FadeImaj;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        matToseffaf();
    }

    public void seffafToMat() 
    {
        FadeImaj.GetComponent<CanvasGroup>().alpha = 0f;
        FadeImaj.GetComponent<CanvasGroup>().DOFade(1f,1f);
    }

    public void matToseffaf() 
    {
        FadeImaj.GetComponent<CanvasGroup>().alpha = 1f;
        FadeImaj.GetComponent<CanvasGroup>().DOFade(0f, 1f);
    }
}
