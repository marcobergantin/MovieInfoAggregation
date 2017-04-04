using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.FileProviders;
using System.IO;

namespace MovieAggregator.Client.Extensions
{
    public static class IApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseNodeModules(this IApplicationBuilder app, string root)
        {
            string path = Path.Combine(root, "node_modules");
            var fileProvider = new PhysicalFileProvider(path);
            var options = new StaticFileOptions()
            {
                RequestPath = "/node_modules",
                FileProvider = fileProvider
            };

            app.UseStaticFiles(options);
            return app;
        }
    }
}