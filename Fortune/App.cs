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

			DateTime dateOfBirth;
			try
			{
				dateOfBirth = AskForAndGetDateOfBirth();
			}
			catch
			{
				_console.WriteLine("That's not a date of birth. No fortune for you!");
				return;
			}

			if (dateOfBirth == DateTime.Parse("01/01/1995"))
			{
				_console.WriteLine($"Happy birthday, {name}!");
			}

			GiveFortune(name, dateOfBirth);
		}

		private DateTime AskForAndGetDateOfBirth()
		{
			_console.WriteLine("When were you born (dd/mm/yyyy)?");
			var dateOfBirthInput = _console.ReadLine();

			var dateOfBirthFormat = "dd/MM/yyyy";

			if (!DateTime.TryParseExact(dateOfBirthInput, dateOfBirthFormat, new CultureInfo("GB"),
				DateTimeStyles.None,
				out var dateOfBirth))
			{
				throw new Exception("Input from user is not in specified date format");
			}

			return dateOfBirth;
		}

		private void GiveFortune(string name, DateTime dateOfBirth)
		{
			var person = new Person {Name = name, DateOfBirth = dateOfBirth};

			_console.WriteLine(
				$"Hi {person.Name}!\nYour fortune for today is: {_fortuneCookie.GetTodaysFortune()}");

			_console.WriteLine(
				$"On the day you were born your fortune was: {_fortuneCookie.GetFortuneForDate(person.DateOfBirth)}");
		}

		private string AskForAndGetName()
		{
			_console.WriteLine("What's your name? ");
			var name = _console.ReadLine();
			return name;
		}
	}
}
