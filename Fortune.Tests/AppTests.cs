using FluentAssertions;
using NUnit.Framework;

namespace Fortune.Tests
{
	public class Tests
	{
		[Test]
		public void AppRun_NameIsGiven_FirstOutputAsksForYourName()
		{
			var fakeConsole = new FakeConsole();
			fakeConsole.LinesToRead.Enqueue("name");

			var fortuneCookie = new FortuneCookie(new DateTimeOffsetWrapper());
			var app = new App(fortuneCookie, fakeConsole);

			app.Run();

			fakeConsole.WrittenLines[0].Should().Be("What's your name? ");
		}

		[Test]
		public void AppRun_NameInputAsPeter_SecondOutputHasGreetingUsingInputName()
		{
			var fakeConsole = new FakeConsole();
			fakeConsole.LinesToRead.Enqueue("Peter");

			var fortuneCookie = new FortuneCookie(new DateTimeOffsetWrapper());
			var app = new App(fortuneCookie, fakeConsole);

			app.Run();

			fakeConsole.WrittenLines[1].Should().StartWith("Hi Peter!");
		}
	}
}
