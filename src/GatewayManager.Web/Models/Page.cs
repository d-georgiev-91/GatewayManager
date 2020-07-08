namespace GatewayManager.Web.Models
{
    public class Page
    {
        private const int DefaultPageIndex = 0;
        private const int DefaultPageSize = 5;

        public Page()
        {
            Index = DefaultPageIndex;
            Size = DefaultPageSize;
        }

        /// <summary>
        /// Page index zero based
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// Page size
        /// </summary>
        public int Size { get; set; }
    }
}