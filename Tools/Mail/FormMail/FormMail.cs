using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net.Mail;
using System.Web;

namespace Toltech.Mvc.Tools
{

    /// <summary>
    /// Dynamically creates an address form to represent a collection of form controls.
    /// </summary>
    public class FormMail
    {

        private HttpContext _context;
        private MailAddressCollection _recipients;
        private string _from;
        private string _replyTo;
        private string _subject;
        private string _redirectPage;
        private string _successMessage;
        private string _googleAnalyticsAccount;
        private string _googleGoalsUrl;
        private int _refreshPause;
        private List<IFormMailItem> _formMailItems = new List<IFormMailItem>();

        public FormMail() : this(System.Web.HttpContext.Current)
        {
        }

        public FormMail(HttpContext context)
        {
            _context = context;
        }
        
        /// <summary>
        /// Gets or sets the from address from the value of the control specified in this property. Defaults to Web.Config SuperAdministratorBusiness.
        /// </summary>
        public string From
        {
            get
            {
                IFormMailItem formMailItem = _formMailItems.Find(delegate(IFormMailItem item) { return item.Name == _from; });

                if (formMailItem != null && !string.IsNullOrEmpty(formMailItem.Value))
                    return formMailItem.Value;
                else
                    return GetAppSettingValue("SuperAdministratorBusiness");
            }
            set
            {
                _from = value;
            }
        }

        /// <summary>
        /// Gets or sets the reply-to address to be used.
        /// </summary>
        public string ReplyTo
        {
            get
            {
                IFormMailItem formMailItem = _formMailItems.Find(delegate(IFormMailItem item) { return item.Name == _replyTo; });

                if (formMailItem != null && !string.IsNullOrEmpty(formMailItem.Value))
                    return formMailItem.Value;
                else
                    return "noreply@" + GetAppSettingValue("SuperAdministratorEmail").Split('@')[1];
            }
            set
            {
                _replyTo = value;
            }
        }

        /// <summary>
        /// Gets or sets the subject line of the address.
        /// </summary>
        public string Subject
        {
            get
            {
                if (string.IsNullOrEmpty(_subject))
                    _subject = string.Format("New {0} Website Enquiry", GetAppSettingValue("BusinessName"));

                return string.Format("{0}: {1}", GetAppSettingValue("EmailPrefix"), _subject);
            }
            set
            {
                _subject = value;
            }
        }

        /// <summary>
        /// Gets or sets the redirectpage of this address. If null then the redirect defaults to HTTP_REFERER.
        /// </summary>
        public string RedirectPage
        {
            get
            {
                if (string.IsNullOrEmpty(_redirectPage))
                    _redirectPage = _context.Request.ServerVariables["HTTP_REFERER"];

                return _redirectPage;
            }
            set
            {
                _redirectPage = value;
            }
        }

        /// <summary>
        /// Gets or sets the success message displayed once an address has been succesfully sent. If 
        /// null then defaults to "Thank You, We will be in contact with you shortly..."
        /// </summary>
        public string SuccessMessage
        {
            get
            {
                if (string.IsNullOrEmpty(_successMessage))
                    _successMessage = "Thank You,<br />We will be in contact with you shortly...";

                return _successMessage;
            }
            set
            {
                _successMessage = value;
            }
        }

        /// <summary>
        /// Gets or sets the length of time in seconds to pause when displaying SuccessMessage. If null then defaults to 4."
        /// </summary>
        public int RefreshPause
        {
            get
            {
                if (_refreshPause == 0)
                    _refreshPause = 4;

                return _refreshPause;
            }
            set
            {
                _refreshPause = value;
            }
        }

