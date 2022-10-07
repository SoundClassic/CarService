using System;

namespace CarService
{
    /// <summary>
    /// Заявка
    /// </summary>
    public struct BidDao
    {
        /// <summary>
        /// Номер заявки
        /// </summary>
        public string NumberBid { get; set; }

        /// <summary>
        /// Статус работы
        /// </summary>
        public Statuses Status { get; set; }
    }
}
