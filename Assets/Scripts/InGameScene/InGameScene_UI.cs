using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InGameScene_UI : MonoBehaviour
{
    [SerializeField] TMP_Text textTitle;

    private void Start()
    {
        //GameObject gameObject1 = Resources.Load<GameObject>("PopupMessage");
        //Instantiate(gameObject1, transform);
    }

    public void SetTitle(string title)
    {
        this.textTitle.text = title;
    }

    public void OnClick_Pause()
    {
        if (Time.timeScale < Mathf.Epsilon)
            Time.timeScale = 1;
        else
            Time.timeScale = 0;
    }
}
