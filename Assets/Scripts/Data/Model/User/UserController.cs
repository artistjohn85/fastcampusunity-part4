public class UserController
{
    public readonly UserConfigInfo UserConfigInfo;

    public UserController(UserConfigInfo userConfigInfo)
    {
        this.UserConfigInfo = userConfigInfo;
    }
}