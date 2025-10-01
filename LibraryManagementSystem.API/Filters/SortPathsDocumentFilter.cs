using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace LibraryManagementSystem.API.Filters
{
    public class SortPathsDocumentFilter : IDocumentFilter
    {

        #region By Length
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            var orderedPaths = new OpenApiPaths();

            foreach (var path in swaggerDoc.Paths.OrderBy(p => p.Key.Length))
            {
                orderedPaths.Add(path.Key, path.Value);
            }

            swaggerDoc.Paths = orderedPaths;
        }
        #endregion

        #region By Collection then Single resource
        //public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        //{
        //    var orderedPaths = new OpenApiPaths();

        //    foreach (var path in swaggerDoc.Paths.OrderBy(p => SortPathKey(p.Key)))
        //    {
        //        orderedPaths.Add(path.Key, path.Value);
        //    }

        //    swaggerDoc.Paths = orderedPaths;
        //}

        //private int SortPathKey(string path)
        //{
        //    if (!path.Contains("{id}"))
        //        return 0; 
        //    return 1;
        //} 
        #endregion

        #region By Method
        //public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        //{
        //    var allEndpoints = swaggerDoc.Paths
        //        .SelectMany(path => path.Value.Operations.Select(op => new
        //        {
        //            Path = path.Key,
        //            Method = op.Key,
        //            Operation = op.Value
        //        }))
        //        .OrderBy(x => GetMethodOrder(x.Method))
        //        .ThenBy(x => x.Path)
        //        .ToList();

        //    var orderedPaths = new OpenApiPaths();

        //    foreach (var ep in allEndpoints)
        //    {
        //        if (!orderedPaths.ContainsKey(ep.Path))
        //            orderedPaths.Add(ep.Path, new OpenApiPathItem());

        //        orderedPaths[ep.Path].Operations.Add(ep.Method, ep.Operation);
        //    }

        //    swaggerDoc.Paths = orderedPaths;
        //}

        //private int GetMethodOrder(OperationType method) =>
        //    method switch
        //    {
        //        OperationType.Get => 0,
        //        OperationType.Post => 1,
        //        OperationType.Put => 2,
        //        OperationType.Patch => 3,
        //        OperationType.Delete => 4,
        //        _ => 5
        //    };
        #endregion
    }
}
