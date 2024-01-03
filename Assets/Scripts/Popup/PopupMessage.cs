using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum POPUP_MESSAGE_TYPE
{
    ONE_BUTTON,
    TWO_BUTTON,
}

public class PopupMessageInfo
{
    public readonly POPUP_MESSAGE_TYPE type;
    public readonly string title;
    public readonly string contents;

    public PopupMessageInfo(POPUP_MESSAGE_TYPE type, string title, string contents)
    {
        this.type = type;
        this.title = title;
        this.contents = contents;
    }
}

public class PopupMessage : MonoBehaviour
{
    [SerializeField] private TMP_Text txtTitle;
    [SerializeField] private TMP_Text contentsTitle;

    [SerializeField] private GameObject oneButton;
    [SerializeField] private GameObject twoButton;

    private PopupMessageInfo popupMessageInfo; // cache
    private Action cancleAction; // cache
    private Action okAction; // cache

    public void OpenMessage(PopupMessageInfo popupMessageInfo, Action cancleAction, Action okAction)
    {
        this.popupMessageInfo = popupMessageInfo;
        this.cancleAction = cancleAction;
        this.okAction = okAction;

        // set UI
        this.txtTitle.text = popupMessageInfo.title;
        this.contentsTitle.text = popupMessageInfo.contents;

        oneButton.SetActive(popupMessageInfo.type == POPUP_MESSAGE_TYPE.ONE_BUTTON);
        twoButton.SetActive(popupMessageInfo.type == POPUP_MESSAGE_TYPE.TWO_BUTTON);
    }

    public void OnClick_Cancle()
    {
        cancleAction?.Invoke();
        Destroy(gameObject);
    }

    public void OnClick_Ok()
    {
        okAction?.Invoke();
        Destroy(gameObject);
    }

    public void OnClick_OnlyOk()
    {
        okAction?.Invoke();
        Destroy(gameObject);
    }
}