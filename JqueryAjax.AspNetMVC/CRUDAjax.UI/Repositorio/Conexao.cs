using System.Data.SqlClient;

namespace CRUDAjax.UI.Repositorio
{
    public class Conexao
    {
        SqlConnection sqlConnection = new SqlConnection();

        public Conexao()
        {
            //Endereço do banco de dados
            sqlConnection.ConnectionString = "Data Source=DESKTOP-PCKEML3;Initial Catalog=MyRecipe;Integrated Security=True";
        }

        //Método para conectar com o banco de dados
        public SqlConnection conectarBanco()
        {
            //Verifica se a conexão já existe, se não existir então inicia a conexão com o banco
            if (sqlConnection.State == System.Data.ConnectionState.Closed)
            {
                sqlConnection.Open();
            }

            return sqlConnection;
        }

        //Método para desconectar com o banco de dados
        public void desconectarBanco()
        {
            //Verifica se a conexão já está fechada, se não estiver fechada então ele fecha a conexão
            if (sqlConnection.State == System.Data.ConnectionState.Open)
            {
                sqlConnection.Close();
            }
        }
    }
}