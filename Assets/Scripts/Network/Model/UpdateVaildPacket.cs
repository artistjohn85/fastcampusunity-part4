using UnityEngine;

public class UpdateVaildSendPacket : SendPacketBase
{
    // environment - dev, stage, live
    // os type - Android, iOS
    // version - 1.0.0

    public int E_ENVIRONMENT_TYPE;
    public int E_OS_TYPE;
    public string AppVersion;
    public int LanguageType;

    public UpdateVaildSendPacket(string url,
        PACKET_NAME_TYPE packetName,
        ENVIRONMENT_TYPE e_ENVIRONMENT_TYPE,
        OS_TYPE e_OS_TYPE,
        string appVersion,
        SystemLanguage languageType) : base(url, packetName)
    {
        this.E_ENVIRONMENT_TYPE = (int)e_ENVIRONMENT_TYPE;
        this.E_OS_TYPE = (int)e_OS_TYPE;
        this.AppVersion = appVersion;
        this.LanguageType = (int)languageType;
    }
}

public class UpdateVaildReceivePacket : ReceivePacketBase
{
    public string ApiUrl;
    public bool IsUpdateVaild; // true 업데이트, false 업데이트 아님
    public bool IsRecommand; // true 추천 업데이트, false 강제 업데이트
    public string Title;
    public string Contents;

    public UpdateVaildReceivePacket(int returnCode, string apiUrl, bool isUpdateVaild, bool isRecommand, string title, string contents) : base(returnCode)
    {
        this.ApiUrl = apiUrl;
        this.IsUpdateVaild = isUpdateVaild;
        this.IsRecommand = isRecommand;
        this.Title = title;
        this.Contents = contents;
    }
}