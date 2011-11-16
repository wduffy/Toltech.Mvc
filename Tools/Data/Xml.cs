using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Caching;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace Toltech.Mvc.Tools
{
    
    public static class XmlTools
    {

        /// <summary>
        /// Reads an rss document from a url, caches it for 1 hour and return an XSLT transformed value
        /// </summary>
        /// <param name="url">The url of the RSS feed</param>
        /// <param name="xslt">The absolute location of the XSLT file: /folder/file.xslt</param>
        /// <returns></returns>
        public static string GetRss(string url, string xslt)
        {
            return GetRss(url, xslt, new HttpContextWrapper(HttpContext.Current));
        }

        /// <summary>
        /// Reads an rss document from a url, caches it for 1 hour and return an XSLT transformed value
        /// </summary>
        /// <param name="url">The url of the RSS feed</param>
        /// <param name="xslt">The absolute location of the XSLT file: /folder/file.xslt</param>
        /// <param name="context">The HttpContextBase object that manages the cache</param>
        /// <returns></returns>
        public static string GetRss(string url, string xslt, HttpContextBase context)
        {
            return TranformXml(GetRss(url, context), xslt);
        }

        /// <summary>
        /// Reads an rss document from a url and caches it for 1 hour to increase efficiency on future requests
        /// </summary>
        /// <param name="url">The url of the RSS feed</param>
        /// <returns></returns>
        public static XmlDocument GetRss(string url)
        {
            return GetRss(url, new HttpContextWrapper(HttpContext.Current));
        }

        /// <summary>
        /// Reads an rss document from a url and caches it for 1 hour to increase efficiency on future requests
        /// </summary>
        /// <param name="url">The url of the RSS feed</param>
        /// <param name="context">The HttpContextBase object that manages the cache</param>
        /// <param name="url">The url of the RSS feed</param>
        /// <returns></returns>
        public static XmlDocument GetRss(string url, HttpContextBase context)
        {
            url = PathTools.EnsureProtocol(url, Protocol.Http);

            if (context.Cache[url] == null)
            {
                XmlDocument rss = new XmlDocument();
                rss.Load(url);
                context.Cache.Add(url, rss, null, DateTime.Now.AddHours(1), Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
            }

            return (XmlDocument)context.Cache[url];
        }

        /// <summary>
        /// Transforms xml via the IXPathNavigable interface using an XSLT stylesheet
        /// </summary>
        /// <param name="xml">The IXPathNavigable interface to transform</param>
        /// <param name="xslt">The location of the XSLT file: /folder/file.xslt</param>
        /// <returns></returns>
        public static string TranformXml(IXPathNavigable xml, string xslt)
        {
            return XmlTools.TranformXml(xml, xslt, new HttpContextWrapper(HttpContext.Current));
        }

        /// <summary>
        /// Transforms xml via the IXPathNavigable interface using an XSLT stylesheet
        /// </summary>
        /// <param name="xml">The IXPathNavigable interface to transform</param>
        /// <param name="xslt">The location of the XSLT file: /folder/file.xslt</param>
        /// <returns></returns>
        public static string TranformXml(IXPathNavigable xml, string xslt, HttpContextBase context)
        {
            string output = string.Empty;

            XslCompiledTransform xsl = new XslCompiledTransform();
            xsl.Load(PathTools.EnsurePhysical(xslt, context));

            using (MemoryStream memoryStream = new MemoryStream())
            {
                xsl.Transform(xml, null, memoryStream);
                memoryStream.Position = 0;

                using (StreamReader reader = new StreamReader(memoryStream))
                    output = reader.ReadToEnd();
            }

            return output;
        }

        /// <summary>
        /// Reads an XPathNavigator object and returns an XmlNamespaceManager containing all namespaces found within it. The default namespace is x.
        /// </summary>
        /// <param name="xpn">The XPathNavigator to be assessed for namespaces</param>
        /// <returns></returns>
        public static XmlNamespaceManager GetXmlNamespaceManager(XPathNavigator xpn)
        {
            xpn.MoveToFollowing(XPathNodeType.Element);

            XmlNamespaceManager xmlnsm = new XmlNamespaceManager(xpn.NameTable);
            xmlnsm.AddNamespace("x", xpn.NamespaceURI);

            foreach (KeyValuePair<string, string> xns in xpn.GetNamespacesInScope(XmlNamespaceScope.All))
                xmlnsm.AddNamespace(xns.Key, xns.Value);

            return xmlnsm;
        }

    }

}
