namespace SolidDemo.Data;

public class UserInformation
{
    public string? FullName { get; set; }
    public string? Password { get; set; }
}

public static class CustomerData
{
    public static Dictionary<string, UserInformation> Information { get; } = new Dictionary<string, UserInformation>
    {
        { "1001", new UserInformation { FullName = "Bryan Joseph Aguinaldo", Password = "CPQ" } },
        { "1002", new UserInformation { FullName = "Hansel Avellana", Password = "CPQ" } }
    };
}
