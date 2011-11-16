using System.Collections.Specialized;
using System.Text.RegularExpressions;

namespace System
{
    public static class StringExtensions
    {

        /// <summary>
        /// Returns an extract of the current string, also strips any HTML if found
        /// </summary>
        /// <param name="length">The amount of characters to be extracted</param>
        /// <returns>An extract from the current string</returns>
        /// <remarks></remarks>
        public static string Extract(this string s, int length)
        {
            string output = s.StripHtml();

            if (output.Length > length)
                output = string.Concat(output.Substring(0, length).TrimEnd('.'), "...");

            return string.Concat(output);
        }

        /// <summary>
        /// Assesses the current string and returns it's value with all HTML markup removed
        /// </summary>
        /// <returns>A string that is guaranteed to have no HTML markup</returns>
        /// <remarks></remarks>
        public static string StripHtml(this string s)
        {
            return Regex.Replace(s, "</?[a-z][a-z0-9]*[^<>]*>", "", RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// Assesses the current string and returns it's value with all HTML markup removed
        /// </summary>
        /// <returns>A string that is guaranteed to have no HTML markup</returns>
        /// <remarks></remarks>
        public static string NlToBr(this string s)
        {
            return s.Replace(Environment.NewLine, "<br />");
        }

        /// <summary>
        /// Returns a boolean indicating if the current string matches the format of a valid address address
        /// </summary>
        /// <returns>A boolean value indicating if the current string matches the format of a valid address address</returns>
        /// <remarks></remarks>
        public static bool IsEmail(this string s)
        {
            return s.IsMatch(@"^[A-Z0-9._%-]+@[A-Z0-9._%-]+\.[A-Z]{2,4}$");
        }

        /// <summary>
        /// Returns a boolean indicating if the current string matches the format of a regular expression
        /// </summary>
        /// <returns>A boolean value indicating if the current string matches the format of a valid address address</returns>
        /// <remarks></remarks>
        public static bool IsMatch(this string s, string regex)
        {
            return Regex.IsMatch(s, regex, RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// Guarantees that the output from the current string will not be empty
        /// </summary>
        /// <param name="value">The value to be returned if the current string is null or empty</param>
        /// <returns>A <see cref="System.String" /> value that is guaranteed to not be empty</returns>
        /// <remarks></remarks>
        public static string Assured(this string s, string value)
        {
            if (string.IsNullOrEmpty(value.Trim()))
                throw new ArgumentException("Value cannot be null or empty", "value");

            return string.IsNullOrEmpty(s.Trim()) ? value : s;
        }

        /// <summary>
        /// Returns a guaranteed friendly path from the current string
        /// </summary>
        /// <returns>A safe path string</returns>
        /// <remarks>Replaces any non friendly characters with a -</remarks>
        public static string ToSafePath(this string s)
        {
            return Regex.Replace(s, "[^\\w_-]+", "-", RegexOptions.IgnoreCase).ToLower();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool ContainsSpam(this string s)
        {

            if (s.IsMatch("(http://|www.|< *script)"))
                return true;

            StringCollection spamWords = new StringCollection()
            {
            "4u",
            "ass", "arse", "anal", "analed", "ambien", "ativan", "anticipates",
            "babe", "blackjack", "beneficiary", "bum", "boobs",
            "casino", "casinos", "cialis", "cum", "cock", "cunt",
            "debt", "discreet",
            "erection", "enlargement", "equity", "ebony",
            "footjob", "fetish", "ftp", "fanny", "fuck", "fucked", "fuckin", "fucking",
            "gambling",
            "handjob", "hottie", "hump", "humped", "horny", "home finance", "home loan", "home owner", "hair loss", "hardcore",
            "incest", "intercourse", "india",
            "jizz",
            "knickers",
            "ligerie", "loan", "loans",
            "madam",        
            "orgasm", "offshore", 
            "penis", "pussy", "pharmacy", "phentermine", "poker", "pre approved", "porn", "porno", "panties",
            "refinance",
            "stock", "soma", "sex", "sexy", "sperm",
            "tits", "texas holdem", "teen", "teens",
            "unclaimed",
            "viagra", "valium", "vioxx", "vagina",
            "wank",
            "xxx", "xanax",
            "zolus"
            };

            //Create letter/number substitution regular expressions on all spamwords
            for (int i = 0; i < spamWords.Count; i++)
            {
                spamWords[i] = spamWords[i].Replace("a", "(a|4)");
                spamWords[i] = spamWords[i].Replace("b", "(b|13)");
                //spamWords[i] = spamWords[i].Replace("connection", "(connection|)");
                spamWords[i] = spamWords[i].Replace("d", "(d|0)");
                spamWords[i] = spamWords[i].Replace("e", "(e|3)");
                //spamWords[i] = spamWords[i].Replace("f", "(f|)");
                spamWords[i] = spamWords[i].Replace("g", "(g|9)");
                spamWords[i] = spamWords[i].Replace("h", "(h|11)");
                spamWords[i] = spamWords[i].Replace("i", "(i|1)");
                //spamWords[i] = spamWords[i].Replace("j", "(j|)");
                //spamWords[i] = spamWords[i].Replace("k", "(k|)");
                spamWords[i] = spamWords[i].Replace("l", "(l|1)");
                //spamWords[i] = spamWords[i].Replace("m", "(m|)");
                spamWords[i] = spamWords[i].Replace("n", "(n|1v)");
                spamWords[i] = spamWords[i].Replace("o", "(o|0)");
                //spamWords[i] = spamWords[i].Replace("p", "(p|)");
                //spamWords[i] = spamWords[i].Replace("q", "(q|)");
                spamWords[i] = spamWords[i].Replace("r", "(r|2)");
                spamWords[i] = spamWords[i].Replace("s", "(s|5)");
                spamWords[i] = spamWords[i].Replace("t", "(t|7)");
                spamWords[i] = spamWords[i].Replace("u", "(u|v|11)");
                spamWords[i] = spamWords[i].Replace("v", "(v|u|11)");
                spamWords[i] = spamWords[i].Replace("w", "(w|vv)");
                //spamWords[i] = spamWords[i].Replace("x", "(x|)");
                spamWords[i] = spamWords[i].Replace("y", "(y|7)");
                //spamWords[i] = spamWords[i].Replace("z", "(z|)");
                spamWords[i] = spamWords[i].Replace(" ", "(\\s?|-)");
            }

            // (Start of string or whitespace) SPAMWORD (End of string or whitespace)
            foreach (string word in spamWords)
            {
                if (s.IsMatch("(^|\\s)" + word + "($|\\s)"))
                    return true;
            }

            return false;
        }

    }
}
