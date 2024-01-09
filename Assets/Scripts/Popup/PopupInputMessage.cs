using System;
using TMPro;
using UnityEngine;

public class PopupInputMessage : MonoBehaviour
{
    [SerializeField] private TMP_Text txtTitle;
    [SerializeField] private TMP_Text contentsTitle;
    [SerializeField] private TMP_InputField inputField;

    [SerializeField] private GameObject oneButton;
    [SerializeField] private GameObject twoButton;

    private PopupMessageInfo popupMessageInfo; // cache
    private Action cancleAction; // cache
    private Action<string> okAction; // cache

    public void OpenMessage(PopupMessageInfo popupMessageInfo, string value, Action cancleAction, Action<string> okAction)
    {
        this.popupMessageInfo = popupMessageInfo;
        this.cancleAction = cancleAction;
        this.okAction = okAction;

        // set UI
        this.txtTitle.text = popupMessageInfo.title;
        this.contentsTitle.text = popupMessageInfo.contents;
        this.inputField.text = value;

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
        okAction?.Invoke(inputField.text);
        Destroy(gameObject);
    }

    public void OnClick_OnlyOk()
    {
        okAction?.Invoke(inputField.text);
        Destroy(gameObject);
    }
}
