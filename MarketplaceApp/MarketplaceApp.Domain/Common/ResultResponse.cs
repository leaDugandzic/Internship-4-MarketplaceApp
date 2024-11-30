namespace MarketplaceApp.Domain.Common;

public class ResultResponse<T>
{
    public bool Success { get; private set; }
    public ResponseResultType ResultType { get; set; }
    public List<T>? Data { get; }
    public string? ErrorMessage { get; set; }

    public ResultResponse(ResponseResultType resultType,string? errorMessage = null, List<T>? data = null)
    {
        Data = data;
        ErrorMessage = errorMessage;
        ResultType = resultType;    
        Success = errorMessage == null;
    }
}