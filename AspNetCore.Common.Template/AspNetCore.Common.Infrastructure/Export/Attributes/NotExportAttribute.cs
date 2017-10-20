using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore.Common.Infrastructure.Export.Attributes
{
    public class NotExportAttribute : ExportAttribute
    {
        public NotExportAttribute() : base(false) { }
    }
}
