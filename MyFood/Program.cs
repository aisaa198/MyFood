using System;
using MyFood.Modules;
using Ninject;

namespace MyFood
{
    internal class Program
    {
        private static void Main()
        {
            Console.WriteLine("Welcome in MyFood!");
            var kernel = new StandardKernel();
            kernel.Load(new [] {new CliModule()});
            var programLoop = kernel.Get<ProgramLoop>();
            programLoop.Run();
        }
    }
}
