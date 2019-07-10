using System;
using System.Linq;
using System.Threading.Tasks;

namespace HttpGrabberFunctions
{
    public abstract class DataGrabber
    {

        private string _URL;
        public string URL
        {
            get
            {
                return _URL;
            }
            set
            {
                _URL = value;
            }
        }
        public DataGrabber() { }
        public DataGrabber(string URL)
        {
            System.Console.WriteLine("Running Base!");
            this.URL = URL;
        }

        public virtual async Task<string> GetResponse()
        {
            string result = "";
            try
            {
                if (_URL == null)
                    throw new Exception("No URL was specififed");
                result = await HttpHelper.MakeGetRequest(_URL);
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
                result = "Error!";
            }
            return result;
        }
        public virtual async Task<string> GetAuthResponse(string token)
        {
            string result = "";
            try
            {
                if (_URL == null)
                    throw new Exception("No URL was specififed");
                result = await HttpHelper.MakeAuthenticatedGetRequest(_URL, token);
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
                result = "Error!";
            }
            return result;
        }
    }

    public class GeneralGrabber : DataGrabber
    {
        public GeneralGrabber() : base()
        {

        }
        public GeneralGrabber(string url) : base(url)
        {

        }

    }
    public class WikiGrabber : DataGrabber
    {
        public WikiGrabber() : base()
        {

        }
        private static string TurnToQuery(string text){
            return string.Join("_",text.Split(" ").Select(item => item.ToLower()));
        }
        public WikiGrabber(string entry) : base("https://en.m.wikipedia.org/wiki/"+TurnToQuery(entry))
        {   
            System.Console.WriteLine("Running Inherited!");
            System.Console.WriteLine(this.URL);
        }

    }
    public class CanvasGrabber : DataGrabber
    {
        public CanvasGrabber() : base()
        {

        }
        private static string TurnToQuery(string text){
            return string.Join("_",text.Split(" ").Select(item => item.ToLower()));
        }
        public CanvasGrabber(string apipath) : base("https://byui.instructure.com"+apipath)
        {   
            System.Console.WriteLine("Running Inherited!");
            System.Console.WriteLine(this.URL);
        }

    }
    public class GifGrabber : DataGrabber
    {
        public GifGrabber() : base()
        {

        }
        private static string TurnToQuery(string text){
            return string.Join("-",text.Split(" ").Select(item => item.ToLower()));
        }
        public GifGrabber(string entry) : base("https://giphy.com/search/"+TurnToQuery(entry))
        {   
            System.Console.WriteLine(this.URL);
        }

    }
}