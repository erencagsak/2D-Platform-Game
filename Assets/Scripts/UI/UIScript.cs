using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    public static UIScript Instance;

    [SerializeField] Slider PlayerSlider;
    [SerializeField] TMP_Text coinText;

    private void Awake()
    {
        Instance = this;
    }

    public void UpdateSlider(int gecerliDeger, int maxDeger) 
    {
        PlayerSlider.maxValue = maxDeger;
        PlayerSlider.value = gecerliDeger;
    }

    public void coinAdet() 
    {
        coinText.text = "" + GameManager.instance.toplananCoinAdet;
    }
}
