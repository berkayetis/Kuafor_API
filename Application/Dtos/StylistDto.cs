using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KuaforRandevu.Application.Dtos
{
    public record StylistDto
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
    }
}
