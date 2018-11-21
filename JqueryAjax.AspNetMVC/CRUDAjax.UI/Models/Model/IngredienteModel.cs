using CRUDAjax.UI.Repositorio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CRUDAjax.UI.Models.Model
{
    public class IngredienteModel
    {
        public int idReceita { get; set; }

        public string nomeIngrediente { get; set; }

        public string qtda { get; set; }


        // MÉTODOS ----------------------------------------------------------------

        Conexao conexao = new Conexao();
        SqlCommand cmd = new SqlCommand();

        public string validaInserirIngrediente(IngredienteModel obj)
        {
            string msg = "OK";

            //Comando a ser executado
            cmd.CommandText = "insert into Ingrediente (idReceita, nomeIngrediente, qtda) values (@idReceita, @nomeIngrediente, @qtda)";

            //Leitura dos parâmetros
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@idReceita", obj.idReceita);
            cmd.Parameters.AddWithValue("@nomeIngrediente", obj.nomeIngrediente);
            cmd.Parameters.AddWithValue("@qtda", obj.qtda);

            try
            {
                //Abre a conexão
                cmd.Connection = conexao.conectarBanco();

                //Executa o comando expecífico
                cmd.ExecuteNonQuery();

                //Fecha a conexão
                conexao.desconectarBanco();

                return msg;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}