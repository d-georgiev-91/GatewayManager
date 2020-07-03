using System.Collections.Generic;

namespace GatewayManager.Services.Models
{
    public class Paginated<TData>
    {
        public IEnumerable<TData> Data { get; set; }

        public int TotalCount { get; set; }
    }
}