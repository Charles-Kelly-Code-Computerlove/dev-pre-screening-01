using System;
using System.Globalization;

namespace Fortune
{
	public class App
	{
		private readonly FortuneCookie _fortuneCookie;
		private readonly IConsole _console;

		public App(FortuneCookie fortuneCookie, IConsole console)
		{
			_fortuneCookie = fortuneCookie;
			_console = console;
		}

		public void Run()
		{
			var name = AskForAndGetName();

			try
			{
				AskForAndGetDateOfBirth();
			}
			catch
			{
				_console.WriteLine("That's not a date of birth. No fortune for you!");
				return;
			}

			GiveFortune(name);
		}

		private DateTime AskForAndGetDateOfBirth()
		{
			_console.WriteLine("When were you born (dd/mm/yyyy)?");
			var dateOfBirthInput = _console.ReadLine();

			var dateOfBirthFormat = "dd/mm/yyyy";

			if (!DateTime.TryParseExact(dateOfBirthInput, dateOfBirthFormat, new CultureInfo("GB"),
				DateTimeStyles.None,
				out var dateOfBirth))
			{
				throw new Exception("Input from user is not in specified date format");
			}

			return dateOfBirth;
		}

		private void GiveFortune(string name)
		{
			var person = new Person {Name = name};

			_console.WriteLine(
				$"Hi {person.Name}!\nYour fortune for today is: {_fortuneCookie.GetTodaysFortune()}");
		}

		private string AskForAndGetName()
		{
			_console.WriteLine("What's your name? ");
			var name = _console.ReadLine();
			return name;
		}
	}
}
