using System;

namespace Fortune.Tests
{
	public class FakeDateTimeOffset: IDateTimeOffset
	{
		public DateTimeOffset Now { get; set; }
	}
}
