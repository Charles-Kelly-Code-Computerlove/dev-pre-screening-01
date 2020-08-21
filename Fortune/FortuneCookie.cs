using System;

namespace Fortune
{
	public class FortuneCookie
	{
		private readonly DateTimeOffset _now;

		public FortuneCookie(IDateTimeOffset dateTimeOffset)
		{
			_now = dateTimeOffset.Now;
		}

		public string GetTodaysFortune()
		{
			return GetFortuneForDate(_now);
		}

		public string GetFortuneForDate(DateTimeOffset date)
		{
			string fortune = date.DayOfWeek switch
			{
				DayOfWeek.Monday => "Bad luck falls on Mondays!",
				DayOfWeek.Tuesday => "Beware of figs!",
				DayOfWeek.Wednesday => "You will meet a tall, dark untimely end!",
				DayOfWeek.Thursday => "Avocadoes are lucky!",
				DayOfWeek.Friday => "Newlyweds should be avoided!",
				DayOfWeek.Saturday => "Everything's coming up Milhouse!",
				DayOfWeek.Sunday => "Fortune cookies are always untrue!",
				_ => "You appear to be in a universe with additional days of the week. Congratulations!"
			};

			return fortune;
		}
	}
}
