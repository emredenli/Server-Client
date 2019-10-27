using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace emreServer
{
    class Program
    {
        static void Main(string[] args)
        {
            

            TcpListener tcpListener = new TcpListener(IPAddress.Any, 9999);
            tcpListener.Start();

            Console.WriteLine("Server has been started...");

            
            Socket clientSocket = tcpListener.AcceptSocket();


            
            if (!clientSocket.Connected)
            {
                Console.WriteLine("Cannot start server....");
            }
            else
            {
                
                while (true)
                {
                    Console.WriteLine("Client connection is provided...");

                    
                    NetworkStream networkStream = new NetworkStream(clientSocket);

                    
                    StreamWriter networkWriter = new StreamWriter(networkStream);
                    StreamReader networkReader = new StreamReader(networkStream);

                   
                    try
                    {
                        string clientMessage = networkReader.ReadLine();

                        Console.WriteLine("Client Message : " + clientMessage);

                        
                        int messageLength = clientMessage.Length;

                        
                        networkWriter.WriteLine(messageLength.ToString());

                        networkWriter.Flush();
                    }
                    catch
                    {
                        Console.WriteLine("Error : Server is shutting down!");
                        return;
                    }
                }
            }

            clientSocket.Close();

            Console.WriteLine("Server is shutting down...");
        }
    }
}