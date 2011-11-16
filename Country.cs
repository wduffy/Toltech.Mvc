using System;
using System.Collections.Generic;
using System.Globalization;

namespace Toltech.Mvc
{
    public class Country : ICloneable
    {
 
        // WD - References 23 August 09
        // http://en.wikipedia.org/wiki/ISO_3166-1
        // http://en.wikipedia.org/wiki/ISO_4217
        // http://www.iso.org/iso/support/currency_codes_list-1.htm

        private static string[,] _countries;
        private static string[,] Countries
        {
            get
            {
                if (_countries == null)
                {
                    _countries = new string[246, 6]
                    {
                    { "Afghanistan", "AF", "AFG", "004", "AFN", "971" },
                    { "Åland Islands", "AX", "ALA", "248", "EUR", "978" },
                    { "Albania", "AL", "ALB", "008", "ALL", "008" },
                    { "Algeria", "DZ", "DZA", "012", "DZD", "012" },
                    { "American Samoa", "AS", "ASM", "016", "USD", "840" },
                    { "Andorra", "AD", "AND", "020", "EUR", "978" },
                    { "Angola", "AO", "AGO", "024", "AOA", "973" },
                    { "Anguilla", "AI", "AIA", "660", "XCD", "951" },
                    { "Antarctica", "AQ", "ATA", "010", "???", "-1" },
                    { "Antigua and Barbuda", "AG", "ATG", "028", "XCD", "951" },
                    { "Argentina", "AR", "ARG", "032", "ARS", "032" },
                    { "Armenia", "AM", "ARM", "051", "AMD", "051" },
                    { "Aruba", "AW", "ABW", "533", "AWG", "533" },
                    { "Australia", "AU", "AUS", "036", "AUD", "036" },
                    { "Austria", "AT", "AUT", "040", "EUR", "978" },
                    { "Azerbaijan", "AZ", "AZE", "031", "AZN", "944" },
                    { "Bahamas", "BS", "BHS", "044", "BSD", "044" },
                    { "Bahrain", "BH", "BHR", "048", "BHD", "048" },
                    { "Bangladesh", "BD", "BGD", "050", "BDT", "050" },
                    { "Barbados", "BB", "BRB", "052", "BBD", "052" },
                    { "Belarus", "BY", "BLR", "112", "BYR", "974" },
                    { "Belgium", "BE", "BEL", "056", "EUR", "978" },
                    { "Belize", "BZ", "BLZ", "084", "BZD", "084" },
                    { "Benin", "BJ", "BEN", "204", "XOF", "952" },
                    { "Bermuda", "BM", "BMU", "060", "USD", "840" },
                    { "Bhutan", "BT", "BTN", "064", "INR", "356" },
                    { "Bolivia, Plurinational State of", "BO", "BOL", "068", "BOB", "068" },
                    { "Bosnia and Herzegovina", "BA", "BIH", "070", "BAM", "977" },
                    { "Botswana", "BW", "BWA", "072", "BWP", "072" },
                    { "Bouvet Island", "BV", "BVT", "074", "NOK", "578" },
                    { "Brazil", "BR", "BRA", "076", "BRL", "986" },
                    { "British Indian Ocean Territory", "IO", "IOT", "086", "GBP", "826" },
                    { "Brunei Darussalam", "BN", "BRN", "096", "BND", "096" },
                    { "Bulgaria", "BG", "BGR", "100", "BGN", "975" },
                    { "Burkina Faso", "BF", "BFA", "854", "XOF", "952" },
                    { "Burundi", "BI", "BDI", "108", "BIF", "108" },
                    { "Cambodia", "KH", "KHM", "116", "KHR", "116" },
                    { "Cameroon", "CM", "CMR", "120", "XAF", "950" },
                    { "Canada", "CA", "CAN", "124", "CAD", "124" },
                    { "Cape Verde", "CV", "CPV", "132", "CVE", "132" },
                    { "Cayman Islands", "KY", "CYM", "136", "KYD", "136" },
                    { "Central African Republic", "CF", "CAF", "140", "XAF", "950" },
                    { "Chad", "TD", "TCD", "148", "XAF", "950" },
                    { "Chile", "CL", "CHL", "152", "CLP", "152" },
                    { "China", "CN", "CHN", "156", "CNY", "156" },
                    { "Christmas Island", "CX", "CXR", "162", "AUD", "036" },
                    { "Cocos (Keeling) Islands", "CC", "CCK", "166", "AUD", "036" },
                    { "Colombia", "CO", "COL", "170", "COP", "170" },
                    { "Comoros", "KM", "COM", "174", "KMF", "174" },
                    { "Congo", "CG", "COG", "178", "XAF", "950" },
                    { "Congo, the Democratic Republic of the", "CD", "COD", "180", "CDF", "976" },
                    { "Cook Islands", "CK", "COK", "184", "NZD", "554" },
                    { "Costa Rica", "CR", "CRI", "188", "CRC", "188" },
                    { "Côte d'Ivoire", "CI", "CIV", "384", "XOF", "952" },
                    { "Croatia", "HR", "HRV", "191", "HRK", "191" },
                    { "Cuba", "CU", "CUB", "192", "CUP", "192" },
                    { "Cyprus", "CY", "CYP", "196", "EUR", "978" },
                    { "Czech Republic", "CZ", "CZE", "203", "CZK", "203" },
                    { "Denmark", "DK", "DNK", "208", "DKK", "208" },
                    { "Djibouti", "DJ", "DJI", "262", "DJF", "262" },
                    { "Dominica", "DM", "DMA", "212", "XCD", "951" },
                    { "Dominican Republic", "DO", "DOM", "214", "DOP", "214" },
                    { "Ecuador", "EC", "ECU", "218", "USD", "840" },
                    { "Egypt", "EG", "EGY", "818", "EGP", "818" },
                    { "El Salvador", "SV", "SLV", "222", "USD", "840" },
                    { "Equatorial Guinea", "GQ", "GNQ", "226", "XAF", "950" },
                    { "Eritrea", "ER", "ERI", "232", "ERN", "232" },
                    { "Estonia", "EE", "EST", "233", "EEK", "233" },
                    { "Ethiopia", "ET", "ETH", "231", "ETB", "230" },
                    { "Falkland Islands (Malvinas)", "FK", "FLK", "238", "FKP", "238" },
                    { "Faroe Islands", "FO", "FRO", "234", "DKK", "208" },
                    { "Fiji", "FJ", "FJI", "242", "FJD", "242" },
                    { "Finland", "FI", "FIN", "246", "EUR", "978" },
                    { "France", "FR", "FRA", "250", "EUR", "978" },
                    { "French Guiana", "GF", "GUF", "254", "EUR", "978" },
                    { "French Polynesia", "PF", "PYF", "258", "XPF", "953" },
                    { "French Southern Territories", "TF", "ATF", "260", "EUR", "978" },
                    { "Gabon", "GA", "GAB", "266", "XAF", "950" },
                    { "Gambia", "GM", "GMB", "270", "GMD", "270" },
                    { "Georgia", "GE", "GEO", "268", "GBP", "826" },
                    { "Germany", "DE", "DEU", "276", "DEM", "276" },
                    { "Ghana", "GH", "GHA", "288", "GHC", "288" },
                    { "Gibraltar", "GI", "GIB", "292", "GIP", "292" },
                    { "Greece", "GR", "GRC", "300", "GRD", "300" },
                    { "Greenland", "GL", "GRL", "304", "DKK", "208" },
                    { "Grenada", "GD", "GRD", "308", "XCD", "951" },
                    { "Guadeloupe", "GP", "GLP", "312", "EUR", "978" },
                    { "Guam", "GU", "GUM", "316", "USD", "840" },
                    { "Guatemala", "GT", "GTM", "320", "GTQ", "320" },
                    { "Guernsey", "GG", "GGY", "831", "GBP", "826" },
                    { "Guinea", "GN", "GIN", "324", "GNF", "324" },
                    { "Guinea-Bissau", "GW", "GNB", "624", "XOF", "952" },
                    { "Guyana", "GY", "GUY", "328", "GYD", "328" },
                    { "Haiti", "HT", "HTI", "332", "HTG", "332" },
                    { "Heard Island and McDonald Islands", "HM", "HMD", "334", "AUD", "036" },
                    { "Holy See (Vatican City State)", "VA", "VAT", "336", "EUR", "978" },
                    { "Honduras", "HN", "HND", "340", "HNL", "340" },
                    { "Hong Kong", "HK", "HKG", "344", "HKD", "344" },
                    { "Hungary", "HU", "HUN", "348", "HUF", "348" },
                    { "Iceland", "IS", "ISL", "352", "ISK", "352" },
                    { "India", "IN", "IND", "356", "INR", "356" },
                    { "Indonesia", "ID", "IDN", "360", "IDR", "360" },
                    { "Iran, Islamic Republic of", "IR", "IRN", "364", "IRR", "364" },
                    { "Iraq", "IQ", "IRQ", "368", "IQD", "368" },
                    { "Ireland", "IE", "IRL", "372", "IEP", "372" },
                    { "Isle of Man", "IM", "IMN", "833", "GBP", "826" },
                    { "Israel", "IL", "ISR", "376", "ILS", "376" },
                    { "Italy", "IT", "ITA", "380", "EUR", "978" },
                    { "Jamaica", "JM", "JAM", "388", "JMD", "388" },
                    { "Japan", "JP", "JPN", "392", "JPY", "392" },
                    { "Jersey", "JE", "JEY", "832", "GBP", "826" },
                    { "Jordan", "JO", "JOR", "400", "JOD", "400" },
                    { "Kazakhstan", "KZ", "KAZ", "398", "KZT", "398" },
                    { "Kenya", "KE", "KEN", "404", "KES", "404" },
                    { "Kiribati", "KI", "KIR", "296", "AUD", "036" },
                    { "Korea, Democratic People's Republic of", "KP", "PRK", "408", "KPW", "408" },
                    { "Korea, Republic of", "KR", "KOR", "410", "KRW", "410" },
                    { "Kuwait", "KW", "KWT", "414", "KWD", "414" },
                    { "Kyrgyzstan", "KG", "KGZ", "417", "KGS", "417" },
                    { "Lao People's Democratic Republic", "LA", "LAO", "418", "LAK", "418" },
                    { "Latvia", "LV", "LVA", "428", "LVL", "428" },
                    { "Lebanon", "LB", "LBN", "422", "LBP", "422" },
                    { "Lesotho", "LS", "LSO", "426", "LSL", "426" },
                    { "Liberia", "LR", "LBR", "430", "LRD", "430" },
                    { "Libyan Arab Jamahiriya", "LY", "LBY", "434", "LYD", "434" },
                    { "Liechtenstein", "LI", "LIE", "438", "CHF", "756" },
                    { "Lithuania", "LT", "LTU", "440", "LTL", "440" },
                    { "Luxembourg", "LU", "LUX", "442", "EUR", "978" },
                    { "Macau", "MO", "MAC", "446", "MOP", "446" },
                    { "Macedonia, the former Yugoslav Republic of", "MK", "MKD", "807", "MKD", "807" },
                    { "Madagascar", "MG", "MDG", "450", "MGA", "969" },
                    { "Malawi", "MW", "MWI", "454", "MWK", "454" },
                    { "Malaysia", "MY", "MYS", "458", "MYR", "458" },
                    { "Maldives", "MV", "MDV", "462", "MVR", "462" },
                    { "Mali", "ML", "MLI", "466", "XOF", "952" },
                    { "Malta", "MT", "MLT", "470", "MTL", "470" },
                    { "Marshall Islands", "MH", "MHL", "584", "USD", "840" },
                    { "Martinique", "MQ", "MTQ", "474", "EUR", "978" },
                    { "Mauritania", "MR", "MRT", "478", "MRO", "478" },
                    { "Mauritius", "MU", "MUS", "480", "MUR", "480" },
                    { "Mayotte", "YT", "MYT", "175", "EUR", "978" },
                    { "Mexico", "MX", "MEX", "484", "MXN", "484" },
                    { "Micronesia, Federated States of", "FM", "FSM", "583", "USD", "840" },
                    { "Moldova, Republic of", "MD", "MDA", "498", "MDL", "498" },
                    { "Monaco", "MC", "MCO", "492", "EUR", "978" },
                    { "Mongolia", "MN", "MNG", "496", "MNT", "496" },
                    { "Montenegro", "ME", "MNE", "499", "EUR", "978" },
                    { "Montserrat", "MS", "MSR", "500", "XCD", "951" },
                    { "Morocco", "MA", "MAR", "504", "MAD", "504" },
                    { "Mozambique", "MZ", "MOZ", "508", "MZN", "943" },
                    { "Myanmar", "MM", "MMR", "104", "MMK", "104" },
                    { "Namibia", "NA", "NAM", "516", "NAD", "516" },
                    { "Nauru", "NR", "NRU", "520", "AUD", "036" },
                    { "Nepal", "NP", "NPL", "524", "NPR", "524" },
                    { "Netherlands", "NL", "NLD", "528", "ANG", "532" },
                    { "Netherlands Antilles", "AN", "ANT", "530", "", "0" },
                    { "New Caledonia", "NC", "NCL", "540", "XPF", "953" },
                    { "New Zealand", "NZ", "NZL", "554", "NZD", "554" },
                    { "Nicaragua", "NI", "NIC", "558", "NIO", "558" },
                    { "Niger", "NE", "NER", "562", "XOF", "952" },
                    { "Nigeria", "NG", "NGA", "566", "NGN", "566" },
                    { "Niue", "NU", "NIU", "570", "NZD", "554" },
                    { "Norfolk Island", "NF", "NFK", "574", "AUD", "036" },
                    { "Northern Mariana Islands", "MP", "MNP", "580", "USD", "840" },
                    { "Norway", "NO", "NOR", "578", "NOK", "578" },
                    { "Oman", "OM", "OMN", "512", "OMR", "512" },
                    { "Pakistan", "PK", "PAK", "586", "PKR", "586" },
                    { "Palau", "PW", "PLW", "585", "USD", "840" },
                    { "Palestinian Territory, Occupied", "PS", "PSE", "275", "???", "-1" },
                    { "Panama", "PA", "PAN", "591", "PAB", "590" },
                    { "Papua New Guinea", "PG", "PNG", "598", "PGK", "598" },
                    { "Paraguay", "PY", "PRY", "600", "PYG", "600" },
                    { "Peru", "PE", "PER", "604", "PEN", "604" },
                    { "Philippines", "PH", "PHL", "608", "PHP", "608" },
                    { "Pitcairn", "PN", "PCN", "612", "NZD", "554" },
                    { "Poland", "PL", "POL", "616", "PLN", "985" },
                    { "Portugal", "PT", "PRT", "620", "EUR", "978" },
                    { "Puerto Rico", "PR", "PRI", "630", "USD", "840" },
                    { "Qatar", "QA", "QAT", "634", "QAR", "634" },
                    { "Réunion", "RE", "REU", "638", "EUR", "978" },
                    { "Romania", "RO", "ROU", "642", "RON", "946" },
                    { "Russian Federation", "RU", "RUS", "643", "RUB", "643" },
                    { "Rwanda", "RW", "RWA", "646", "RWF", "646" },
                    { "Saint Barthélemy", "BL", "BLM", "652", "EUR", "978" },
                    { "Saint Helena", "SH", "SHN", "654", "SHP", "654" },
                    { "Saint Kitts and Nevis", "KN", "KNA", "659", "XCD", "951" },
                    { "Saint Lucia", "LC", "LCA", "662", "XCD", "951" },
                    { "Saint Martin (French part)", "MF", "MAF", "663", "EUR", "978" },
                    { "Saint Pierre and Miquelon", "PM", "SPM", "666", "EUR", "978" },
                    { "Saint Vincent and the Grenadines", "VC", "VCT", "670", "XCD", "951" },
                    { "Samoa", "WS", "WSM", "882", "WST", "882" },
                    { "San Marino", "SM", "SMR", "674", "EUR", "978" },
                    { "São Tomé and Príncipe", "ST", "STP", "678", "STD", "678" },
                    { "Saudi Arabia", "SA", "SAU", "682", "SAR", "682" },
                    { "Senegal", "SN", "SEN", "686", "XOF", "952" },
                    { "Serbia", "RS", "SRB", "688", "RSD", "941" },
                    { "Seychelles", "SC", "SYC", "690", "SCR", "690" },
                    { "Sierra Leone", "SL", "SLE", "694", "SLL", "694" },
                    { "Singapore", "SG", "SGP", "702", "SGD", "702" },
                    { "Slovakia", "SK", "SVK", "703", "EUR", "978" },
                    { "Slovenia", "SI", "SVN", "705", "EUR", "978" },
                    { "Solomon Islands", "SB", "SLB", "090", "SBD", "090" },
                    { "Somalia", "SO", "SOM", "706", "SOS", "706" },
                    { "South Africa", "ZA", "ZAF", "710", "ZAR", "710" },
                    { "South Georgia and the South Sandwich Islands", "GS", "SGS", "239", "GBP", "826" },
                    { "Spain", "ES", "ESP", "724", "EUR", "978" },
                    { "Sri Lanka", "LK", "LKA", "144", "LKR", "144" },
                    { "Sudan", "SD", "SDN", "736", "SDG", "938" },
                    { "Suriname", "SR", "SUR", "740", "SRD", "968" },
                    { "Svalbard and Jan Mayen", "SJ", "SJM", "744", "NOK", "578" },
                    { "Swaziland", "SZ", "SWZ", "748", "SZL", "748" },
                    { "Sweden", "SE", "SWE", "752", "SEK", "752" },
                    { "Switzerland", "CH", "CHE", "756", "CHE", "947" },
                    { "Syrian Arab Republic", "SY", "SYR", "760", "SYP", "760" },
                    { "Taiwan, Province of China", "TW", "TWN", "158", "TWD", "901" },
                    { "Tajikistan", "TJ", "TJK", "762", "TJS", "972" },
                    { "Tanzania, United Republic of", "TZ", "TZA", "834", "TZS", "834" },
                    { "Thailand", "TH", "THA", "764", "THB", "764" },
                    { "Timor-Leste", "TL", "TLS", "626", "USD", "840" },
                    { "Togo", "TG", "TGO", "768", "XOF", "952" },
                    { "Tokelau", "TK", "TKL", "772", "NZD", "554" },
                    { "Tonga", "TO", "TON", "776", "TOP", "776" },
                    { "Trinidad and Tobago", "TT", "TTO", "780", "TTD", "780" },
                    { "Tunisia", "TN", "TUN", "788", "TND", "788" },
                    { "Turkey", "TR", "TUR", "792", "TRY", "949" },
                    { "Turkmenistan", "TM", "TKM", "795", "TMM", "795" },
                    { "Turks and Caicos Islands", "TC", "TCA", "796", "USD", "840" },
                    { "Tuvalu", "TV", "TUV", "798", "AUD", "036" },
                    { "Uganda", "UG", "UGA", "800", "UGX", "800" },
                    { "Ukraine", "UA", "UKR", "804", "UAH", "980" },
                    { "United Arab Emirates", "AE", "ARE", "784", "", "0" },
                    { "United Kingdom", "GB", "GBR", "826", "GBP", "826" },
                    { "United States", "US", "USA", "840", "USD", "840" },
                    { "United States Minor Outlying Islands", "UM", "UMI", "581", "USD", "840" },
                    { "Uruguay", "UY", "URY", "858", "UYU", "858" },
                    { "Uzbekistan", "UZ", "UZB", "860", "UZS", "860" },
                    { "Vanuatu", "VU", "VUT", "548", "VUV", "548" },
                    { "Venezuela, Bolivarian Republic of", "VE", "VEN", "862", "VEF", "937" },
                    { "Viet Nam", "VN", "VNM", "704", "VND", "704" },
                    { "Virgin Islands, British", "VG", "VGB", "092", "USD", "840" },
                    { "Virgin Islands, U.S.", "VI", "VIR", "850", "USD", "840" },
                    { "Wallis and Futuna", "WF", "WLF", "876", "XPF", "953" },
                    { "Western Sahara", "EH", "ESH", "732", "MAD", "504" },
                    { "Yemen", "YE", "YEM", "887", "YER", "886" },
                    { "Zambia", "ZM", "ZMB", "894", "ZMK", "894" },
                    { "Zimbabwe", "ZW", "ZWE", "716", "ZWL", "932" }
                    };
                }

                return _countries;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="countryCode">The ISO 3166-1 alpha-2 or alpha-3 county code for this country</param>
        public Country(string countryCode)
        {
            int alpha = countryCode.Length;

            // Check to see if an ISO 3166-1 alpha-2 or alpha-3 county code was supplied
            if (alpha < 2 || alpha > 3)
                throw new ArgumentException("Parameter must be ISO 3166-1 alpha-2 or alpha-3 formatted country code", "countryCode");

            // Check to see if Alpha-2 Country Code returns a grid row and create this object with that 
            for (int i = 0; i < Countries.GetUpperBound(0); i++)
                if (Countries[i, alpha - 1] == countryCode)
                {
                    _name = Countries[i, 0];
                    _isoRegionAlpha2Code = Countries[i, 1];
                    _isoRegionAlpha3Code = Countries[i, 2];
                    _isoRegionNumber = int.Parse(Countries[i, 3]);
                    _isoCurrencySymbol = Countries[i, 4];
                    _isoCurrencyNumber = int.Parse(Countries[i, 5]);
                }

            // If no country has been found throw an exception
            if (string.IsNullOrEmpty(_name))
                throw new Exception(string.Format("\"{0}\" is an unknown ISO 3166-1 alpha-{1} formatted country code", countryCode, alpha));
        }

        private string _name;
        private string _isoRegionAlpha2Code;
        private string _isoRegionAlpha3Code;
        private int _isoRegionNumber;
        private string _isoCurrencySymbol;
        private int _isoCurrencyNumber;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string ISORegionAlpha2Code
        {
            get { return _isoRegionAlpha2Code; }
            set { _isoRegionAlpha2Code = value; }
        }

        public string ISORegionAlpha3Code
        {
            get { return _isoRegionAlpha3Code; }
            set { _isoRegionAlpha3Code = value; }
        }

        public int ISORegionNumber
        {
            get { return _isoRegionNumber; }
            set { _isoRegionNumber = value; }
        }

        public string ISOCurrencyCode
        {
            get { return _isoCurrencySymbol; }
            set { _isoCurrencySymbol = value; }
        }

        public int ISOCurrencyNumber
        {
            get { return _isoCurrencyNumber; }
            set { _isoCurrencyNumber = value; }
        }

        internal static string[,] GetAllCountries()
        {
            return Countries;
        }

        public RegionInfo GetRegionInfo()
        {
            throw new NotImplementedException();
        }

        #region ICloneable Members

        public Country Clone() // Exposes strongly typed clone
        {
            return (Country)MemberwiseClone();
        }

        object ICloneable.Clone()
        {
            return Clone();
        }

        #endregion
        #region Operator Overloading

        public static bool operator ==(Country a, Country b)
        {
            if (b as object == null)
                return (a as object == null);
            else
                return a.ISORegionAlpha2Code == b.ISORegionAlpha2Code;
        }

        public static bool operator !=(Country a, Country b)
        {
            return !(a == b);
        }

        #endregion
        #region Base Class Overrides

        public override bool Equals(object obj)
        {
            return this == (Country)obj;
        }

        public override int GetHashCode()
        {
            return - ISORegionAlpha2Code.GetHashCode();
        }

        #endregion
    }
}
