using System;
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

		[TestCase("2020/09/07", "Bad luck falls on Mondays!")]
		[TestCase("2020/09/08", "Beware of figs!")]
		[TestCase("2020/09/09", "You will meet a tall, dark untimely end!")]
		[TestCase("2020/09/10", "Avocadoes are lucky!")]
		[TestCase("2020/09/11", "Newlyweds should be avoided!")]
		[TestCase("2020/09/12", "Everything's coming up Milhouse!")]
		[TestCase("2020/09/13", "Fortune cookies are always untrue!")]
		public void AppRun_WhenDayOfTheWeek_SecondOutputReturnsCorrectFortuneForTheDayOfTheWeek(
			DateTimeOffset currentDate,
			string result)
		{
			var fakeConsole = new FakeConsole();
			fakeConsole.LinesToRead.Enqueue("Peter");

			var fortuneCookie = new FortuneCookie(new FakeDateTimeOffset {Now = currentDate});
			var app = new App(fortuneCookie, fakeConsole);

			app.Run();

			fakeConsole.WrittenLines[1].Should().EndWith(result);
		}
	}
}
