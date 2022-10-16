using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Models.Sklad
{
    public class RashodyQueryParams
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        /// <summary>
        /// Флаг какая дата учитвается в сортировке
        /// 1 - дата расходника
        /// 2 - дата отгрузки
        /// 3 - дата оплаты счета
        /// </summary>
        public int? DateFilter { get; set; }
        public string Search { get; set; }
        //public Tovary SearchTovar { get; set; }

        public RashodyQueryParams(DateTime? startDate, DateTime? endDate, string search, int dateFilter = 1)
        {
            this.StartDate = startDate;
            this.EndDate = endDate;
            this.Search = search;
            this.DateFilter = dateFilter;
        }
    }
}
