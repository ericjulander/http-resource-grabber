using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Dom;
using DataGrabber.ProcessingTools;

namespace DataGrabber
{
    public static class HtmlGrabber
    {

        public static string DEFAULT_NODE_PROCESS(IHtmlCollection<IElement> elements)
        {
            var message = "";
            foreach (var item in elements)
                message += (item.Text() + "\n");
            return message.Trim().Trim('\n');
        }
        public static async Task<string> ProcessElementsFromQuery(string Query, string Html, ProcessToolKit.DataNodeProcess process)
        {
            //Create a new context for evaluating webpages with the default config
            var context = BrowsingContext.New(Configuration.Default);
            //Create a document from a virtual request / response pattern
            var document = await context.OpenAsync(req => req.Content(Html));
            var NodesFromQuery = document.QuerySelectorAll(Query);
            string res = process(NodesFromQuery);
            return res;
        }
    }
}