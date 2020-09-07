using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Fortune.Tests
{
	public class FakeConsole : IConsole
	{
		public List<string> WrittenLines { get; } = new List<string>();
		public Queue<string> LinesToRead { get; } = new Queue<string>();

		public void WriteLine(string output) => WrittenLines.Add(output);

		public string ReadLine() => LinesToRead.Dequeue();
	}
}
