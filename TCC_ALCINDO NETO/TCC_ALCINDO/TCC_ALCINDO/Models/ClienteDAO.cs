using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace TCC_ALCINDO.Models
{
    public class ClienteDAO : IDAO<Cliente>
    {
        public void Salvar(Cliente obj)
        {
            if (obj.Id == 0)
                Insert(obj);
            else
                Update(obj);
        }

        public Cliente Autenticar(LoginModel model)
        {
            var conex = new BDconnection();
            string sp = "spLoginCliente";

            var parametros = new List<SqlParameter>
            {
                new SqlParameter("@login", model.UserName),
                new SqlParameter("@senha", model.Password),
            };
            var dt = conex.ExecutaSpDataTable(sp, parametros);

            if (dt.Rows.Count == 0)
                return null;


            Cliente obj = new Cliente()
            {
                Id = Convert.ToInt32(dt.Rows[0]["Id"]),
                Nome = dt.Rows[0]["Nome"].ToString(),
                Email = dt.Rows[0]["Email"].ToString(),
            };

            return obj;
        }

        public void Insert(Cliente obj)
        {
            var conex = new BDconnection();
            string sp = "spInsertCliente";

            //Criando lista de parâmetros e inserindo um a um
            var parametros = new List<SqlParameter>
            {
                new SqlParameter("@login", obj.Login),
                new SqlParameter("@senha", obj.Senha),
                new SqlParameter("@nome", obj.Nome),
                new SqlParameter("@endereco", obj.Endereco),
                new SqlParameter("@telefone", obj.Telefone),
                new SqlParameter("@email", obj.Email),
                new SqlParameter("@cpf", obj.CPF),
                new SqlParameter("@genero", obj.Genero)
            };
            obj.Id = (int)conex.ExecutaScalarSP(sp, parametros);
        }

        public void Delete(Cliente obj)
        {
            var conex = new BDconnection();
            string sp = "spDeleteCliente";

            var parametros = new List<SqlParameter>
            {
                new SqlParameter("@id", obj.Id)
            };
            conex.ExecutaNonQuerySP(sp, parametros);
        }

        public void Update(Cliente obj)
        {
            var conex = new BDconnection();
            string sp = "spUpdateCliente";

            //Criando lista de parâmetros e inserindo um a um
            var parametros = new List<SqlParameter>
            {
                new SqlParameter("@id", obj.Id),
                new SqlParameter("@login", obj.Login),
                new SqlParameter("@senha", obj.Senha),
                new SqlParameter("@nome", obj.Nome),
                new SqlParameter("@endereco", obj.Endereco),
                new SqlParameter("@telefone", obj.Telefone),
                new SqlParameter("@email", obj.Email),
                new SqlParameter("@cpf", obj.CPF),
                new SqlParameter("@genero", obj.Genero)
            };
            conex.ExecutaNonQuerySP(sp, parametros);
        }

        public Cliente Buscar(int id)
        {
            var conex = new BDconnection();
            string sp = "spBuscaCliente";

            var parametros = new List<SqlParameter>
            {
                new SqlParameter("@id", id)
            };
            var dt = conex.ExecutaSpDataTable(sp, parametros);

            if (dt.Rows.Count < 1)
                throw new Exception("Cliente não encontrado");

            var cliente = new Cliente
            (
                dt.Rows[0]["Nome"].ToString(),
                dt.Rows[0]["CPF"].ToString()
            )
            {
                Id = Convert.ToInt32(dt.Rows[0]["Id"]),
                Telefone = dt.Rows[0]["Telefone"].ToString(),
                Endereco = dt.Rows[0]["Endereco"].ToString(),
                DataCadastro = Convert.ToDateTime(dt.Rows[0]["DataCadastro"]),
                Email = dt.Rows[0]["Email"].ToString(),
                Genero = Convert.ToChar(dt.Rows[0]["Genero"]),
                Login = dt.Rows[0]["Login"].ToString(),
                Senha = "*"
            };
            return cliente;
        }

        public List<Cliente> Listar()
        {
            var conex = new BDconnection();
            string sp = "spListaCliente";

            var dt = conex.ExecutaSpDataTable(sp);
            var clientes = new List<Cliente>();

            for (var i = 0; i < dt.Rows.Count; i++)
            {
                var cliente = new Cliente
                    (
                       dt.Rows[i]["Nome"].ToString(),
                       dt.Rows[i]["CPF"].ToString()
                    );


                if (dt.Rows.Count < 1)
                    throw new Exception("Cliente não encontrado");



                cliente.Id = Convert.ToInt32(dt.Rows[i]["Id"]);
                cliente.Telefone = dt.Rows[i]["Telefone"].ToString();
                cliente.Endereco = dt.Rows[i]["Endereco"].ToString();
                cliente.DataCadastro = Convert.ToDateTime(dt.Rows[i]["DataCadastro"]);
                cliente.Email = dt.Rows[i]["Email"].ToString();
                cliente.Genero = Convert.ToChar(dt.Rows[i]["Genero"]);
                cliente.Login = dt.Rows[i]["Login"].ToString();
                cliente.Senha = dt.Rows[i]["Senha"].ToString();
                clientes.Add(cliente);
            }
            return clientes;
        }

        public List<Cliente> Listar(string pesquisa)
        {
            var conex = new BDconnection();
            string sp = "spListaCliente";

            var parametros = new List<SqlParameter>
            {
                new SqlParameter("@pesquisa", pesquisa)
            };
            var dt = conex.ExecutaSpDataTable(sp, parametros);
            var clientes = new List<Cliente>();

            for (var i = 0; i < dt.Rows.Count; i++)
            {
                var cliente = new Cliente
                    (
                       dt.Rows[i]["Nome"].ToString(),
                       dt.Rows[i]["CPF"].ToString()
                    );

                if (dt.Rows.Count < 1)
                    throw new Exception("Cliente não encontrado");

                cliente.Id = Convert.ToInt32(dt.Rows[i]["Id"]);
                cliente.Telefone = dt.Rows[i]["Telefone"].ToString();
                cliente.Endereco = dt.Rows[i]["Endereco"].ToString();
                cliente.DataCadastro = Convert.ToDateTime(dt.Rows[i]["DataCadastro"]);
                cliente.Email = dt.Rows[i]["Email"].ToString();
                cliente.Genero = Convert.ToChar(dt.Rows[i]["Genero"]);
                cliente.Login = dt.Rows[i]["Login"].ToString();
                cliente.Senha = "*";
                clientes.Add(cliente);
            }
            return clientes;
        }


        private List<Cliente> ConvertTabela(DataTable tabela)
        {
            List<Cliente> list = new List<Cliente>();

            foreach (DataRow linha in tabela.Rows)
            {
                list.Add(new Cliente()
                {
                    Id = Convert.ToInt32(linha["Id"]),
                    Nome = linha["Nome"].ToString(),
                    Email = linha["Email"].ToString(),
                    DataCadastro = DateTime.Parse(linha["DataCadastro"].ToString()),
                    CPF = linha["CPF"].ToString(),
                    Genero = linha["Genero"].ToString()[0],
                    Telefone = linha["Telefone"].ToString()

                    //Terminar 

                });

            }
            return list;
        }
    }
}