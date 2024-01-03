using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CertHandler : CertificateHandler
{
    protected override bool ValidateCertificate(byte[] certificateData)
    {
        // ���⿡�� ������ �������� �����ϴ� ������ �ۼ��� �� �ֽ��ϴ�.
        // �⺻�����δ� true�� ��ȯ�Ͽ� ��� �������� ����մϴ�.
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

        // HTTPS ����� ���� ���� ����
        request.certificateHandler = new CertHandler();

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Error: " + request.error);
        }
        else
        {
            // ���������� �����͸� �������� �� ó��
            string jsonData = request.downloadHandler.text;
            Debug.Log("Received Data: " + jsonData);

            // ���⼭���ʹ� JSON �����͸� ���ϴ� ������� ó���ϸ� �˴ϴ�.
            // ���� ���, JSON �����͸� C# ��ü�� ��ȯ�Ϸ��� JsonUtility.FromJson<T>() �Լ��� ����մϴ�.
            // ��: YourDataObject data = JsonUtility.FromJson<YourDataObject>(jsonData);
        }
    }
}
