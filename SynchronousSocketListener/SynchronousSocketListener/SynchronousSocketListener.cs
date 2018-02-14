using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class SynchronousSocketListener
{

    // dados a serem recebidos do cliente  
    public static string data = null;

    public static void StartListening()
    {
	  
        byte[] bytes = new Byte[1024];

        // Estabelecendo conexão usando a porta 11000
        IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
        IPAddress ipAddress = ipHostInfo.AddressList[0];
        IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);
  
        Socket listener = new Socket(ipAddress.AddressFamily,
            SocketType.Stream, ProtocolType.Tcp);

        try
        {
            // Esperar até 10 conexões
            listener.Bind(localEndPoint);
            listener.Listen(10);

            // Esperando conexões
            while (true)
            {
                Console.WriteLine("Waiting for a connection...");
                Socket handler = listener.Accept();
                data = null;

                // Processando conexão com o cliente e extraindo a mensagem
                while (true)
                {
                    bytes = new byte[1024];
                    int bytesRec = handler.Receive(bytes);
                    data += Encoding.ASCII.GetString(bytes, 0, bytesRec);
                    if (data.IndexOf("<EOF>") > -1)
                    {
                        break;
                    }
                }

                // Enviando resposta para o cliente
                Console.WriteLine("Text received : {0}", data);
                byte[] msg = Encoding.ASCII.GetBytes(data);

                handler.Send(msg);
                handler.Shutdown(SocketShutdown.Both);
                handler.Close();
            }

        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }

        Console.WriteLine("\nPress ENTER to continue...");
        Console.Read();

    }

    // Iniciando servidor
    public static int Main(String[] args)
    {
	Console.WriteLine("Start server...");
        StartListening();
        return 0;
    }
}