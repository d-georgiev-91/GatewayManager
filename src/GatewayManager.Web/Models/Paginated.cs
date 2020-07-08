using System.Collections.Generic;

namespace GatewayManager.Web.Models
{
    public class Paginated<TData>
    {
        public IReadOnlyCollection<TData> Data { get; set; }

        public int TotalCount { get; set; }
    }
}