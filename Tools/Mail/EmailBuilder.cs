using System;
using System.IO;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Hosting;
using System.Collections.Generic;

namespace Toltech.Tools.Mail
{

    internal class EmailBuilderAction 
    {
        public string Key { get; private set; }
        public object Value { get; private set; }
        public string Formatter { get; private set; }

        public EmailBuilderAction(string key, object value, string formatter)
        {
            Key = key;
            Value = value;
            Formatter = formatter;
        }

        public override bool Equals(object obj)
        {
            var action = obj as EmailBuilderAction;

            if (obj == null)
                return false;
            else
                return this.Key == action.Key;
        }

        public override int GetHashCode()
        {
            return this.Key.GetHashCode();
        }

        public override string ToString()
        {
            return this.Key;
        }
    }

    public class EmailBuilder
    {
        private string DelimeterKey { get; set; }
        private Queue<EmailBuilderAction> Actions { get; set; }
        private Queue<string> Templates { get; set; }
        private string Header { get; set; }
        private string Footer { get; set; }

        public EmailBuilder(string template) : this(template, null, null) { }
        public EmailBuilder(string template, string header, string footer)
        {
            // Build to allow scalability to handle multiple templates, just not had time
            DelimeterKey = "@";
            Actions = new Queue<EmailBuilderAction>();
            Templates = new Queue<string>();

            Templates.Enqueue(ReadTemplate(template));
            Header = ReadTemplate(header);
            Footer = ReadTemplate(footer);
        }

        public void AddAction(string key, object value)
        {
            AddAction(key, value, null);
        }

        public void AddAction(string key, object value, string formatter)
        {
            if (!key.StartsWith(DelimeterKey))
                key = string.Concat(DelimeterKey, key);

            if (value == null)
                throw new ArgumentException("Parameter cannot be null or empty.", "value");

            var action = new EmailBuilderAction(key, value, formatter);

            if (!Actions.Contains(action))
                Actions.Enqueue(action);
        }

        public string Build()
        {
            var output = string.Concat(Header, Templates.Dequeue(), Footer);
            
            // Replace all keys with values
            foreach (var action in Actions)
                output = output.Replace(action.Key, (action.Value is IFormattable) ? (action.Value as IFormattable).ToString(action.Formatter, null) : action.Value.ToString());

            // Check for unreplaced keys
            var matches = Regex.Matches(output, string.Concat(DelimeterKey, "\\w+"));
            if (matches.Count > 0)
            {
                var sb = new StringBuilder();
                sb.AppendLine("The following actions have not been added.");

                foreach (Match m in matches)
                    sb.AppendLine(m.Value);

                throw new Exception(sb.ToString());
            }

            return output;
        }

        protected string ReadTemplate(string location)
        {
            string output = null;

            if (!string.IsNullOrEmpty(location))
            {
                // Get the physical path for the templates location (can be used within non HttpContext thread)
                string path = HostingEnvironment.MapPath(location);

                // Check that a template exists at the specified location
                if (!File.Exists(path))
                    throw new Exception(string.Format("A template could not be found at '{0}'", path));

                // Read the template
                output = File.ReadAllText(path);
            }

            return output;
        }

    }
}
