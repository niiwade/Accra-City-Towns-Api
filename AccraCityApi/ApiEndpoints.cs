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
    
}