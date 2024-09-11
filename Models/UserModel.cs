public class UserModel
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string EmailAddress { get; set; }
    public string Role { get; set; }
}

public class UserConstants
{
    public static List<UserModel> Users = new List<UserModel>()
    {
        new UserModel() {
            Username     = "admin",
            EmailAddress = "admin@email.com",
            Password     = "password",
            Role         = "Administrator",
        },
        new UserModel() {
            Username     = "seller",
            EmailAddress = "seller@email.com",
            Password     = "password",
            Role         = "Seller",
        },
    };
}