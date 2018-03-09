using System;
using System.Threading;

namespace MultithreadingApplication
{
    class ThreadCreation
    {
        public static void CallToChildThread()
        {
            try
            {
		// Recebe o nome do usuário
                Console.Write("Digite seu nome: ");
                String name = Console.ReadLine();
                Console.WriteLine("Iniciando thread");

                // são criadas 10 threads cada uma com um nome
                for (int counter = 1; counter <= 10; counter++)
                {
                    Thread.Sleep(500);
                    Console.WriteLine(name + " " + counter);
                }

                Console.WriteLine("Método completado");
            } catch(Exception e)
            {
                Console.WriteLine(e);
            }

        }

        // Iniciando Thread-Filha
        static void Main(string[] args)
        {
            ThreadStart childref = new ThreadStart(CallToChildThread);
            Console.WriteLine("Criando Thread");

            Thread childThread = new Thread(childref);
            childThread.Start();

            Console.ReadKey();
        }
    }
}