using Microsoft.AspNetCore.Authorization;

namespace Advanced.NET6.MinimalApi.Utility
{
    public class CustomAuthorizeData : IAuthorizeData
    {
        public string? Policy { get; set; }
        public string? Roles { get; set; }
        public string? AuthenticationSchemes { get; set; }
    }
}
