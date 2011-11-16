using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Toltech.Mvc.Tools.AutoMapper
{
    public interface IAutoMapperRegistrationResolver
    {
        T Get<T>();
    }
}
