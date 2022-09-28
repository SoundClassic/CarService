using System;

namespace CarService
{
    /// <summary>
    /// Заявка
    /// </summary>
    public struct Bid
    {
        /// <summary>
        /// Номер заявки
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Имя Фамилия Отчество
        /// </summary>
        public string LFP { get; set; }

        /// <summary>
        /// Марка автомобиля
        /// </summary>
        public Brands Brand { get; set; }
        
        /// <summary>
        /// Тип работы
        /// </summary>
        public TypeWorks TypeWork { get; set; }

        /// <summary>
        /// Дата приема заказа
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Стоимость ремонта
        /// </summary>
        public decimal Cost { get; set; }

        /// <summary>
        /// Статус работы
        /// </summary>
        public Statuses Status { get; set; }

        /// <summary>
        /// Успех работы
        /// </summary>
        public bool Succes { get; set; }

        /// <summary>
        /// Комментарий по заявке
        /// </summary>
        public string Comment { get; set; }
    }
}
