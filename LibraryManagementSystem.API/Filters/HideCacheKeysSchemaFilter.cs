using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace LibraryManagementSystem.API.Filters
{
    public class HideCacheKeysFormDataOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.RequestBody?.Content == null)
                return;

            if (operation.RequestBody.Content.TryGetValue("multipart/form-data", out var mediaType))
            {
                var schema = mediaType.Schema;
                if (schema?.Properties != null && schema.Properties.ContainsKey("CacheKeys"))
                {
                    schema.Properties.Remove("CacheKeys");
                }
            }
        }
    }
}
