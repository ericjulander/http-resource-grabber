using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using AngleSharp.Dom;

namespace DataGrabber.ProcessingTools
{
    public static class ProcessToolKit
    {

        public delegate string DataNodeProcess(IHtmlCollection<IElement> elements);
        public delegate IEnumerable<IElement> ElementProcess(IEnumerable<IElement> elements);
        public static IEnumerable<IElement> GetElementsFromQuery(IEnumerable<IElement> elements, string query)
        {
            System.Console.WriteLine(elements.Count());
            elements = elements.Where(element => element.Text().Contains(query));
            System.Console.WriteLine(elements.Count());
            System.Console.WriteLine();
            return elements;
        }
        public static IEnumerable<IElement> GetElementsFromQuery(IEnumerable<IElement> elements, Regex query)
        {
            elements.Where(element => query.IsMatch(element.Text()));
            return elements;
        }

        public static DataNodeProcess GetLinesWithMentionOf(string query)
        {
            return (elements) =>
            {
                var NewElements = (IEnumerable<IElement>)elements;
                NewElements = GetElementsFromQuery(NewElements, query);
                var ElementText = elements.Select(element => element.Text()).ToArray();
                return string.Join("\n", ElementText);
            };

        }

        public static DataNodeProcess GetLinesWithMentionOf(Regex exp)
        {
            return (elements) =>
           {
               var NewElements = GetElementsFromQuery(elements, exp);
               var ElementText = NewElements.Select(element => element.Text()).ToArray();
               return string.Join("\n", ElementText);
           };
        }

        public static DataNodeProcess DoProcessesOnElements(List<ElementProcess> processes)
        {
            return (elements) =>
            {
                var SortedElements = (IEnumerable<IElement>)elements;
                while (processes.Count > 0)
                {
                    SortedElements = processes[0](SortedElements);
                    processes.RemoveAt(0);
                }
                var results = (from remaining in SortedElements select remaining.Text()).ToArray();
                return string.Join("\n", results);
            };
        }
    }
}