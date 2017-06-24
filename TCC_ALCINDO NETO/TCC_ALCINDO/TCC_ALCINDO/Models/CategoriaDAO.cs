using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace TCC_ALCINDO.Models
{
    public class CategoriaDAO : IDAO<Categoria> 
    {
        public List<Categoria> Listar()
        {
            throw new NotImplementedException();
        }
        public Categoria Buscar(int id)
        {
            throw new NotImplementedException();
        }

        public void Delete(Categoria obj)
        {
            throw new NotImplementedException();
        }

        public void Insert(Categoria obj)
        {
            throw new NotImplementedException();
        }

        public List<Categoria> Listar(Categoria categoria)
        {
            var conex = new BDconnection();
            string sp = "spListaCategoria";

            var parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@idCategoria", categoria.Id));

            var dt = conex.ExecutaSpDataTable(sp, parametros);

            var categorias = new List<Categoria>();
            for (var i = 0; i < dt.Rows.Count; i++)
            {
                var categoriaa = new Categoria();


                if (dt.Rows.Count < 1)
                    throw new Exception("Categoria não encontrada");

                categoriaa.Id = Convert.ToInt32(dt.Rows[i]["id"]);
                categoriaa.Nome = dt.Rows[i]["nome"].ToString();

                categorias.Add(categoriaa);
            }
            return categorias;
        }

        public List<Categoria> Listar(string pesquisa)
        {
            throw new NotImplementedException();
        }

        public void Salvar(Categoria obj)
        {
            throw new NotImplementedException();
        }

        public void Update(Categoria obj)
        {
            throw new NotImplementedException();
        }
    }
}