using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace TCC_ALCINDO.Models
{
    public class ProdutoDAO : IDAO<Produto>
    {
        public void Salvar(Produto obj)
        {
            if (obj.Id == 0)
                Insert(obj);
            else
                Update(obj);
        }

        public void Insert(Produto obj)
        {
            var conex = new BDconnection();
            string sp = "spInsertProduto";

            //Criando lista de parâmetros e inserindo um a um
            var parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@nome", obj.Nome));
            parametros.Add(new SqlParameter("@marca", obj.Marca));
            parametros.Add(new SqlParameter("@preco", obj.Preco));

            obj.Id = (int)conex.ExecutaScalarSP(sp, parametros);
        }

        public void Delete(Produto obj)
        {
            var conex = new BDconnection();
            string sp = "spDeleteProduto";

            var parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@id", obj.Id));

            conex.ExecutaNonQuerySP(sp, parametros);
        }

        public void Update(Produto obj)
        {
            var conex = new BDconnection();
            string sp = "spUpdateProduto";

            //Criando lista de parâmetros e inserindo um a um
            var parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@id", obj.Id));
            parametros.Add(new SqlParameter("@nome", obj.Nome));
            parametros.Add(new SqlParameter("@marca", obj.Marca));
            parametros.Add(new SqlParameter("@preco", obj.Preco));

            conex.ExecutaNonQuerySP(sp, parametros);
        }

        public Produto Buscar(int id)
        {
            var conex = new BDconnection();
            string sp = "spBuscaProduto";

            var parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@id", id));

            var dt = conex.ExecutaSpDataTable(sp, parametros);

            if (dt.Rows.Count < 1)
                throw new Exception("Produto não encontrado");

            var produto = new Produto();

            produto.Id = Convert.ToInt32(dt.Rows[0]["id"]);
            produto.Nome = dt.Rows[0]["nome"].ToString();
            produto.Marca = dt.Rows[0]["marca"].ToString();
            produto.Preco = Convert.ToDecimal(dt.Rows[0]["preco"]);
            produto.DataCadastro = Convert.ToDateTime(dt.Rows[0]["dataCadastro"]);
            return produto;
        }

        public List<Produto> Listar()
        {
            var conex = new BDconnection();
            string sp = "spListaProduto";

            var dt = conex.ExecutaSpDataTable(sp);
            var produtos = new List<Produto>();

            for (var i = 0; i < dt.Rows.Count; i++)
            {
                var produto = new Produto();

                if (dt.Rows.Count < 1)
                    throw new Exception("Produto não encontrado");

                produto.Id = Convert.ToInt32(dt.Rows[i]["id"]);
                produto.Nome = dt.Rows[i]["nome"].ToString();
                produto.Marca = dt.Rows[i]["marca"].ToString();
                produto.Preco = Convert.ToDecimal(dt.Rows[i]["preco"]);
                produto.DataCadastro = Convert.ToDateTime(dt.Rows[i]["dataCadastro"]);
                produtos.Add(produto);
            }
            return produtos;
        }

        public List<Produto> Listar(string pesquisa)
        {
            var conex = new BDconnection();
            string sp = "spListaProduto";

            var parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@pesquisa", pesquisa));

            var dt = conex.ExecutaSpDataTable(sp, parametros);
            var produtos = new List<Produto>();

            for (var i = 0; i < dt.Rows.Count; i++)
            {
                var produto = new Produto();


                if (dt.Rows.Count < 1)
                    throw new Exception("Produto não encontrado");



                produto.Id = Convert.ToInt32(dt.Rows[i]["id"]);
                produto.Nome = dt.Rows[i]["nome"].ToString();
                produto.Marca = dt.Rows[i]["marca"].ToString();
                produto.Preco = Convert.ToDecimal(dt.Rows[i]["preco"]);
                produto.DataCadastro = Convert.ToDateTime(dt.Rows[i]["dataCadastro"]);
                produtos.Add(produto);
            }
            return produtos;
        }
    }
}