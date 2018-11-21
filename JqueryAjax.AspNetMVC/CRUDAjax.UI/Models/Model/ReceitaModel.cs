using CRUDAjax.UI.Repositorio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CRUDAjax.UI.Models.Model
{
    public class ReceitaModel
    {
        public int idReceita { get; set; }

        public string tituloReceita { get; set; }

        public string modoPreparo { get; set; }

        public string porcao { get; set; }

        public List<ReceitaModel> listaReceitas { get; set; }

        public List<IngredienteModel> listaIngrediente { get; set; }

        public InformacaoNutricionalModel informacaoNutricional { get; set; }


        //MÉTODOS

        Conexao conexao = new Conexao();
        SqlCommand cmd = new SqlCommand();

        //Métodos de consulta -------------------------------------------------------------
        public ReceitaModel consultaReceitaEspecifica(string tituloReceita)
        {
            ReceitaModel receita = new ReceitaModel();
            SqlDataAdapter sqlConsulta = new SqlDataAdapter("SELECT * FROM Receita r where r.tituloReceita = @tituloReceita", conexao.conectarBanco());
            DataTable dt = new DataTable();

            sqlConsulta.SelectCommand.Parameters.AddWithValue("@tituloReceita", tituloReceita);

            sqlConsulta.GetFillParameters();
            sqlConsulta.Fill(dt);

            foreach (DataRow linha in dt.Rows)
            {
                receita.idReceita = Convert.ToInt32(linha["idReceita"]);
            }

            return receita;
        }

        public List<ReceitaModel> consultaTodasReceita()
        {
            List<ReceitaModel> lista = new List<ReceitaModel>();
            SqlDataAdapter sqlConsulta = new SqlDataAdapter("SELECT * FROM Receita", conexao.conectarBanco());
            DataTable dt = new DataTable();

            sqlConsulta.GetFillParameters();
            sqlConsulta.Fill(dt);

            foreach (DataRow linha in dt.Rows)
            {
                ReceitaModel receita = new ReceitaModel();

                receita.idReceita = Convert.ToInt32(linha["idReceita"]);
                receita.tituloReceita = Convert.ToString(linha["tituloReceita"]);
                receita.modoPreparo = Convert.ToString(linha["modoPreparo"]);

                lista.Add(receita);
            }

            return lista;
        }


        //Métodos de CRUD -----------------------------------------------------------------

        public string validaInserirReceita(ReceitaModel obj)
        {
            string msg = "OK";

            //Comando a ser executado
            cmd.CommandText = "insert into Receita (tituloReceita, modoPreparo) values (@tituloReceita, @modoPreparo)";

            //Leitura dos parâmetros
            cmd.Parameters.AddWithValue("@tituloReceita", obj.tituloReceita);
            cmd.Parameters.AddWithValue("@modoPreparo", obj.modoPreparo);

            try
            {
                //Abre a conexão
                cmd.Connection = conexao.conectarBanco();

                //Executa o comando expecífico
                cmd.ExecuteNonQuery();

                //Fecha a conexão
                conexao.desconectarBanco();

                //--------------------------------------- Pesquisa a receita inserida para inserir seus ingredientes -------------------------------------
                ReceitaModel receita = new ReceitaModel();
                IngredienteModel ingrediente = new IngredienteModel();
                receita = consultaReceitaEspecifica(obj.tituloReceita);

                //Insere os ingredientes da receita
                foreach (var item in obj.listaIngrediente)
                {
                    item.idReceita = receita.idReceita;
                    msg = ingrediente.validaInserirIngrediente(item);
                }

                return msg;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}