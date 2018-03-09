using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class SynchronousSocketListener
{

    // dados a serem recebidos do cliente  
    public static string data, datx, daty = null;
    public static int x, y;

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
                Console.WriteLine("Aguardando conexão...");
                Socket handler = listener.Accept();
                string data, datx, daty = null;

                // Processando conexão com o cliente e extraindo a mensagem
                while (true)
                {
                    bytes = new byte[1024];
                   /* bytx = new byte[1024];
                    byty = new byte[1024];
                    int bytesRec = handler.Receive(bytes);
                    int bytesRecx = handler.Receive(bytx);
                    int bytesRecy = handler.Receive(byty);
                    data += Encoding.ASCII.GetString(bytes, 0, bytesRec);
                    datx += Encoding.ASCII.GetString(bytes, 0, bytesRec);
                    daty += Encoding.ASCII.GetString(bytes, 0, bytesRec); */
                    if (data.IndexOf("<EOF>") > -1)
                    {
                        break;
                    }
                }

                // Enviando resposta para o cliente
                Console.WriteLine("Texto recebido: {0}", data);
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

        Console.WriteLine("\nPressione ENTER para continuar...");
        Console.Read();

    }

    // Iniciando servidor
    public static int Main(String[] args)
    {
	Console.WriteLine("Iniciando servidor...");
        StartListening();
        return 0;
    }
}