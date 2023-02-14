using BRGames.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Data.SqlClient;

namespace BRGames.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        readonly string connectionString = "Data Source = DESKTOP-KV2D6G6\\SQLEXPRESS;Integrated Security=true;Initial Catalog=BRGamersSquare";
        
        /// <summary>
        /// Cadastra nomes dos Jogos
        /// </summary>
        /// <param name="categoria">Dados dos Jogos</param>
        /// <returns>Dados do Jogo cadastrado</returns>
        [HttpPost]
        public IActionResult Cadastrar(Categoria categoria)
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(connectionString))
                {
                    conexao.Open();

                    string script = "INSERT INTO Categoria(NomeDoJogo) VALUES (@NomeDoJogo)";

                    using (SqlCommand cmd = new SqlCommand(script, conexao))
                    {
                        cmd.Parameters.Add("@NomeDoJogo", SqlDbType.NVarChar).Value = categoria.NomeDoJogo;

                        cmd.CommandType = CommandType.Text;
                        cmd.ExecuteNonQuery();
                    }
                }
                
                return Ok(categoria);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new
                {
                    msg = "Falha na conexão",
                    erro = ex.Message,
                });
            }

        }
        /// <summary>
        /// Lista todos os usuários da Aplicação
        /// </summary>
        /// <returns>Lista de Usuários</returns>
        [HttpGet]
        public IActionResult Listar()
        {
            try
            {
                var categorias=new List<Categoria>();

                using (SqlConnection conexao = new SqlConnection(connectionString))
                {
                    conexao.Open();
                    
                    string consulta = "SELECT * FROM Categoria";

                    using (SqlCommand cmd = new SqlCommand(consulta, conexao))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while(reader.Read())
                            {
                                categorias.Add(new Categoria {
                                    CategoriaJogoID = (int)reader[0],
                                    NomeDoJogo = (string)reader[1]
                                });
                            }
                        }
                    }
                }
                return Ok(categorias);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new
                {
                    msg = "Falha na conexão",
                    erro = ex.Message,
                });
            }
        }
        /// <summary>
        /// Altera os dados da categoria
        /// </summary>
        /// <param name="Id">ID da categoria</param>
        /// <param name="categoria">Todas as informações da categoria do Jogo</param>
        /// <returns>Categoria alterada</returns>
        [HttpPut("/{Id}")]
        public IActionResult AlterarCategoria(int Id, Categoria categoria)
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(connectionString))
                {
                    conexao.Open();

                    string script = "UPDATE Categoria SET NomeDoJogo =@NomeDoJogo WHERE NomeDoJogoID =@Id";

                    using (SqlCommand cmd = new SqlCommand(script, conexao))
                    {
                        cmd.Parameters.Add("@Id", SqlDbType.NVarChar).Value = Id; 
                        cmd.Parameters.Add("@NomeDoJogo", SqlDbType.NVarChar).Value = categoria.NomeDoJogo;

                        cmd.CommandType = CommandType.Text;
                        cmd.ExecuteNonQuery();
                        categoria.CategoriaJogoID = Id;
                    }
                }
                return Ok(categoria);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new
                {
                    msg = "Falha na conexão",
                    erro = ex.Message,
                });
            }
        }
    }
}
