namespace AspNetCore.Common.Infrastructure.Pagination
{
    public class PageQuery
    {
        public virtual int PageIndex { get; set; }

        public virtual int PageSize { get; set; }

        /// <summary>
        /// 是否分页
        /// </summary>
        public bool IsPage { get; set; } = true;
    }
}