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
                Console.Write("Enter your name: ");
                String name = Console.ReadLine();
                Console.WriteLine("Child thread starts");

                // são criadas 10 threads cada uma com um nome
                for (int counter = 1; counter <= 10; counter++)
                {
                    Thread.Sleep(500);
                    Console.WriteLine(name + " " + counter);
                }

                Console.WriteLine("Child Thread Completed");
            }

        }

        // Iniciando Thread-Filha
        static void Main(string[] args)
        {
            ThreadStart childref = new ThreadStart(CallToChildThread);
            Console.WriteLine("In Main: Creating the Child thread");

            Thread childThread = new Thread(childref);
            childThread.Start();

            Console.ReadKey();
        }
    }
}