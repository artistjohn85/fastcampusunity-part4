//public class PacketName
//{
//    public const string ApplicationConfig = "ApplicationConfig";
//}

public enum PACKET_NAME_TYPE
{
    ApplicationConfig,
    Maintenance,
    UpdateVaild,
}

public enum SCENE_TYPE
{
    Init,
    Loading,
    Lobby,
    InGame,
}

public enum ENVIRONMENT_TYPE
{
    Dev = 1,
    Stage = 2,
    Live = 3,
    LiveTest = 4,
}

public enum OS_TYPE
{
    Android = 1,
    iOS = 2,
    Windows = 3,
}

public enum RETURN_CODE
{
    Success = 200,
    Error = -1,
}

public enum DEVELOPMENT_ID_AUTHORITY
{
    None = 0,
    Tester = 1,
    Master = 2,
}