using System;

[Serializable]
public class UserConfigInfo : StoreBase
{
    public string UserId;
    public string Password;
    public string UserName;
    public long TimeStamp;

    public UserConfigInfo(string userName, string password)
    {
        UserId = Guid.NewGuid().ToString();
        UserName = userName;
        TimeStamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        Password = password;
    }

    public string EncrpytPassword(out string iv)
    {
        return AesEncrypt.EncryptString(this.Password, Config.AES_KEY, out iv);
    }
}