        /// <summary>
        /// Sets a Google Goal for tracking of successful form submissions
        /// </summary>
        /// <param name="googleAnalyticsAccount">The Google Analytics account (example: UA-0000000-1)</param>
        /// <param name="googleGoalsUrl">The Google Goals url that will be tracked (example: /G1/fake-url)</param>
        public void SetGoogleGoal(string googleAnalyticsAccount, string googleGoalsUrl)
        {
            if (string.IsNullOrEmpty(googleAnalyticsAccount))
                throw new ArgumentNullException("googleAnalyticsAccount", "You must supply a Google Analytics account (example: UA-0000000-1)");

            if (string.IsNullOrEmpty(googleGoalsUrl))
                throw new ArgumentNullException("googleGoalsUrl", "You must supply a Google Goals url for Google Analytics to track (example: /G1/fake-url)");

            _googleAnalyticsAccount = googleAnalyticsAccount;
            _googleGoalsUrl = googleGoalsUrl;
        }

#region " FormMailItem Methods "

        #region AddFormMailItem

        /// <summary>
        /// Adds a new control to the address.
        /// </summary>
        /// <param name="label">The label to be displayed next to the value.</param>
        /// <param name="value">The value to be displayed on the address.</param>
        public void AddFormMailItem(string label, string value)
        {
            AddFormMailItem(label, value, FormMailItemType.None);
        }

        /// <summary>
        /// Adds a new control to the address.
        /// </summary>
        /// <param name="label">The label to be displayed next to the value.</param>
        /// <param name="value">The value to be displayed on the address.</param>
        /// <param name="type">Binary representation of the data type in <paramref name="value"/>.</param>
        public void AddFormMailItem(string label, string value, FormMailItemType type)
        {
            AddFormMailItem(label, value, type, true);
        }

        /// <summary>
        /// Adds a new control to the address.
        /// </summary>
        /// <param name="label">The label to be displayed next to the value.</param>
        /// <param name="value">The value to be displayed on the address.</param>
        /// <param name="spamCheck">Enables or disables spam checking on <paramref name="value"/>.</param>
        public void AddFormMailItem(string label, string value, bool spamCheck)
        {
            AddFormMailItem(label, value, FormMailItemType.None, spamCheck);
        }

        /// <summary>
        /// Adds a new control to the address
        /// </summary>
        /// <param name="label">The label to be displayed next to the value.</param>
        /// <param name="value">The value to be displayed on the address.</param>
        /// <param name="type">Binary representation of the data type in <paramref name="value"/>.</param>
        /// <param name="spamCheck">Enables or disables spam checking on <paramref name="value"/>.</param>
        public void AddFormMailItem(string label, string value, FormMailItemType type, bool spamCheck)
        {
            _formMailItems.Add(new FormMailStringItem(label, value, type, spamCheck));
        }

        #endregion

        /// <summary>
        /// Loops through all control and creates the address structure of label/value rows.
        /// </summary>
        /// <param name="emailType">Specifies the address structure.</param>
        /// <returns></returns>
        private string GetFormMailItems(FormMailEmailType emailType)
        {
            if (_formMailItems.Count == 0)
                throw new Exception("At least one FormMailItem must be specified.");

            string output = string.Empty;

            for (int i = 0; i < _formMailItems.Count; i++)
                if ((_formMailItems[i].Type & FormMailItemType.RobotChecker) == 0)
                {
                    if (emailType == FormMailEmailType.Html)
                    {
                        output += "<tr style=\"background-color: " + ((i % 2 == 0) ? "#ffffff" : "#e0e0e0") + ";\">" + Environment.NewLine +
                                    "<td style=\"width: 150px;\"><strong>" + _formMailItems[i].Name + ":</strong></td>" + Environment.NewLine +
                                    "<td>" + _formMailItems[i].Value + "</td>" + Environment.NewLine +
                                  "</tr>" + Environment.NewLine;
                    }
                    else
                    {
                        output += _formMailItems[i].Name + ": " + _formMailItems[i].Value + Environment.NewLine;
                    }
                }

            if (emailType == FormMailEmailType.Html)
            {
                return "<html>" + Environment.NewLine +
                            "<table style=\"width: 100%; font: 13px arial, verdana, sans-serif;\" cellpadding=\"4\" cellspacing=\"0\">" + Environment.NewLine +
                                output +
                            "</table>" + Environment.NewLine +
                       "</html>";
            }
            else
            {
                return output;
            }
        }

#endregion
#region " Recipient Methods "

