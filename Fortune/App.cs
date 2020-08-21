using System;

namespace Fortune
{
	public class App
	{
		private readonly FortuneCookie _fortuneCookie;

		public App(FortuneCookie fortuneCookie)
		{
			_fortuneCookie = fortuneCookie;
		}

		public void Run()
		{
			Console.Write("What's your name? ");
			var name = Console.ReadLine();

			var person = new Person {Name = name};

			Console.WriteLine(
				"Hi {0}!\nYour fortune for today is: {1}",
				person.Name,
				_fortuneCookie.GetTodaysFortune());
		}
	}
}
