using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace fr.Core
{
    public static class Constants
    {
        public const string DefaultSchema = JwtBearerDefaults.AuthenticationScheme;
        public const string AccessPurpose = "AccessToken";
    }
}