        /// <summary>
        /// Adds a recipient to the address delivery list.
        /// </summary>
        /// <param name="address">The address of the recipient in address format. (example: name@address.domain)</param>
        /// <param name="displayName">The name to be displayed instead of <paramref name="address"/>.</param>
        public void AddRecipient(string address, string displayName)
        {
            if (_recipients == null)
                _recipients = new MailAddressCollection();

            _recipients.Add(new MailAddress(address, displayName));
        }

        /// <summary>
        /// Returns the list of recipients. If the system is InDev then only returns the InDevEmail.
        /// </summary>
        /// <returns></returns>
        private MailAddressCollection GetRecipients()
        {
            if (_recipients == null || _recipients.Count == 0)
                throw new Exception("At least one recipient must be specified.");

            if (bool.Parse(GetAppSettingValue("InDev")))
            {
                _recipients.Clear();
                _recipients.Add(new MailAddress(GetAppSettingValue("InDevEmail"), "In Development Recipient"));
            }

            return _recipients;
        }

#endregion

        /// <summary>
        /// Parses all data and sends the address to all recipients
        /// </summary>
        public void Send()
        {
            string response;
            string[] errors = GetErrors();

            if (errors.Length > 0)
                response = GetResponse(HtmlType.Error, errors);
            else
            {
                SmtpClient smtp = new SmtpClient();

                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(GetAppSettingValue("SuperAdministratorEmail"), From);
                    mail.ReplyToList.Add(new MailAddress(ReplyTo));
                    mail.Subject = Subject;
                    mail.Priority = MailPriority.High;

                    AlternateView textView = AlternateView.CreateAlternateViewFromString(GetFormMailItems(FormMailEmailType.Text), null, "text/plain");
                    textView.TransferEncoding = System.Net.Mime.TransferEncoding.SevenBit;
                    mail.AlternateViews.Add(textView);

                    AlternateView htmlView = AlternateView.CreateAlternateViewFromString(GetFormMailItems(FormMailEmailType.Html), null, "text/html");
                    htmlView.TransferEncoding = System.Net.Mime.TransferEncoding.SevenBit;
                    mail.AlternateViews.Add(htmlView);

                    foreach (MailAddress mailAddress in GetRecipients())
                    {
                        mail.To.Clear();
                        mail.To.Add(mailAddress);
                        smtp.Send(mail);
                    }
                }

                response = GetResponse(HtmlType.Success);
            }

            _context.Response.Write(response);
            _context.Response.End();
        }

       

#region " Helpers "

        private string GetAppSettingValue(string value)
        {
            NameValueCollection appSettings = (NameValueCollection)_context.GetSection("appSettings");
            return appSettings[value];
        }

