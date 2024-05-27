namespace UserDataService.Models.General.Responses;

public class ApiResponse()
{
    public required int Code {get;set;}
    public object? Payload {get;set;}
    public Exception? Exception {get;set;}
}