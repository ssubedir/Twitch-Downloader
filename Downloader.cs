using NReco.VideoConverter;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Twitch_Downloader_Console
{
    public class Downloader
    {


        //  Fetch video info
        public string FetchVideoData(string id)
        {

            WebClient client = new WebClient();

            Stream data;
            StreamReader reader;
            string s;

            try
            {
                // required http headers
                client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
                client.Headers.Add("Client-ID", "jzkbprff40iqj646a697cyrvl0zt2m6");

                data = client.OpenRead("https://api.twitch.tv/kraken/videos/v" + id);
                reader = new StreamReader(data);
                s = reader.ReadToEnd();

                data.Close();
                reader.Close();
                //Console.WriteLine(s);
                return s;
            }
            catch (Exception e)
            {

                return "-1";
            }

        }


        // Fetch m3u8 list

        public string FetchVideoM3List(string url)
        {
            WebClient client = new WebClient();

            Stream data;
            StreamReader reader;
            string s;

            try
            {
                // required http headers
                client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
                client.Headers.Add("Client-ID", "jzkbprff40iqj646a697cyrvl0zt2m6");
                data = client.OpenRead(url);

                reader = new StreamReader(data);

                string m3list = reader.ReadToEnd();


                data.Close();
                reader.Close();

                return m3list;
            }
            catch (Exception e) // invalid pram
            {
                return "-1";
            }
        }


        // Fetch Auth Token
        public string FetchTwitchAuth(string url)
        {
            WebClient client = new WebClient();

            Stream data;
            StreamReader reader;
            string s;
            try
            {
                // required http headers
                client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
                client.Headers.Add("Client-ID", "jzkbprff40iqj646a697cyrvl0zt2m6");
                data = client.OpenRead("https://api.twitch.tv/api/vods/" + url + "/access_token");
                reader = new StreamReader(data);
                s = reader.ReadToEnd();

                data.Close();
                reader.Close();
                //Console.WriteLine(s);
                return s;
            }
            catch (Exception e) // invalid pram
            {
                return "-1";
            }
        }

        // Fetch video Chunks
        public string FetchVideoChunks(string url)
        {

            WebClient client = new WebClient();

            Stream data;
            StreamReader reader;
            string s;

            try
            {
                // required http headers
                client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
                client.Headers.Add("Client-ID", "jzkbprff40iqj646a697cyrvl0zt2m6");
                data = client.OpenRead(url);
                reader = new StreamReader(data);
                s = reader.ReadToEnd();

                data.Close();
                reader.Close();
                //Console.WriteLine(s);
                return s;
            }
            catch (Exception e)
            {

                return "-1";
            }

        }


        // encode using ffmpeg
        internal void Encode(string v)
        {
            FFMpegConverter c = new FFMpegConverter();
            Random rnd = new Random();
            c.Invoke(string.Format("-f concat -safe 0 -i "+v+".txt -c copy " + v + rnd.Next(100) + ".mp4"));
        }


        // N/A
        public string vidextract(string vid)
        {

            string s = "https://www.twitch.tv/videos/" + vid;

            string[] list = vid.ToString().Split('/');

            if (list[4].Contains("v"))
            {
                list[4] = list[4].Replace("v", "");
            }

            return list[4];
        }



        // start download
        public string Start_Download(string[] video_chunks, string fname, string q)
        {
            
            // setup temp dir
            string dir = GetTemporaryDirectory();

            //Console.WriteLine("dir: " + dir);

            //Console.WriteLine("name: " + fname);

            //Console.WriteLine("qual: " + q);

            //Console.WriteLine(video_chunks.Length);



            Console.Write("\nDownloading Video ... ");

            StreamWriter sw = new StreamWriter(fname+".txt");
            WebClient client = new WebClient();
            client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
            client.Headers.Add("Client-ID", "jzkbprff40iqj646a697cyrvl0zt2m6");


            using (var progress = new ProgressBar())
            {
                for (int i = 0; i < video_chunks.Length; i++)
                {

                    try
                    {
                        client.DownloadFile("https://vod-secure.twitch.tv/" + fname + "/" + q + "/" + video_chunks[i], dir + "\\" + video_chunks[i]);
                        sw.WriteLine("file '" + dir + "\\" + video_chunks[i] + "'");
                        progress.Report((double)(from file in Directory.EnumerateFiles(dir, "*.ts", SearchOption.AllDirectories)
                                                 select file).Count() / video_chunks.Length);
                        //https://vod-secure.twitch.tv/4f7ff144c4_summit1g_23236224848_520939581/mobile/index-0000018329-RoHr-342160.ts
                    }
                    catch (Exception e)
                    {

                    }
                }
            }

            Console.WriteLine("Done Downloading.");


            sw.Close();
            return dir;
        }

        string GetTemporaryDirectory()
        {
            string tempDirectory = System.IO.Path.Combine(System.IO.Path.GetTempPath(), System.IO.Path.GetRandomFileName());
            Directory.CreateDirectory(tempDirectory);
            return tempDirectory;
        }
    }

}
