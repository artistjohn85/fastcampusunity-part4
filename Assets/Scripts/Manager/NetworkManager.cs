using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CertHandler : CertificateHandler
{
    protected override bool ValidateCertificate(byte[] certificateData)
    {
        // 여기에서 서버의 인증서를 검증하는 로직을 작성할 수 있습니다.
        // 기본적으로는 true를 반환하여 모든 인증서를 허용합니다.
        return true;
    }
}

public class NetworkManager : ManagerBase
{
    private InitScene_UI initScene_UI; // cache


    private void Awake()
    {
        Dontdestory<NetworkManager>();
        this.initScene_UI = FindAnyObjectByType<InitScene_UI>();
    }

    public void SetInit()
    {
    }

    //public void SendPacket(SendPacketBase sendPacketBase)
    //{
    //    StartCoroutine(C_SendPacket(sendPacketBase));
    //}

    public IEnumerator C_SendPacket<T>(SendPacketBase sendPacketBase, Action<ReceivePacketBase> action = null) where T : ReceivePacketBase
    {
        string packet = JsonUtility.ToJson(sendPacketBase);
        Debug.Log("[NetworkManager Send Packet] " + packet);

        try
        {
            packet = AesEncrypt.EncryptString(packet, Config.AES_KEY, out string iv);
            Debug.Log("Encrypt: " + packet);
            Debug.Log("IV: " + iv);
            packet = iv + "|" + packet;
        } 
        catch (Exception e)
        {
            packet = string.Empty;
        }
        if (!string.IsNullOrEmpty(packet))
        {
            yield return null;
        }


        using (UnityWebRequest request = UnityWebRequest.PostWwwForm(sendPacketBase.Url, packet))
        {
            byte[] bytes = new System.Text.UTF8Encoding().GetBytes(packet);
            request.uploadHandler = new UploadHandlerRaw(bytes);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            // HTTPS 통신을 위한 보안 설정
            request.certificateHandler = new CertHandler();

            this.initScene_UI.LoadingGear.EnableGear();

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error: " + request.error);
                yield return null;
                this.initScene_UI.LoadingGear.DisableGear();
                action?.Invoke(new ReceivePacketBase((int)RETURN_CODE.Error));
            }
            else
            {
                // 성공적으로 데이터를 가져왔을 때 처리
                string jsonData = request.downloadHandler.text;
                Debug.Log("Received Data: " + jsonData);

                T receivePacket = JsonUtility.FromJson<T>(jsonData);
                yield return receivePacket;
                this.initScene_UI.LoadingGear.DisableGear();
                action?.Invoke(receivePacket);
            }
        }
    }
}
