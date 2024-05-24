namespace AuthService.Models.Responses;

public class TokensResponse
{
    public required string RefreshToken {get;set;}
    public required string AccessToken {get;set;}
}