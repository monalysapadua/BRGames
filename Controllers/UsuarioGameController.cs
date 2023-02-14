using BRGames.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace BRGames.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioGameController : ControllerBase
    {
        readonly string connectionString = "Data Source = DESKTOP-KV2D6G6\\SQLEXPRESS;Integrated Security=true;Initial Catalog=BRGamersSquare";

        /// <summary>
        /// Cadastrar Usuário da Aplicação
        /// </summary>
        /// <param name="usuario">Dados do Usuário Cadastrado Para o Jogo</param>
        /// <returns>Dados do Usuário Cadastrado</returns>
        [HttpPost]
        public IActionResult Cadastrar(UsuarioGame usuario)
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(connectionString))
                {
                    conexao.Open();

                    string script = "INSERT INTO UsuariosGame(Nome, UserName, Senha, Email, Idade) VALUES (@Nome, @UserName, @Senha, @Email, @Idade )";

                    using (SqlCommand cmd = new SqlCommand(script, conexao))
                    {
                        cmd.Parameters.Add("@Nome", SqlDbType.NVarChar).Value = usuario.Nome;
                        cmd.Parameters.Add("@UserName", SqlDbType.NVarChar).Value = usuario.UserName;
                        cmd.Parameters.Add("@Senha", SqlDbType.NVarChar).Value = usuario.Senha;
                        cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = usuario.Email;
                        cmd.Parameters.Add("@Idade", SqlDbType.NVarChar).Value = usuario.Idade;

                        cmd.CommandType = CommandType.Text;
                        cmd.ExecuteNonQuery();
                    }
                }

                return Ok(usuario);
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
        /// <returns>Dados dos usuários da aplicação</returns>
        [HttpGet]
        public IActionResult Listar()
        {
            try
            {
                var usuarios = new List<UsuarioGame>();

                using (SqlConnection conexao = new SqlConnection(connectionString))
                {
                    conexao.Open();

                    string consulta = "SELECT * FROM UsuariosGame";

                    using (SqlCommand cmd = new SqlCommand(consulta, conexao))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                usuarios.Add(new UsuarioGame
                                {
                                    UsuarioGameID = (int)reader[0],
                                    Nome = (string)reader[1],
                                    UserName = (string)reader[2],
                                    Senha = (string)reader[3],
                                    Email = (string)reader[4],
                                    Idade = (int)reader[5],
                                });
                            }
                        }
                    }
                }
                return Ok(usuarios);
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
        /// Altera os dados de um usuário.
        /// </summary>
        /// <param name="id">Id do Usuário</param>
        /// <param name="usuario">Todas as informações do usuário</param>
        /// <returns>usuário alterado</returns>
        [HttpPut("/{id}")]
        public IActionResult Alterar(int id, UsuarioGame usuario)
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(connectionString))
                {
                    conexao.Open();

                    string script = "UPDATE UsuariosGame SET nome =@Nome, userName=@userName, Senha=@Senha, Email=@Email, Idade=@Idade WHERE UsuarioGameID=@id";

                    using (SqlCommand cmd = new SqlCommand(script, conexao))
                    {
                        cmd.Parameters.Add("@id", SqlDbType.NVarChar).Value = id;
                        cmd.Parameters.Add("@Nome", SqlDbType.NVarChar).Value = usuario.Nome;
                        cmd.Parameters.Add("@userName", SqlDbType.NVarChar).Value = usuario.UserName;
                        cmd.Parameters.Add("@Senha", SqlDbType.NVarChar).Value = usuario.Senha;
                        cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = usuario.Email;
                        cmd.Parameters.Add("@Idade", SqlDbType.NVarChar).Value = usuario.Idade;

                        cmd.CommandType = CommandType.Text;
                        cmd.ExecuteNonQuery();
                        usuario.UsuarioGameID = id;
                    }
                }
                return Ok(usuario);
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

