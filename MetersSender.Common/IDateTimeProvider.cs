namespace MetersSender.Common
{
	public interface IDateTimeProvider
	{
		DateTime Now { get; }

		DateTime UtcNow { get; }
	}
}
