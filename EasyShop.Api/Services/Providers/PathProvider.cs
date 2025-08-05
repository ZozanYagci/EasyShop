using Core.Interfaces;


namespace EasyShop.Api.Services.Providers
{
    public class PathProvider : IPathProvider
    {
        private readonly IWebHostEnvironment _env;

        public PathProvider(IWebHostEnvironment env)
        {
            _env = env;
        }

        public string GetRootPath()
        {
            //wwwroot yolu döndürülür.
            return _env.WebRootPath ?? Path.Combine(_env.ContentRootPath, "wwwroot");
        }
    }
}
