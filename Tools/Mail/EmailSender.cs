using System;
using System.Configuration;
using System.Net.Mail;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Net;

namespace Toltech.Tools.Mail
{

    public class EmailSender
    {

        public EmailSender(string testingEmail, bool testing) : this(testingEmail, testing, null, null) { }
        public EmailSender(string testingEmail, bool testing, string host) : this(testingEmail, testing, host, null) { }
        public EmailSender(string testingEmail, bool testing, NetworkCredential credentials) : this(testingEmail, testing, null, credentials) { }
        public EmailSender(string testingEmail, bool testing, string host, NetworkCredential credentials)
        {
            _testing = testing;
            _testingEmail = testingEmail;
            _host = host;
            _credentials = credentials;

            _cc = new MailAddressCollection();
            _bcc = new MailAddressCollection();
            _attachments = new List<Attachment>();

            Priority = MailPriority.Normal;
        }

        private bool _testing;
        private string _testingEmail;
        private string _host;
        private NetworkCredential _credentials;

        private MailAddress _sender;
        private MailAddress _from;
        private MailAddress _recipient;
        private MailAddress _replyTo;
        private MailAddressCollection _cc;
        private MailAddressCollection _bcc;
        private List<Attachment> _attachments;

        public string SubjectPrefix { get; set; }
        public MailPriority Priority { get; set; }

        public virtual void SetSender(string name, string email)
        {
            _sender = new MailAddress(email, name);
        }

        public virtual void SetFrom(string name, string email)
        {
            _from = new MailAddress(email, name);
        }

        public virtual void SetRecipient(string name, string email)
        {
            _recipient = new MailAddress(email, name);
        }

        public virtual void SetReplyTo(string name, string email)
        {
            _replyTo = new MailAddress(email, name);
        }

        public virtual void AddCc(string name, string email)
        {
            _cc.Add(new MailAddress(email, name));
        }

        public virtual void AddBcc(string name, string email)
        {
            _bcc.Add(new MailAddress(email, name));
        }

        public virtual void AddAttachment(string filename, string contentType)
        {
            _attachments.Add(new Attachment(filename, contentType));
        }

        public virtual void AddAttachment(Stream stream, string name, string contentType)
        {
            _attachments.Add(new Attachment(stream, name, contentType));
        }

        ///<summary>
        ///Send a simple text email to a collection of recipients.
        ///</summary>
        ///<param name="sender">The address that is sending the email</param>
        ///<param name="recipients">The collection of addresses that are to receive the email</param>
        ///<param name="subject">The email subject</param>
        ///<param name="htmlBody">The HTML email body (a plain text version is automatically created)</param>
        ///<remarks></remarks>
        public virtual void Send(string subject, string body)
        {
            // Setup testing if required
            SetupTesting();

            // Ensure that the required values have been set
            EnsureValues(subject, body);

            // Create the MailMessage object and set its properties
            MailMessage mail = new MailMessage();
            mail.Priority = Priority;
            mail.BodyEncoding = Encoding.UTF8;
            mail.Subject = string.Concat(SubjectPrefix, subject);
            mail.SubjectEncoding = Encoding.UTF8;
            mail.To.Add(_recipient);                                        // Recipient
            mail.Sender = _sender;                                          // Sender (ie Tech Support)
            mail.From = _from ?? _sender;                                   // From (ie the CEO)
            if (_replyTo != null) mail.ReplyToList.Add(_replyTo);           // ReplyTo (ie the Secretary)
            foreach (var cc in _cc) mail.CC.Add(cc);                        // CC
            foreach (var bcc in _bcc) mail.Bcc.Add(bcc);                    // BCC
            foreach (var att in _attachments) mail.Attachments.Add(att);    // Attachments

            // Create and add the MailMessage plain text view (Uses SevenBit to avoid spam rating from .NET frameworks faulty base64 implementation)
            AlternateView plainTextView = AlternateView.CreateAlternateViewFromString(body.NlToBr().StripHtml(), Encoding.UTF8, "text/plain");
            plainTextView.TransferEncoding = System.Net.Mime.TransferEncoding.SevenBit;
            mail.AlternateViews.Add(plainTextView);

            // Create and add the MailMessage html view (Uses SevenBit to avoid spam rating from .NET frameworks faulty base64 implementation)
            AlternateView htmlView = AlternateView.CreateAlternateViewFromString(body, Encoding.UTF8, "text/html");
            htmlView.TransferEncoding = System.Net.Mime.TransferEncoding.SevenBit;
            mail.AlternateViews.Add(htmlView);

            // Create an SmtpClient object and send the MailMessage
            var smtp = new SmtpClient(_host ?? "127.0.0.1");
            smtp.Credentials = _credentials;
            smtp.Send(mail);

            // The mail has been sent, so reset the EmailSender
            Reset();
        }

        protected void SetupTesting()
        {
            if (_testing)
            {
                Reset();
                SetRecipient("Test Recipient", _testingEmail);
            }
        }

        protected void Reset()
        {
            _recipient = null;
            _cc.Clear();
            _bcc.Clear();
        }

        protected void EnsureValues(string subject, string body)
        {
            if (_sender == null)
                throw new Exception("SetSender() must be called before calling Send() on EmailSender.");

            if (_recipient == null)
                throw new Exception("SetRecipient() must be called before calling Send() on EmailSender.");

            if (string.IsNullOrWhiteSpace(subject))
                throw new ArgumentException("Subject cannot be null or empty.", "subject");

            if (string.IsNullOrWhiteSpace(body))
                throw new ArgumentException("Body cannot be null or empty.", "body");
        }

    }

}