using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InGameScene_UI : MonoBehaviour
{
    [SerializeField] TMP_Text textTitle;

    public void SetTitle(string title)
    {
        this.textTitle.text = title;
    }
}
