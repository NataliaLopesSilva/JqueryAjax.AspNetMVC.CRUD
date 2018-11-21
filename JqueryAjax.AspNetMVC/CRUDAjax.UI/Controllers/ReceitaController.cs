using CRUDAjax.UI;
using CRUDAjax.UI.Models.Api;
using CRUDAjax.UI.Models.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;


namespace CRUDAjax.Controllers
{
    public class ReceitaController : Controller
    {
        [HttpGet]
        public ActionResult ReceitaPrincipal()
        {
            ReceitaModel obj = new ReceitaModel();
            obj.listaReceitas = obj.consultaTodasReceita();

            return View(obj);
        }

        [HttpGet]
        public ActionResult VerReceita()
        {
            //Consulta as receitas existentes no arquivo json
            ReceitaModel receita = new ReceitaModel();
            receita.tituloReceita = "BRIGADEIRO";

            return View(receita);
        }


        [HttpPost]
        public ActionResult InserirReceita(ReceitaModel receita)
        {
            string msg = receita.validaInserirReceita(receita);
            return Json(msg, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult TraduzReceita(ReceitaModel receita)
        {
            try
            {
                Translate translate = new Translate();
                ReceitaModel receitaIngles = new ReceitaModel();
                receitaIngles.listaIngrediente = new List<IngredienteModel>();

                receitaIngles.tituloReceita = translate.Traduzir(receita.tituloReceita);
                receitaIngles.modoPreparo = translate.Traduzir(receita.modoPreparo);

                foreach (var item in receita.listaIngrediente)
                {
                    IngredienteModel ingrediente = new IngredienteModel();

                    ingrediente.nomeIngrediente = translate.Traduzir(item.nomeIngrediente);
                    ingrediente.qtda = translate.Traduzir(item.qtda);

                    receitaIngles.listaIngrediente.Add(ingrediente);
                }

                var resultado = new
                {
                    receitaTraduzida = receitaIngles,
                    msg = "OK"
                };

                return Json(resultado, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json("ERRO", JsonRequestBehavior.AllowGet);
            }
        }
    }
}