namespace BarberBoss.Communication.Responses;

public class ResponseRegisteredUserJson
{
    public long UserId { get; set; }
    public string Token { get; set; } = string.Empty;
}
