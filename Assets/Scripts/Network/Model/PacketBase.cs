public class SendPacketBase
{
    public string PacketName;

    public SendPacketBase(PACKET_NAME_TYPE packetName)
    {
        PacketName = packetName.ToString();
    }
}

public class ReceivePacketBase
{
    public readonly int ReturnCode; // 성공, 실패

    public ReceivePacketBase(int returnCode)
    {
        ReturnCode = returnCode;
    }
}