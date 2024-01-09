public class ApplicationConfigSendPacket : SendPacketBase
{
    // 서버에서 환경에 따라 내려준 주소를 사용하려고 합니다.
    // environment - dev, stage, live
    // os type - Android, iOS
    // version - 1.0.0

    public int E_ENVIRONMENT_TYPE;
    public int E_OS_TYPE;
    public string AppVersion;
    public string DevelopmentId;

    public ApplicationConfigSendPacket(string url, 
        PACKET_NAME_TYPE packetName,
        ENVIRONMENT_TYPE e_ENVIRONMENT_TYPE,
        OS_TYPE e_OS_TYPE,
        string appVersion,
        string developmentId) : base(url, packetName)
    {
        this.E_ENVIRONMENT_TYPE = (int)e_ENVIRONMENT_TYPE;
        this.E_OS_TYPE = (int)e_OS_TYPE;
        this.AppVersion = appVersion;
        this.DevelopmentId = developmentId;
    }
}

public class ApplicationConfigReceivePacket : ReceivePacketBase
{
    public string ApiUrl;
    public int DevelopmentIdAuthority;

    public ApplicationConfigReceivePacket(int returnCode, string apiUrl, int developmentIdAuthority) : base(returnCode)
    {
        this.ApiUrl = apiUrl;
        this.DevelopmentIdAuthority = developmentIdAuthority;
    }
}
