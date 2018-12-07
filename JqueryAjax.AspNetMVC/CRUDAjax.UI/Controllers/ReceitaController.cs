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

            foreach (var item in obj.listaReceitas)
            {
                if (item.imagem.Count() == 0)
                {
                    string path = Server.MapPath("~/Images/nophotoII.jpg");
                    byte[] imageByteData = System.IO.File.ReadAllBytes(path);
                    string imageBase64Data = Convert.ToBase64String(imageByteData);
                    item.imagemArrayBytes = string.Format("data:image/png;base64,{0}", imageBase64Data);
                }
            }

            return View(obj);
        }

        [HttpGet]
        public ActionResult VerReceita(int idReceita)
        {
            try
            {
                ReceitaModel receita = new ReceitaModel();
                IngredienteModel ingrediente = new IngredienteModel();

                receita = receita.consultaReceitaPorId(idReceita);

                receita.listaIngrediente = new List<IngredienteModel>();
                receita.listaIngrediente = ingrediente.consultaIngrendientePorIdReceita(receita.idReceita);

                if (receita.imagem.Count() == 0)
                {
                    string path = Server.MapPath("~/Images/nophotoII.jpg");
                    byte[] imageByteData = System.IO.File.ReadAllBytes(path);
                    string imageBase64Data = Convert.ToBase64String(imageByteData);
                    receita.imagemArrayBytes = string.Format("data:image/png;base64,{0}", imageBase64Data);
                }

                return View(receita);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public ActionResult InserirReceita(ReceitaModel receita)
        {
            try
            {
                if (receita.imagemArrayBytes != null)
                {
                    string[] byteStrings = receita.imagemArrayBytes.Split(',');
                    receita.imagem = new byte[byteStrings.Length];

                    for (int i = 0; i < byteStrings.Length; i++)
                    {
                        receita.imagem[i] = Convert.ToByte(byteStrings[i]);
                    }
                }
                else
                {
                    receita.imagem = new byte[0];
                }

                string msg = receita.validaInserirReceita(receita);
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult ExcluirReceita(int idReceita)
        {
            try
            {
                ReceitaModel receita = new ReceitaModel();

                string msg = receita.validaExcluirReceita(idReceita);
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
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