        private string[] GetErrors()
        {
            List<string> errors = new List<string>();
            

            //Check that the submitted form has come from the same server and not a zombie machine posting to the "FORM ACTION"
            if (!_context.Request.ServerVariables["HTTP_REFERER"].Contains(_context.Request.ServerVariables["HTTP_HOST"]))
            {
                errors.Add("\"REMOTE ACCESS DENIED\"......message cancelled.");
                return errors.ToArray();
            }

            foreach (IFormMailItem formMailItem in _formMailItems)
            {

                //If this is a "RobotChecker" type check that it has no content 
                if ((formMailItem.Type & FormMailItemType.RobotChecker) > 0 && !string.IsNullOrEmpty(formMailItem.Value))
                {
                    errors.Clear();
                    errors.Add("\"ROBOT SUBMISSION DETECTED\"......message cancelled.");
                    break;
                }

                //If "SpamCheck" is enabled on this FormMailItem check to see if it contains spam
                if (formMailItem.SpamCheck && formMailItem.Value.ContainsSpam())
                {
                    errors.Clear();
                    errors.Add("\"SPAM DETECTED\"......message cancelled.");
                    break;
                }

                //If "IsRequired" is true for this FormMailItem check that it contains a value
                if ((formMailItem.Type & FormMailItemType.Required) > 0 && String.IsNullOrEmpty(formMailItem.Value))
                    errors.Add("\"" + formMailItem.Name + "\" is a required field.");

                //If "IsEmail" is true for this FormMailItem check that it contains a valid address address
                if ((formMailItem.Type & FormMailItemType.Email) > 0 && !string.IsNullOrEmpty(formMailItem.Value) && !formMailItem.Value.IsEmail())
                    errors.Add("\"" + formMailItem.Name + "\" must be a valid address address.");
            }

            return errors.ToArray();
        }

        private enum HtmlType
        {
            Error,
            Success
        }

        private string GetResponse(HtmlType type, params string[] errors)
        {
            // Create the basic html structure of the response
            string html = @"<!DOCTYPE html PUBLIC ""-//W3C//DTD XHTML 1.1//EN"" ""http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd"">
                            <html xmlns=""http://www.w3.org/1999/xhtml"">
                            <head>
                                <meta http-equiv=""Content-Type"" content=""text/html; charset=iso-8859-1"" />
                                <title>Form Submission</title>
                                {metaRefresh}
                            </head>
                            <body>
                                <div style=""font: 13px arial, verdana, sans-serif;
                                             color: red;
                                             width: 400px;
                                             margin: 100px auto 0px auto;
                                             padding: 20px;
                                             border: 1px solid black;
                                             background-color: #efefef;"">
                                    {responseMessage}
                                    {googleAnalytics}
                                </div>
                            </body>
                            </html>";

            // Set the refresh pause and redirect
            if (type == HtmlType.Success)
                html = html.Replace("{metaRefresh}", string.Format("<meta http-equiv=\"refresh\" content=\"{0};URL={1}\" />", RefreshPause.ToString(), RedirectPage));
            else
                html = html.Replace("{metaRefresh}", string.Empty);

            // Display the success message or the list of errors
            if (type == HtmlType.Success)
            {
                html = html.Replace("{responseMessage}", string.Format("{0}<br /><br /><em>(please wait to be redirected...)</em>", SuccessMessage));
            }
            else
            {
                string error = string.Empty;
                foreach (string e in errors) error += string.Format("&bull; {0}<br />", e);
                error += "<br /><input type=\"button\" value=\"&laquo;&laquo;&laquo; back to form\" onclick=\"history.back(1)\" />";

                html = html.Replace("{responseMessage}", error);
            }
        
            // Create the Google Analytics code if required
            if (type == HtmlType.Success && !string.IsNullOrEmpty(_googleAnalyticsAccount))
            {
                string googleAnalytics = @"<script type=""text/javascript"">
                                               var gaJsHost = ((""https:"" == document.location.protocol) ? ""https://ssl."" : ""http://www."");
                                               document.write(unescape(""%3Cscript src='"" + gaJsHost + ""google-analytics.com/ga.js' type='text/javascript'%3E%3C/script%3E""));
                                            </script>
                                            <script type=""text/javascript"">
                                               try
                                               {{
                                                    var pageTracker = _gat._getTracker(""{0}"");
                                                    pageTracker._trackPageview(""{1}"");
                                               }}
                                               catch(err)
                                               {{
                                               }}
                                            </script>";

                html = html.Replace("{googleAnalytics}", string.Format(googleAnalytics, _googleAnalyticsAccount, _googleGoalsUrl));
            }
            else
            {
                html = html.Replace("{googleAnalytics}", string.Empty);
            }

            return html;
        }

#endregion

    }
}