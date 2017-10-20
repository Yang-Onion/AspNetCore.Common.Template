using AspNetCore.Common.Infrastructure.Export.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AspNetCore.Common.Infrastructure.Export.Formaters
{
    /// <summary>
    /// CSV格式化
    /// </summary>
    public class ExcelCSVFormater : BaseDataFormater<string>
    {
        public bool HasHeader { get; set; } = true;

        public override string Format(IList<IEnumerable<ExportProperty>> entities) {
            var sb = new StringBuilder();
            if (entities != null) {
                var buildHeader = false;
                foreach (var entity in entities) {
                    if (!buildHeader && HasHeader) {
                        sb.Append(string.Join((string)",", (IEnumerable<string>)entity.Select(g => g.Header)));
                        buildHeader = true;
                        sb.AppendLine();
                    }
                    sb.Append(string.Join((string)",", (IEnumerable<string>)entity.Select(g => g.Value)));
                    sb.AppendLine();
                }
            }
            return sb.ToString();
        }

        public override string Format(object[,] twoDimensArray) {
            throw new NotImplementedException();
        }
    }
}