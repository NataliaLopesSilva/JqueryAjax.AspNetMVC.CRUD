using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRUDAjax.UI.Models.Model
{
    public class ReceitaModel
    {
        public string tituloReceita { get; set; }

        public string modoPreparo { get; set; }

        public string porcao { get; set; }

        public List<IngredienteModel> listaIngrediente { get; set; }

        public InformacaoNutricionalModel informacaoNutricional { get; set; }

    }
}