namespace AccraCityApi;

public static class ApiEndpoints
{
    private const string ApiBase = "api";
    
    public static class Region
    {
        private const string Base = $"{ApiBase}/region";
        
        public const string Get = $"{Base}/{{id:guid}}";
        
        public const string GetRegion = $"{Base}/region/{{id:guid}}";
        public const string GetAll = Base;
        public const string Create = Base;
        public const string Update = $"{Base}/{{id:guid}}";
        public const string Delete = $"{Base}/{{id:guid}}";
    }
    
    
    public static class District
    {
        private const string Base = $"{ApiBase}/district";
        
        public const string Get = $"{Base}/{{id:guid}}";
        
        public const string GetDistrict = $"{Base}/district/{{id:guid}}";
        public const string GetAll = Base;
        public const string Create = Base;
        public const string Update = $"{Base}/{{id:guid}}";
        public const string Delete = $"{Base}/{{id:guid}}";
    }
    
    public static class Town
    {
        private const string Base = $"{ApiBase}/town";
        
        public const string Get = $"{Base}/{{id:guid}}";
        
        public const string GetTown = $"{Base}/town/{{id:guid}}";
        public const string GetAll = Base;
        public const string Create = Base;
        public const string Update = $"{Base}/{{id:guid}}";
        public const string Delete = $"{Base}/{{id:guid}}";
    }
    
    public static class Auth
    {
        private const string Base = $"{ApiBase}/Auth";
        
        public const string SeedRoles = $"{Base}/seed-roles";
        //public const string GetWallet = $"{Base}/wallets/{{id:guid}}";
        public const string Register = $"{Base}/register";
        public const string Login = $"{Base}/login";
        public const string MakeAdmin = $"{Base}/make_user_admin";
        public const string MakeOwner = $"{Base}/make_user_owner";
        public const string RemoveOwnerRole = $"{Base}/remove_owner_role";
        public const string RemoveAdminRole = $"{Base}/remove_admin_role";
    }
    
}