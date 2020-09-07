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

			fakeConsole.WrittenLines[3].Should().EndWith(result);
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

		[Test]
		public void AppRun_WhenGivenDateOfBirthIsTuesday_FourthOutputGivesFortuneForTuesday()
		{
			var fakeConsole = new FakeConsole();
			fakeConsole.LinesToRead.Enqueue("Peter");
			fakeConsole.LinesToRead.Enqueue("01/09/2020");

			var fortuneCookie = new FortuneCookie(new DateTimeOffsetWrapper());
			var app = new App(fortuneCookie, fakeConsole);

			app.Run();

			fakeConsole.WrittenLines[4].Should().Be("On the day you were born your fortune was: Beware of figs!");
		}

		[Test]
		public void AppRun_WhenGivenDateOfBirthIsThursday_FourthOutputGivesFortuneForThursday()
		{
			var fakeConsole = new FakeConsole();
			fakeConsole.LinesToRead.Enqueue("Peter");
			fakeConsole.LinesToRead.Enqueue("03/09/2020");

			var fortuneCookie = new FortuneCookie(new DateTimeOffsetWrapper());
			var app = new App(fortuneCookie, fakeConsole);

			app.Run();

			fakeConsole.WrittenLines[4].Should().Be("On the day you were born your fortune was: Avocadoes are lucky!");
		}

		[Test]
		public void AppRun_WhenNameIsPeterAndTodayAndDateOfBirthIsJanuaryFirst_WishPeterHappyBirthday()
		{
			var fakeConsole = new FakeConsole();
			fakeConsole.LinesToRead.Enqueue("Peter");
			fakeConsole.LinesToRead.Enqueue("01/01/1995");

			var fortuneCookie = new FortuneCookie(new FakeDateTimeOffset { Now = DateTimeOffset.Parse("01/01/2020") });
			var app = new App(fortuneCookie, fakeConsole);

			app.Run();

			fakeConsole.WrittenLines[2].Should().Be("Happy birthday, Peter!");
		}

		[Test]
		public void AppRun_WhenNameIsJohnAndTodayAndDateOfBirthIsJanuaryFirst_WishJohnHappyBirthday()
		{
			var fakeConsole = new FakeConsole();
			fakeConsole.LinesToRead.Enqueue("John");
			fakeConsole.LinesToRead.Enqueue("01/01/1995");

			var fortuneCookie = new FortuneCookie(new FakeDateTimeOffset { Now = DateTimeOffset.Parse("01/01/2020") });
			var app = new App(fortuneCookie, fakeConsole);

			app.Run();

			fakeConsole.WrittenLines[2].Should().Be("Happy birthday, John!");
		}

		[Test]
		public void AppRun_DateOfBirthIsJanuaryFirstButTodayIsDifferentDate_DoNotWishHappyBirthday()
		{
			var fakeConsole = new FakeConsole();
			fakeConsole.LinesToRead.Enqueue("John");
			fakeConsole.LinesToRead.Enqueue("01/01/1995");

			var fortuneCookie = new FortuneCookie(new FakeDateTimeOffset { Now = DateTimeOffset.Parse("02/02/2020") });
			var app = new App(fortuneCookie, fakeConsole);

			app.Run();

			fakeConsole.WrittenLines[2].Should().NotBe("Happy birthday, John!");
		}

		[Test]
		public void AppRun_TodayIsJanuaryFirstButDateOfBirthIsDifferentDate_DoNotWishHappyBirthday()
		{
			var fakeConsole = new FakeConsole();
			fakeConsole.LinesToRead.Enqueue("John");
			fakeConsole.LinesToRead.Enqueue("02/02/1995");

			var fortuneCookie = new FortuneCookie(new FakeDateTimeOffset { Now = DateTimeOffset.Parse("01/01/2020") });
			var app = new App(fortuneCookie, fakeConsole);

			app.Run();

			fakeConsole.WrittenLines[2].Should().NotBe("Happy birthday, John!");
		}

		[Test]
		public void AppRun_TodayIsBirthdayForDateOfBirth_WishHappyBirthdayAndDoNotGreetAgain()
		{
			var fakeConsole = new FakeConsole();
			fakeConsole.LinesToRead.Enqueue("John");
			fakeConsole.LinesToRead.Enqueue("05/05/1985");

			var fortuneCookie = new FortuneCookie(new FakeDateTimeOffset { Now = DateTimeOffset.Parse("05/05/2020") });
			var app = new App(fortuneCookie, fakeConsole);

			app.Run();

			fakeConsole.WrittenLines[2].Should().Be("Happy birthday, John!");
			fakeConsole.WrittenLines[3].Should().Be("Your fortune for today is: Beware of figs!");
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
