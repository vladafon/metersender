namespace MetersSender.Common
{
	public class DateTimeProvider : IDateTimeProvider
	{
		public DateTime Now => DateTime.Now;

		public DateTime UtcNow => DateTime.UtcNow;
	}
}
