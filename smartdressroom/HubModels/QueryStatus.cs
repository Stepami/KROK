namespace smartdressroom.HubModels
{
    /// <summary>
    /// Тип статуса запроса
    /// </summary>
    public enum QueryStatus : byte
    {
        /// <summary>
        /// Запрос закрыт
        /// </summary>
        CLOSED = 0,
        /// <summary>
        /// Запрос может быть
        /// любым консультантом
        /// </summary>
        FREE = 1,
        /// <summary>
        /// Запрос может быть
        /// взят ответственным
        /// по комнате консультантом
        /// </summary>
        FREE_BUSY = 2,
        /// <summary>
        /// Запрос выполняется консультантом
        /// </summary>
        BUSY = 3
    }
}