namespace MetersSender.Neodom.Models.Configuration
{
    /// <summary>
    ///     Модель настроек.
    /// </summary>
    internal class NeodomConfig
    {
        /// <summary>
        ///     Логин (электронная почта).
        /// </summary>
        public string Login { get; set; }

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
