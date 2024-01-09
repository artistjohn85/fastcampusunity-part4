using System;

[Serializable]
public class UserConfigInfo : StoreBase
{
    public string UserId;
    public string UserName;
    public long TimeStamp;

    public UserConfigInfo(string userName)
    {
        UserId = Guid.NewGuid().ToString();
        UserName = userName;
        TimeStamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
    }
}