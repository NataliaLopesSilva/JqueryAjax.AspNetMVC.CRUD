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

        public byte[] imagem { get; set; }

        public string nomeImagem { get; set; }

        public string imagemArrayBytes { get; set; }

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
                receita.tituloReceita = Convert.ToString(linha["tituloReceita"]);
                receita.modoPreparo = Convert.ToString(linha["modoPreparo"]);
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
                receita.informacaoNutricional = new InformacaoNutricionalModel();

                receita.idReceita = Convert.ToInt32(linha["idReceita"]);
                receita.tituloReceita = Convert.ToString(linha["tituloReceita"]);
                receita.modoPreparo = Convert.ToString(linha["modoPreparo"]);

                if (!linha["imagem"].ToString().Equals(""))
                {
                    receita.imagem = (byte[])linha["imagem"];

                    //Converte imagem para base 64
                    string imageBase64Data = Convert.ToBase64String(receita.imagem);
                    receita.imagemArrayBytes = string.Format("data:image/png;base64,{0}", imageBase64Data);
                }

                receita.informacaoNutricional.calorias = Convert.ToDecimal(linha["calorias"]);

                lista.Add(receita);
            }

            return lista;
        }

        public ReceitaModel consultaReceitaPorId(int idReceita)
        {
            ReceitaModel receita = new ReceitaModel();
            SqlDataAdapter sqlConsulta = new SqlDataAdapter("SELECT * FROM Receita r where r.idReceita = @idReceita", conexao.conectarBanco());
            DataTable dt = new DataTable();

            sqlConsulta.SelectCommand.Parameters.AddWithValue("@idReceita", idReceita);

            sqlConsulta.GetFillParameters();
            sqlConsulta.Fill(dt);

            receita.informacaoNutricional = new InformacaoNutricionalModel();

            foreach (DataRow linha in dt.Rows)
            {

                receita.idReceita = Convert.ToInt32(linha["idReceita"]);
                receita.tituloReceita = Convert.ToString(linha["tituloReceita"]);
                receita.modoPreparo = Convert.ToString(linha["modoPreparo"]);

                if (!linha["imagem"].ToString().Equals(""))
                {
                    receita.imagem = (byte[])linha["imagem"];

                    //Converte imagem para base 64
                    string imageBase64Data = Convert.ToBase64String(receita.imagem);
                    receita.imagemArrayBytes = string.Format("data:image/png;base64,{0}", imageBase64Data);
                }

                receita.informacaoNutricional.calorias = Convert.ToInt32(linha["calorias"]);
                receita.informacaoNutricional.carboidratos = Convert.ToDecimal(linha["carboidratos"]);
                receita.informacaoNutricional.gordurasTotais = Convert.ToDecimal(linha["gordurasTotais"]);
                receita.informacaoNutricional.gordurasSaturadas = Convert.ToDecimal(linha["gordurasSaturadas"]);
                receita.informacaoNutricional.fibra = Convert.ToDecimal(linha["fibra"]);
                receita.informacaoNutricional.sodio = Convert.ToDecimal(linha["sodio"]);
                receita.informacaoNutricional.acucar = Convert.ToDecimal(linha["acucar"]);
            }

            return receita;
        }

        public ReceitaModel consultaCaloriaReceita(string tituloReceita)
        {
            ReceitaModel receita = new ReceitaModel();
            SqlDataAdapter sqlConsulta = new SqlDataAdapter("SELECT * FROM Receita r where r.tituloReceita = @tituloReceita", conexao.conectarBanco());
            DataTable dt = new DataTable();

            sqlConsulta.SelectCommand.Parameters.AddWithValue("@tituloReceita", tituloReceita);

            sqlConsulta.GetFillParameters();
            sqlConsulta.Fill(dt);

            receita.informacaoNutricional = new InformacaoNutricionalModel();

            foreach (DataRow linha in dt.Rows)
            {
                receita.informacaoNutricional.calorias = Convert.ToInt32(linha["calorias"]);
            }

            return receita;
        }

        //Métodos de CRUD -----------------------------------------------------------------

        public string validaInserirReceita(ReceitaModel obj)
        {
            string msg = "OK";

            //Comando a ser executado
            cmd.CommandText = "insert into Receita (tituloReceita, modoPreparo, imagem, calorias, carboidratos, gordurasTotais, gordurasSaturadas, fibra, sodio, acucar, proteina)" +
                " values (@tituloReceita, @modoPreparo, @imagem, @calorias, @carboidratos, @gordurasTotais, @gordurasSaturadas, @fibra, @sodio, @acucar, @proteina)";

            //Leitura dos parâmetros
            cmd.Parameters.AddWithValue("@tituloReceita", obj.tituloReceita);
            cmd.Parameters.AddWithValue("@modoPreparo", obj.modoPreparo);
            cmd.Parameters.AddWithValue("@imagem", obj.imagem);
            cmd.Parameters.AddWithValue("@calorias", obj.informacaoNutricional.calorias);
            cmd.Parameters.AddWithValue("@carboidratos", obj.informacaoNutricional.carboidratos);
            cmd.Parameters.AddWithValue("@gordurasTotais", obj.informacaoNutricional.gordurasTotais);
            cmd.Parameters.AddWithValue("@gordurasSaturadas", obj.informacaoNutricional.gordurasSaturadas);
            cmd.Parameters.AddWithValue("@fibra", obj.informacaoNutricional.fibra);
            cmd.Parameters.AddWithValue("@sodio", obj.informacaoNutricional.sodio);
            cmd.Parameters.AddWithValue("@acucar", obj.informacaoNutricional.acucar);
            cmd.Parameters.AddWithValue("@proteina", obj.informacaoNutricional.proteina);

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

