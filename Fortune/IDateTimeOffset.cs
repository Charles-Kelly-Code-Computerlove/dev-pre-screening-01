using System;

namespace Fortune
{
	public interface IDateTimeOffset
	{
		DateTimeOffset Now { get; }
	}
}
