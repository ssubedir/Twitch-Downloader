using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace Twitch_Downloader_Console
{
    public class Twitch
    {
        public Video v; // video info
        public Auth a;  // auth token 
        public string MainM3 = "";   // playlist m3list
        public string ChunksRaw = ""; // Video chunks raw data
        public string[] ChunksArr;  // video chunks
        public string Dir  = ""; // temp dir

        Downloader d = new Downloader();
        public Twitch()
        {
            
        }


        // check if video exists in twitch DB
        public string checkVideo(string vid)
        {

            string s = d.FetchVideoData(vid);

            if(s != "-1") // found
            {
                v = Video.FromJson(s); // parse JSON 
                
                return s;
            }
            else // not found
            {
                return s;
            }

              
        }


        // display video information

        public void displayInformation()
        {
            Console.WriteLine("\nTitle : "+v.Title);
            Console.WriteLine("Description : " + v.Description);
            Console.WriteLine("Game : " + v.Game);
            Console.WriteLine("Channel : " + v.Channel.DisplayName);
            Console.WriteLine("Url : " + v.Url);

        }

        // fetch tokens from twitch
        public string fetchTwitchAuth(string url)
        {
            string s = d.FetchTwitchAuth(url); // fetch auth token

            if (s != "-1") // token recieved
            {
                a = Auth.FromJson(s); // parse json token
                return s;
            }
            else // token not recieved
            {
                return s;
            }
        }


        // fetch video chunks
        public string fetchChunks(string url)
        {
            string s = d.FetchVideoChunks(url);

            if (s != "-1")  // retrived chunks
            {

                // write file

                ChunksRaw = s;

                ChunksArr = processChunks(s); // save chunks

                return s;
            }
            else  // failed
            {
                return s;
            }
        }


        public void tDownload(string q)
        {
            string [] id = v.Preview.ToString().Split('/');
            Dir = d.Start_Download(ChunksArr, id[4], q);
        }


        // process chunks and parse chunks file names
        private string [] processChunks(string s)
        {

            // parse
            string[] video_chunks = Regex.Matches(s, @".+\.ts")
            .OfType<Match>()
            .Select(m => m.Groups[0].Value)
            .ToArray();

            return video_chunks;
        }

        // fetch m3u8 from twitch
        public string fetchM3(string url)
        {
            MainM3 = d.FetchVideoM3List(url);
            return MainM3;
        }

        public string rr(string a)
        {
            return a.Remove(0, 1);
        }

        // auth token builder
        public string auth_builder(string nauth, string vidid, string nsig)
        {
            //"https://usher.twitch.tv/vod/{0}?nauthsig={1}&nauth={2}&allow_source=true&player=twitchweb&allow_spectre=true&allow_audio_only=true"
            return "https://usher.ttvnw.net/vod/" + vidid + "?nauth=" + nauth + "&nauthsig=" + nsig + "&allow_source=true&player=twitchweb&allow_spectre=true";
           
        }


        // delete leftover files from temp dir
        public void deleteFiles(string path)
        {
            string[] id = v.Preview.ToString().Split('/');
            var dir = new DirectoryInfo(path);
            foreach (var file in Directory.GetFiles(dir.ToString()))
            {
                File.Delete(file);
            }
            File.Delete(id[4]+".txt");
            Directory.Delete(path);
        }


        // encode files
        internal void EncodeFiles()
        {
            string[] id = v.Preview.ToString().Split('/');
            
            string dir = "";

            Console.Write("Encoding Video.... ");
            
                d.Encode(id[4]);
                
            Console.WriteLine("Done Encoding.");

            deleteFiles(Dir);
        }


        // parse resulation info 
        public string [] getResInfo(string[] a)
        {
            for (int i = 0; i < a.Length; i++)
            {
                Dictionary<string, string> keyValuePairs = a[i].Split(',').Select(value => value.Split('=')).ToDictionary(pair => pair[0], pair => pair[1]);

                a[i] =  keyValuePairs["VIDEO"].Replace("\"", "");

            }
            return a;
        }

        // parse resulation info 
        public string[] parseRes(string[] a)
        {

            for (int i = 0; i < a.Length; i++)
            {
                Dictionary<string, string> keyValuePairs = a[i].Split(',').Select(value => value.Split('=')).ToDictionary(pair => pair[0], pair => pair[1]);

                a[i] = keyValuePairs["RESOLUTION"].Replace("\"", "")+" - "+keyValuePairs["VIDEO"].Replace("\"", "");
            }

            return a;
        }

    }
}
