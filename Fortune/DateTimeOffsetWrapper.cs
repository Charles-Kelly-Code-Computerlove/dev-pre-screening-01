using System;

namespace Fortune
{
	public class DateTimeOffsetWrapper : IDateTimeOffset
	{
		public DateTimeOffset Now => DateTimeOffset.Now;
	}
}
