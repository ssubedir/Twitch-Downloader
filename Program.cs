using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Twitch_Downloader_Console
{
    class Program
    {
        

        static void Main(string[] args)
        {
            header();
            menu();
        }

        private static void menu()
        {
            int userInput;
            Console.WriteLine("\n\n1 : Download Twitch Video");
            Console.WriteLine("2 : Exit");
            do
            {
                Console.Write($"\n\nEnter a choice (1 - 2): ");
            } while (!int.TryParse(Console.ReadLine(), out userInput) || userInput < 1 || userInput > 2);


            if(userInput == 1)
            {
                _Start_();
            }
            else
            {
                Environment.Exit(1);
            }
        }

        static void header()
        {
            Console.WriteLine("----------------- TWITCH DOWNLOADER v1.0 -------------");
            Console.WriteLine("----------------- Bb: Rajan Subedi -------------------");
            Console.WriteLine("----------------- github.com/ssubedir ----------------");
        }




        // Download Video from twitch
        static void _Start_()
        {

            Console.Clear();

            header();

            Console.WriteLine("\n\n-------------------- VIDEO INFO ----------------------");

            string vid = "";


            Console.WriteLine("Enter valid video ids. [Example: 91119042]");
            Console.Write("Id: ");

            vid = Console.ReadLine();

            Twitch twitch = new Twitch();

            if(twitch.checkVideo(vid) != "-1")
            {
                Console.WriteLine("-------------------- VIDEO INFO ----------------------");

                // display requested video information 
                twitch.displayInformation();


                // fetch token from twitch
                if (twitch.fetchTwitchAuth(twitch.rr(twitch.v.Id)) != "-1") {
                    
                    // build Auth token

                    string build_auth = twitch.auth_builder(twitch.a.Token, twitch.rr(twitch.v.Id), twitch.a.Sig);


                    // retrive m3 list
                    string list = twitch.fetchM3(build_auth);

                    // extract urls
                    string[] urls = Regex.Matches(list, @"https:\/\/.+")
                        .OfType<Match>()
                        .Select(m => m.Groups[0].Value)
                        .ToArray();


                    // extract video resolutions
                    string[] res = twitch.parseRes(Regex.Matches(list, @"RESOLUTION=.+").OfType<Match>().Select(m => m.Groups[0].Value).ToArray());
                    string[] res2 = twitch.getResInfo(Regex.Matches(list, @"RESOLUTION=.+").OfType<Match>().Select(m => m.Groups[0].Value).ToArray());




                    // resolution selection

                    Console.WriteLine("\n-------------------- RESOLUTIONS ---------------------\n");

                    int userInput;
                    char useri;
                    for (int i = 0; i< res.Length; i++)
                    {
                        Console.WriteLine(i+" : "+res[i]);
                    }
                        Console.WriteLine((res.Length) + " : New Video");

                    do
                    {
                        Console.Write($"Enter a choice (0 - {res.Length}): ");
                    } while (!int.TryParse(Console.ReadLine(), out userInput) || userInput < 0 || userInput > res.Length);


                    if(userInput == res.Length)
                    {
                        Console.Clear();
                        header();

                        menu();
                    }
                    else
                    {

                        // start download

                        twitch.fetchChunks(urls[userInput]); // fetch video chunks
                        twitch.tDownload(res2[userInput]); // start download
                        twitch.EncodeFiles(); // encode files

                        string userii;

                        // try again?



                        do
                        {
                            Console.Write($"\n\nDo you want to download another video? Y/N : ");
                            userii = Console.ReadLine();

                            if ((userii == "y") || (userii == "n") || (userii == "Y") || (userii == "N"))
                                break;
                        } while (true);



                        if (userii == "y" | userii == "Y")
                        {
                            _Start_();
                        }
                        else
                        {
                            Environment.Exit(0);
                        }

                    }

                }
                else
                {
                    Console.WriteLine("Failed to fetch token.");
                }
                
            }
            else
            {
                Console.WriteLine("Fine not found or supported.");
                string useri;
               

                // try again?

                do
                {
                    Console.Write($"\n\nDo you want to try another video? Y/N : ");
                    useri = Console.ReadLine();
                    
                    if ((useri == "y") || (useri == "n") || (useri == "Y") || (useri == "N"))
                        break;
                } while (true);



                if (useri == "y" | useri == "Y")
                {
                    _Start_();
                }
                else
                {
                    Environment.Exit(0);
                }
            }


           
        }

    }
}
