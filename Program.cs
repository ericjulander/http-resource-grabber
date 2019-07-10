using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AngleSharp.Dom;
using DataGrabber;
using HttpGrabberFunctions;
using static DataGrabber.HtmlGrabber;
using DataGrabber.ProcessingTools;

namespace Html_Parsing
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // await GetTrumpData();
            //await DownloadGifs();
            var grabber = new CanvasGrabber("/api/v1/courses/59796/group_categories");
            var data = await grabber.GetAuthResponse(System.Environment.GetEnvironmentVariable("API_TOKEN"));
            System.IO.File.WriteAllText("./output/group_categories.json",data);
        }

        private static async Task DownloadGifs()
        {
            var giphy = new GifGrabber("Thank You");
            var html = await giphy.GetResponse();
            var procez = new List<ProcessToolKit.ElementProcess>(){
                (elements)=>
                {
                    var SortElements = (IEnumerable<IElement>)elements;
                    var attrs = SortElements.First().GetType().GetProperties();
                    foreach(var prop in attrs)
                        System.Console.WriteLine(prop.GetValue(SortElements.First()));
                    
                    return SortElements;
                }
            };
            var data = await HtmlGrabber.ProcessElementsFromQuery("div", html, ProcessToolKit.DoProcessesOnElements(procez));

        }
        private static async Task GetTrumpData()
        {
            // Goes to Trump's wikipedia article and then finds all mentions of military stuff and septemeber.
            var grabber = new WikiGrabber("Donald Trump");
            var html = await grabber.GetResponse();
            var procez = new List<ProcessToolKit.ElementProcess>(){
                (elements)=>
                {
                    var SortElements = (IEnumerable<IElement>)elements;
                    System.Console.WriteLine("Process 1");
                    return ProcessToolKit.GetElementsFromQuery(elements, "September");
                },
                (elements)=>
                {
                    System.Console.WriteLine("Process 2");
                    return ProcessToolKit.GetElementsFromQuery(elements, "military");
                }

            };
            var data = await HtmlGrabber.ProcessElementsFromQuery("#bodyContent p", html, ProcessToolKit.DoProcessesOnElements(procez));
            System.IO.File.WriteAllText("./output/trump.txt", data);
        }
    }
}
