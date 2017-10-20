
namespace AspNetCore.Common.Infrastructure.Export.Converter
{
    /// <summary>
    /// int类型的数据转换成"是/否"，0时返回"否", 其他返回"是"
    /// </summary>
    public class Int2BoolConverter : BaseDataConverter<int, string>
    {
        public override string ConvertTo(int data, object entity) {
            return data == 0 ? "否" : "是";
        }
    }
}