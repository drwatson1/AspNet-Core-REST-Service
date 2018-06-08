using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace ReferenceProject
{
    /// <summary>
    /// OPTIONS HTTP query handler
    /// </summary>
    /// <remarks>The code based on this answer https://stackoverflow.com/a/36454275/8127016</remarks>
    public class OptionsHttpMessageHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request.Method == HttpMethod.Options)
            {
                /*
                 * TODO: Find the way to implement this correctly
                // apiExplorer.ApiDescriptions - always has 0 entries in my case.
                // This would be checked out in IIS instead of IISExpress thus this doesn't work
                var apiExplorer = GlobalConfiguration.Configuration.Services.GetApiExplorer();
                var desc = apiExplorer.ApiDescriptions;
                */

                /*
                var apiExplorer = GlobalConfiguration.Configuration.Services.GetApiExplorer();
                var route = request.GetRouteData();
                if (route == null)
                {
                    route = request.GetConfiguration().Routes.GetRouteData(request);
                }
                var controllerRequested = route.Values["controller"] as string;

                var supportedMethods = apiExplorer.ApiDescriptions.Where(d =>
                    {
                        var controller = d.ActionDescriptor.ControllerDescriptor.ControllerName;
                        return string.Equals(
                            controller, controllerRequested, StringComparison.OrdinalIgnoreCase);
                    })
                    .Select(d => d.HttpMethod.Method)
                    .Distinct();

                if (!supportedMethods.Any())
                    return Task.Factory.StartNew(
                        () => request.CreateResponse(HttpStatusCode.NotFound));
                        */

                var response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Headers.Add("Access-Control-Allow-Methods", "*");
                return Task.FromResult(response);
            }
            return base.SendAsync(request, cancellationToken);
        }
    }
}
