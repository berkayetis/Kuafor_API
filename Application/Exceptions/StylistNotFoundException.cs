using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KuaforRandevu.Application.Exceptions
{
    public sealed class StylistNotFoundException : NotFoundException
    {
        public StylistNotFoundException(Guid id) : base($"Book id:{id} is not found!")
        {
        }
    }
}
