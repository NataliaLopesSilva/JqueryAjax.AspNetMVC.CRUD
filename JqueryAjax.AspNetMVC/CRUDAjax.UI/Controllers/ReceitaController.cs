using CRUDAjax.UI;
using CRUDAjax.UI.Models.Api;
using CRUDAjax.UI.Models.Model;
using CRUDAjax.UI.Models.Negocio;
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
            //Consulta as receitas existentes no arquivo json

            return View();
        }

        [HttpGet]
        public ActionResult VerReceita()
        {
            //Consulta as receitas existentes no arquivo json

            return View();
        }


        [HttpPost]
        public ActionResult InserirReceita()
        {
            return View();
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

        //Informamos no Atributo HTTPPOST que será uma requisição deste tipo
        [HttpPost]
        public void Cadastrar(PessoaModel pessoa)
        {
            try
            {
                new PessoaNeg().Cadastrar(pessoa);

            }
            catch (Exception)
            {

                throw;
            }
        }
        //por padrão será HttpGet, entao nao precisamos informar no atributo
        public ActionResult Editar(int id)
        {
            try
            {
                var pessoa = new PessoaNeg().GetById(id);
                //para retornar ao ajax, temos que enviar nosso objeto em formato JSON, e LIBERA-LO
                //Para a requisicao GET
                return Json(pessoa, JsonRequestBehavior.AllowGet);


            }
            catch (Exception)
            {

                throw;
            }

        }
        [HttpPost]
        public void Atualizar(PessoaModel pessoa)
        {
            try
            {
                new PessoaNeg().Atualizar(pessoa);

            }
            catch (Exception)
            {

                throw;
            }

        }
        public void Deletar(int id)
        {
            try
            {
                new PessoaNeg().Deletar(id);
            }
            catch (Exception)
            {

                throw;
            }

        }
        public ActionResult Listar()
        {
            try
            {
                var listPessoas = new PessoaNeg().Listar();
                //para retornar ao ajax, temos que enviar nosso objeto em formato JSON, e LIBERA-LO
                //Para a requisicao GET
                return Json(listPessoas, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}