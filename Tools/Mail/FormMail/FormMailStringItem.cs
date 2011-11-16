
namespace Toltech.Mvc.Tools
{

    internal class FormMailStringItem : IFormMailItem
    {
        private string _name;
        private string _value;
        private FormMailItemType _type;
        private bool _spamCheck;

        public FormMailStringItem(string name, string value, FormMailItemType type, bool spamCheck)
        {
            _name = name;
            _value = value;
            _type = type;
            _spamCheck = spamCheck;
        }

        public string Name
        {
            get { return _name; }
        }

        public string Value
        {
            get { return _value; }
        }

        public FormMailItemType Type
        {
            get { return _type; }
        }

        public bool SpamCheck
        {
            get { return _spamCheck; }
        }
    }

}
