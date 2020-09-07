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
			var fakeConsole = GenerateFakeConsoleWithInputs();

			var fortuneCookie = new FortuneCookie(new DateTimeOffsetWrapper());
			var app = new App(fortuneCookie, fakeConsole);

			app.Run();

			fakeConsole.WrittenLines[0].Should().Be("What's your name? ");
		}

		[Test]
		public void AppRun_NameInputAsPeter_ThirdOutputHasGreetingUsingInputName()
		{
			var fakeConsole = new FakeConsole();
			fakeConsole.LinesToRead.Enqueue("Peter");
			fakeConsole.LinesToRead.Enqueue("01/01/1990");

			var fortuneCookie = new FortuneCookie(new DateTimeOffsetWrapper());
			var app = new App(fortuneCookie, fakeConsole);

			app.Run();

			fakeConsole.WrittenLines[2].Should().StartWith("Hi Peter!");
		}

		[TestCase("2020/09/07", "Bad luck falls on Mondays!")]
		[TestCase("2020/09/08", "Beware of figs!")]
		[TestCase("2020/09/09", "You will meet a tall, dark untimely end!")]
		[TestCase("2020/09/10", "Avocadoes are lucky!")]
		[TestCase("2020/09/11", "Newlyweds should be avoided!")]
		[TestCase("2020/09/12", "Everything's coming up Milhouse!")]
		[TestCase("2020/09/13", "Fortune cookies are always untrue!")]
		public void AppRun_WhenDayOfTheWeek_ThirdOutputReturnsCorrectFortuneForTheDayOfTheWeek(
			DateTimeOffset currentDate,
			string result)
		{
			var fakeConsole = GenerateFakeConsoleWithInputs();

			var fortuneCookie = new FortuneCookie(new FakeDateTimeOffset {Now = currentDate});
			var app = new App(fortuneCookie, fakeConsole);

			app.Run();

			fakeConsole.WrittenLines[2].Should().EndWith(result);
		}

		[Test]
		public void AppRun_SecondOutputAsksForYourBirthday()
		{
			var fakeConsole = GenerateFakeConsoleWithInputs();

			var fortuneCookie = new FortuneCookie(new DateTimeOffsetWrapper());
			var app = new App(fortuneCookie, fakeConsole);

			app.Run();

			fakeConsole.WrittenLines[1].Should().Be("When were you born (dd/mm/yyyy)?");
		}

		[Test]
		public void AppRun_WhenNormalUse_ReadsTwoInputs()
		{
			var fakeConsole = GenerateFakeConsoleWithInputs();

			var fortuneCookie = new FortuneCookie(new DateTimeOffsetWrapper());
			var app = new App(fortuneCookie, fakeConsole);

			app.Run();

			fakeConsole.LinesToRead.Count.Should().Be(0);
		}

		[Test]
		public void AppRun_WhenIncorrectDateEnteredForDateOfBirthday_ThirdOutputReturnsWarning()
		{
			var fakeConsole = new FakeConsole();
			fakeConsole.LinesToRead.Enqueue("Peter");
			fakeConsole.LinesToRead.Enqueue("incorrect date format");

			var fortuneCookie = new FortuneCookie(new DateTimeOffsetWrapper());
			var app = new App(fortuneCookie, fakeConsole);

			app.Run();

			fakeConsole.WrittenLines[2].Should().Be("That's not a date of birth. No fortune for you!");
		}

		private static FakeConsole GenerateFakeConsoleWithInputs()
		{
			var fakeConsole = new FakeConsole();
			fakeConsole.LinesToRead.Enqueue("name");
			fakeConsole.LinesToRead.Enqueue("01/01/1990");
			return fakeConsole;
		}
	}
}
