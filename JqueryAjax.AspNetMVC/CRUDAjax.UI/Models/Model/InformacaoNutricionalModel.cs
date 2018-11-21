using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRUDAjax.UI.Models.Model
{
    public class InformacaoNutricionalModel
    {
        public decimal calorias { get; set; }

        public decimal valorEnergetico { get; set; }

        public decimal carboidratos { get; set; }

        public decimal gordurasTotais { get; set; }

        public decimal gordurasSaturadas { get; set; }

        public decimal sodio { get; set; }
    }
}