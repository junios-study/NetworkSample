using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            Socket listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            IPEndPoint listenEndPoint = new IPEndPoint(IPAddress.Parse("192.168.0.22"), 4000);
            listenSocket.Bind(listenEndPoint);

            listenSocket.Listen(10);

            bool isRunning = true;
            while (isRunning)
            {
                //동기, 블록킹 
                Socket clientSocket = listenSocket.Accept();

                byte[] buffer = new byte[1024];
                int RecvLength = clientSocket.Receive(buffer);
                if (RecvLength <= 0)
                {
                    //close
                    //error
                    isRunning = false;
                }

                Console.WriteLine(Encoding.UTF8.GetString(buffer));

                string message = "태규가 쏜다.";
                buffer = Encoding.UTF8.GetBytes(message);
                int SendLength = clientSocket.Send(buffer);
                if ( SendLength <= 0)
                {
                    isRunning = false;
                }

                clientSocket.Close();
            }

            listenSocket.Close();
        }
    }
}
