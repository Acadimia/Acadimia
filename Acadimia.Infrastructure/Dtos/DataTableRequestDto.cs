using System;
using System.Collections.Generic;
using System.Text;

namespace Acadimia.Infrastructure.Dtos
{
    public class DataTableRequestDto
    {
        public string? SearchValue { get; set; }
        public string? SortColumn { get; set; }
        public string? SortColumnDirection { get; set; }
        public int PageSize { get; set; } = 10;
        public int Skip { get; set; } = 0;

    }
}
