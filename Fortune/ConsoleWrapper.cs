using System;

namespace Fortune
{
	class ConsoleWrapper : IConsole
	{
		public void WriteLine(string output) => Console.WriteLine(output);
		public string ReadLine() => Console.ReadLine();
	}
}
