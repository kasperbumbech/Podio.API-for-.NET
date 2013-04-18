using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Podio.API
{
    public sealed class ExportType
    {
        private readonly string name;
        private readonly int value;

        public static readonly ExportType XLS = new ExportType(0, "xls");
        public static readonly ExportType XLSX = new ExportType(1, "xlsx");

        private ExportType(int value, string name)
        {
            this.name = name;
            this.value = value;
        }
        public override string ToString()
        {
            return name;
        }
    }
}
