namespace MetersSender.Saures.Models
{
    /// <summary>
    ///     Модель настроек.
    /// </summary>
    internal class SauresConfig
    {
        /// <summary>
        ///     Электронная почта.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        ///     Пароль.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        ///     Ссылка на API.
        /// </summary>
        public string ApiUrl { get; set; }
    }
}
