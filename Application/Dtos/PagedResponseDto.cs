using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KuaforRandevu.Application.Dtos
{
    public record PagedResponseDto<T>
    (
        IEnumerable<T> Items,
        int TotalCount,
        int PageNumber,
        int PageSize
    );
}
