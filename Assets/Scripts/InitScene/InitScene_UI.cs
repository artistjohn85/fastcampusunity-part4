using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InitScene_UI : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private TMP_Text textPercent;

    public LoadingGear LoadingGear { get; set; } // cache

    private void Awake()
    {
        this.LoadingGear = FindAnyObjectByType<LoadingGear>(FindObjectsInactive.Include);
    }

    public void SetPercent(float factor) // 0~1
    {
        slider.value = factor;
        textPercent.text = $"{(int)(factor * 100)}%";
    }
}
