﻿public class ApplicationConfigSendPacket : SendPacketBase
{
    // 서버에서 환경에 따라 내려준 주소를 사용하려고 합니다.
    // environment - dev, stage, live
    // os type - Android, iOS
    // version - 1.0.0

    public int E_ENVIRONMENT_TYPE;
    public int E_OS_TYPE;
    public string AppVersion;

    public ApplicationConfigSendPacket(PACKET_NAME_TYPE packetName,
        ENVIRONMENT_TYPE e_ENVIRONMENT_TYPE,
        OS_TYPE e_OS_TYPE,
        string appVersion) : base(packetName)
    {
        E_ENVIRONMENT_TYPE = (int)e_ENVIRONMENT_TYPE;
        E_OS_TYPE = (int)e_OS_TYPE;
        AppVersion = appVersion;
    }
}

public class ApplicationConfigReceivePacket : ReceivePacketBase
{
    public readonly string ApiUrl;

    public ApplicationConfigReceivePacket(int returnCode, string apiUrl) : base(returnCode)
    {
        ApiUrl = apiUrl;
    }
}