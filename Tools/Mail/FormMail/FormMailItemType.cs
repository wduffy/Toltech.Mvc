using System;

namespace Toltech.Mvc.Tools
{

    [Flags]
    public enum FormMailItemType
    {
        None = 0,
        Required = 1,
        Email = 2,
        RobotChecker = 4
    }

}