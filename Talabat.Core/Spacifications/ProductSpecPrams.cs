namespace Talabat.Api.Helpers
{
    public class ProductSpecPrams
    {

        public int? BrandId { get; set; }
        public int? TypeId { get; set; }

        public string? Sort { get; set; }

        private const int MaxPageSize = 10;
        private int pageSize = 5;
        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value > MaxPageSize ? MaxPageSize : value; }
        }

        public int PageIndex { get; set; } = 1;

        private string? search;

        public string? Search
        {
            get { return search; }
            set { search = value.ToLower(); }
        }

    }
}
