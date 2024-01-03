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
    private string apiUrl;

    private void Awake()
    {
        Dontdestory<NetworkManager>();
    }

    public void SetInit(string apiUrl)
    {
        this.apiUrl = apiUrl;
    }

    public void SendPacket()
    {
        StartCoroutine(C_SendPacket());
    }

    IEnumerator C_SendPacket()
    {
        UnityWebRequest request = UnityWebRequest.Get(this.apiUrl);

        // HTTPS 통신을 위한 보안 설정
        request.certificateHandler = new CertHandler();

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Error: " + request.error);
        }
        else
        {
            // 성공적으로 데이터를 가져왔을 때 처리
            string jsonData = request.downloadHandler.text;
            Debug.Log("Received Data: " + jsonData);

            // 여기서부터는 JSON 데이터를 원하는 방식으로 처리하면 됩니다.
            // 예를 들어, JSON 데이터를 C# 객체로 변환하려면 JsonUtility.FromJson<T>() 함수를 사용합니다.
            // 예: YourDataObject data = JsonUtility.FromJson<YourDataObject>(jsonData);
        }
    }
}
