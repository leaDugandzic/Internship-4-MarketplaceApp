namespace MarketplaceApp.Domain.Common;

public enum ResponseResultType
{
    Success = 1,
    NotFound = 2,
    AlreadyExists = 3,
    NoChanges = 4,
    ValidationError = 5,
    InternalError = 6,
    NoData = 7,
}