
namespace Toltech.Mvc.Tools
{

    internal interface IFormMailItem
    {
        string Name { get; }
        string Value { get; }
        FormMailItemType Type { get; }
        bool SpamCheck { get; }
    }

}