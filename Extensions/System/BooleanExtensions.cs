namespace System
{

    ///<summary>
    ///Defines a textual boolean value
    ///</summary>
    ///<remarks></remarks>
    public enum BooleanText
    {
        AcceptedDeclined,
        ActiveInactive,
        CheckedUnchecked,
        CorrectIncorrent,
        EnabledDisabled,
        OnOff,
        YesNo
    }

    public static class BooleanExtensions
    {
        /// <summary>
        /// Assesses a boolean value and returns a textual value
        /// </summary>
        /// <param name="text">The textual value to be returned</param>
        /// <returns>A textual representation of the boolean value</returns>
        /// <remarks></remarks>
        public static string GetAsWord(this bool value, BooleanText text)
        {
            switch (text)
            {
                case BooleanText.AcceptedDeclined:
                    return value.GetAsWord("Accepted", "Declined");
                case BooleanText.ActiveInactive:
                    return (value) ? "Active" : "Inactive";
                case BooleanText.CheckedUnchecked:
                    return (value) ? "Checked" : "Unchecked";
                case BooleanText.CorrectIncorrent:
                    return (value) ? "Correct" : "Incorrent";
                case BooleanText.EnabledDisabled:
                    return (value) ? "Enabled" : "Disabled";
                case BooleanText.OnOff:
                    return (value) ? "On" : "Off";
                case BooleanText.YesNo:
                    return (value) ? "Yes" : "No";
                default :
                    return (value) ? "True" : "False";
            }            
        }
        
        /// <summary>
        /// Assesses a boolean value and returns a specified word in respect to true or false
        /// </summary>
        /// <param name="value">The value to be assessed</param>
        /// <returns>A string representation of the boolean value</returns>
        /// <remarks></remarks>
        public static string GetAsWord(this bool value, string trueValue, string falseValue)
        {
            return (value) ? trueValue : falseValue;
        }

    }
    
}