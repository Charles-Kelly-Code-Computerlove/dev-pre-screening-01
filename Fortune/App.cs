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
			_console.WriteLine("What's your name? ");
			var name = _console.ReadLine();

			_console.WriteLine("When were you born (dd/mm/yyyy)?");

			var person = new Person {Name = name};

			_console.WriteLine(
				$"Hi {person.Name}!\nYour fortune for today is: {_fortuneCookie.GetTodaysFortune()}");
		}
	}
}
