using System.Collections.Generic;
using Newtonsoft.Json;

namespace AspNetCore.Common.Infrastructure.Pagination
{
    public interface IPagedList
    {
        Pagination Paged { get; set; }
    }

    public class PagedList<T> : IPagedList
    {
        public PagedList(IEnumerable<T> collection) {
            Items = collection;
        }

        [JsonProperty("items")]
        public IEnumerable<T> Items { get; set; }

        [JsonProperty("paged")]
        public Pagination Paged { get; set; }
    }

    public class Pagination
    {
        [JsonProperty("pageIndex")]
        public int PageIndex { get; set; }

        [JsonProperty("pageSize")]
        public int PageSize { get; set; }

        [JsonProperty("pageCount")]
        public int PageCount { get; set; }

        [JsonProperty("totalCount")]
        public int TotalCount { get; set; }
    }
}