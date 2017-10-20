
namespace AspNetCore.Common.Infrastructure.Export.Converter
{
    public abstract class BaseDataConverter<TInt, TOut> : IDataConvert
    {
        public object Convert(object data, object entity) {
            if (!(data is TInt)) {
                return null;
            }
            return ConvertTo((TInt)data, entity);
        }

        public abstract TOut ConvertTo(TInt data, object entity);
    }

    public interface IDataConvert
    {
        object Convert(object data, object entity);
    }
}