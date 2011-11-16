using System;

namespace Toltech.Mvc.Tools.AutoMapper
{
    public interface IAutoMapperRegistrar
    {
        void Register(IAutoMapperRegistrationResolver resolver);
    }
}
