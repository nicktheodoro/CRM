namespace SharedDomain;
public sealed class UserRequest
{
    public string ID { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string IP { get; set; }

    public UserRequest()
    {
    }

    public UserRequest(string id, string name, string email, string ip)
    {
        ID = id;
        Name = name;
        Email = email;
        IP = ip;
    }
}

