﻿using System;
using System.Configuration;
using System.Security.Cryptography.X509Certificates;
using WebSocketSharp;
using WebSocketSharp.Net;
using WebSocketSharp.Server;

namespace Demo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var wssv = new WebSocketServer(System.Net.IPAddress.Parse("192.168.0.6"), 5050);
            wssv.AddWebSocketService<Echo>("/Echo");
            wssv.Start();
            if (wssv.IsListening)
            {
                Console.WriteLine("Listening on port {0}, and providing WebSocket services:", wssv.Port);
                foreach (var path in wssv.WebSocketServices.Paths)
                    Console.WriteLine("- {0}", path);
            }
            Console.WriteLine("\nPress Enter key to stop the server...");
            Console.ReadLine();

            wssv.Stop();
        }
    }
}
