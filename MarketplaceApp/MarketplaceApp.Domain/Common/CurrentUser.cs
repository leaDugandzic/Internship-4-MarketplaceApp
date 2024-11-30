using MarketplaceApp.Domain.Enum;

namespace MarketplaceApp.Domain.Common;

public static class CurrentUser
{
    public static int CurrentUserId { get; set; } = 0;
    public static string CurrentUserName { get; set; } = string.Empty;
    public static KorisnikEnum KorisnikType { get; set; } = KorisnikEnum.Kupac;
}