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
        /// Дата заказа
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Статус работы
        /// </summary>
        public string Status { get; set; }
    }
}
