using ServiceStack;
using ServiceStack.DataAnnotations;

namespace SSRQ.ServiceInterface.Routing
{
    [Exclude(Feature.Metadata)]
    [FallbackRoute("/{PathInfo*}", Matches = "AcceptsHtml")]
    public class FallbackForClientRoutes
    {
        public string PathInfo { get; set; }
    }
}
