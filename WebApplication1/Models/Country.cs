using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Country : IParentModel
    {
        /*
        (Id - идентификатор, Название – строка, Код страны – строка, 
            Столица – идентификатор с таблицы Города, площадь – дробное число, 
            Население – целое число, Регион – идентификатор с таблицы Регионы
            
        Попробовать через ангулар принять данные
             */

        public int Id { get; set; }
        public string Name { get; set; }
        public string StateCode { get; set; }
        public double Area { get; set; }
        public int Population { get; set; }

        public virtual City City { get; set; }
        public virtual int CityId { get; set; }

        public virtual Region Region { get; set; }

    }
}
