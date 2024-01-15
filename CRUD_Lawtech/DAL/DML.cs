using CRUD_Lawtech.Types;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Configuration;

namespace CRUD_Lawtech.DAL
{
    public class DML
    {
        private  string connectionString;
        
        public DML(string connectionString)
        {
            this.connectionString = ConfigurationManager.ConnectionStrings["CRUD_Lawtech.Properties.Settings.LawtechConnectionString"].ConnectionString;
        }
        public static string FiltrarPorCategoria(string categoria, string filtroTexto)
        {
            string whereConsulta;
            switch (categoria)
            {
                case "Selecione um filtro":
                    whereConsulta = $"nomeCompleto LIKE '%{filtroTexto}%' OR email LIKE '%{filtroTexto}%' OR CONVERT(dataCadastro, 'System.String') LIKE '%{filtroTexto}%'";
                    break;
                case "Nome":
                    whereConsulta = $"nomeCompleto LIKE '%{filtroTexto}%'";
                    break;
                case "E-mail":
                    whereConsulta = $"email LIKE '%{filtroTexto}%'";
                    break;
                case "Data de Cadastro":
                    whereConsulta = $"CONVERT(dataCadastro, 'System.String') LIKE '%{filtroTexto}%'";
                    break;
                default:
                    whereConsulta = "";
                    break;
            }
            return whereConsulta;
        }
        public List<Contato> ObterTodos()
        {
            using (var db = new SqlConnection(connectionString))
            {
                return db.Query<Contato>("SELECT * FROM contatos").ToList();
            }
        }

        public Contato ObterPorId(int id)
        {
            using (var db = new SqlConnection(connectionString))
            {
                return db.QueryFirstOrDefault<Contato>("SELECT * FROM contatos WHERE id = @Id", new { Id = id });
            }
        }

        public void Inserir(Contato contato)
        {
            using (var db = new SqlConnection(connectionString))
            {
                var query = "INSERT INTO contatos (nomeCompleto, cpf, email,dataCadastro,dataAlteracao) VALUES (@NomeCompleto, @Cpf,@Email,GETDATE(),NULL )";
                db.Execute(query, contato);
            }
        }

        public void Atualizar(Contato usuario)
        {
            using (var db = new SqlConnection(connectionString))
            {
                var query = "UPDATE contatos SET nomeCompleto = @NomeCompleto, cpf = @Cpf , email = @Email, dataCadastro = @DataCadastro, dataAlteracao = GETDATE() WHERE Id = @Id";
                db.Execute(query, usuario);
            }
        }

        public void Excluir(int id)
        {
            using (var db = new SqlConnection(connectionString))
            {
                var query = "DELETE FROM contatos WHERE id = @Id";
                db.Execute(query, new { Id = id });
            }
        }
    }
    
}
