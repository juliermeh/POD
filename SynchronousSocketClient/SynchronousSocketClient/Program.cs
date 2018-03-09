using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class SynchronousSocketClient
{

    public static void StartClient()
    {
        // Data buffer  
        byte[] bytes = new byte[1024];

        // Estabelecendo conexão com servidor  
        try
        {
            // Usando a porta 11000 e o endereço de IP da máquina
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            IPEndPoint remoteEP = new IPEndPoint(ipAddress, 11000);

            // Criando um socket TCP/IP  
            Socket sender = new Socket(ipAddress.AddressFamily,
                SocketType.Stream, ProtocolType.Tcp);

            // Estabelecendo conexões e suas cláusulas de erro  
            try
            {
                sender.Connect(remoteEP);

                Console.WriteLine("Socket conectado a {0}",
                    sender.RemoteEndPoint.ToString());

                // Convertendo String em um array de bytes e enviando
                byte[] msg = Encoding.ASCII.GetBytes("Olá mundo! <EOF>");
                int bytesSent = sender.Send(msg);

                /* Console.WriteLine("Digite o valor de x: ");
                String x = Console.ReadLine();
                Console.WriteLine("Digite o valor de y: ");
                String y = Console.ReadLine();
                byte[] bytx = Encoding.ASCII.GetBytes(x);
                int bytxSent = sender.Send(bytx);
                byte[] byty = Encoding.ASCII.GetBytes(y);
                int bytySent = sender.Send(byty); */

                // Recebendo resposta 
                int bytesRec = sender.Receive(bytes);
                Console.WriteLine("Teste = {0}",
                    Encoding.ASCII.GetString(bytes, 0, bytesRec));

                // Fechando conexão 
                sender.Shutdown(SocketShutdown.Both);
                sender.Close();

            }
            catch (ArgumentNullException ane)
            {
                Console.WriteLine("ArgumentNullException: {0}", ane.ToString());
            }
            catch (SocketException se)
            {
                Console.WriteLine("SocketException: {0}", se.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine("Unexpected exception: {0}", e.ToString());
            }

        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
    }

    // Iniciando cliente
    public static int Main(String[] args)
    {
	Console.WriteLine("Iniciando cliente...");
        StartClient();


        Console.ReadLine();
        return 0;
    }
}