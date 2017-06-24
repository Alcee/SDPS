using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace TCC_ALCINDO.Models
{
    public class AnimalDAO : IDAO<Animal>
    {
        public Animal Buscar(int id)
        {
            var conex = new BDconnection();
            string sp = "spBuscaAnimal";

            var parametros = new List<SqlParameter>
            {
                new SqlParameter("@id", id)
            };
            var dt = conex.ExecutaSpDataTable(sp, parametros);

            if (dt.Rows.Count < 1)
                throw new Exception("Animal não encontrado");

            var animal = new Animal()
            {
                Id = Convert.ToInt32(dt.Rows[0]["Id"]),
                Nome = dt.Rows[0]["Nome"].ToString(),
                Genero = dt.Rows[0]["Genero"].ToString()[0],
                DataCadastro = Convert.ToDateTime(dt.Rows[0]["DataCadastro"]),
                DataNascimento = Convert.ToDateTime(dt.Rows[0]["DataNascimento"]),
                Especie = dt.Rows[0]["Especie"].ToString(),
                Porte = dt.Rows[0]["Porte"].ToString(),
                Raca = dt.Rows[0]["Raca"].ToString(),
                Foto = (byte[])dt.Rows[0]["Foto"],
                Cliente = new Cliente { Id = Convert.ToInt32(dt.Rows[0]["IdCliente"]) }
            };
            return animal;
        }

        public void Delete(Animal obj)
        {
            var conex = new BDconnection();
            string sp = "spDeleteAnimal";

            var parametros = new List<SqlParameter>
            {
                new SqlParameter("@id", obj.Id)
            };
            conex.ExecutaNonQuerySP(sp, parametros);
        }

        public void Insert(Animal obj)
        {
            var conex = new BDconnection();
            string sp = "spInsertAnimal";

            //Criando lista de parâmetros e inserindo um a um
            var parametros = new List<SqlParameter>
            {
                new SqlParameter("@nome", obj.Nome),
                new SqlParameter("@genero", obj.Genero),
                new SqlParameter("@dataNascimento", obj.DataNascimento),
                new SqlParameter("@especie", obj.Especie),
                new SqlParameter("@porte", obj.Porte),
                new SqlParameter("@raca", obj.Raca),
                new SqlParameter("@foto", obj.Foto),
                new SqlParameter("@idCliente", obj.Cliente.Id),
                new SqlParameter("@regUser", obj.Cliente.Id)
            };
            obj.Id = (int)conex.ExecutaScalarSP(sp, parametros);
        }
        public List<Animal> Listar()
        {
            throw new NotImplementedException();
        }
        public List<Animal> Listar(Cliente cliente)
        {
            var conex = new BDconnection();
            string sp = "spListaAnimal";

            var parametros = new List<SqlParameter>
            {
                new SqlParameter("@idCliente", cliente.Id)
            };
            var dt = conex.ExecutaSpDataTable(sp, parametros);

            var animais = new List<Animal>();
            for (var i = 0; i < dt.Rows.Count; i++)
            {
                var animal = new Animal();


                if (dt.Rows.Count < 1)
                    throw new Exception("Animal não encontrado");

                animal.Id = Convert.ToInt32(dt.Rows[i]["Id"]);
                animal.Nome = dt.Rows[i]["Nome"].ToString();
                animal.Genero = dt.Rows[i]["Genero"].ToString()[0];
                animal.DataCadastro = Convert.ToDateTime(dt.Rows[i]["DataCadastro"]);
                animal.DataNascimento = Convert.ToDateTime(dt.Rows[i]["DataNascimento"]);
                animal.Especie = dt.Rows[i]["Especie"].ToString();
                animal.Porte = dt.Rows[i]["Porte"].ToString();
                animal.Raca = dt.Rows[i]["Raca"].ToString();
                animal.Foto = (byte[])dt.Rows[i]["Foto"];
                animal.Cliente = new Cliente { Id = Convert.ToInt32(dt.Rows[i]["IdCliente"]) };
                animais.Add(animal);
            }
            return animais;
        }

        public List<Animal> Listar(string pesquisa)
        {
            var conex = new BDconnection();
            string sp = "spListaAnimal";

            var parametros = new List<SqlParameter>
            {
                new SqlParameter("@pesquisa", pesquisa)
            };
            var dt = conex.ExecutaSpDataTable(sp, parametros);

            var animais = new List<Animal>();
            for (var i = 0; i < dt.Rows.Count; i++)
            {
                var animal = new Animal();


                if (dt.Rows.Count < 1)
                    throw new Exception("Animal não encontrado");

                animal.Id = Convert.ToInt32(dt.Rows[i]["Id"]);
                animal.Nome = dt.Rows[i]["Nome"].ToString();
                animal.Genero = dt.Rows[i]["Genero"].ToString()[0];
                animal.DataCadastro = Convert.ToDateTime(dt.Rows[i]["DataCadastro"]);
                animal.DataNascimento = Convert.ToDateTime(dt.Rows[i]["DataNascimento"]);
                animal.Especie = dt.Rows[i]["Especie"].ToString();
                animal.Porte = dt.Rows[i]["Porte"].ToString();
                animal.Raca = dt.Rows[i]["Raca"].ToString();
                animal.Foto = dt.Rows[i]["Foto"] == DBNull.Value? new byte[0] : (byte[])dt.Rows[i]["Foto"];
                animal.Cliente = new Cliente { Id = Convert.ToInt32(dt.Rows[i]["IdCliente"]) };
                animais.Add(animal);
            }
            return animais;
        }

        public void Salvar(Animal obj)
        {
            if (obj.Id == 0)
                Insert(obj);
            else
                Update(obj);
        }

        public void Update(Animal obj)
        {
            var conex = new BDconnection();
            string sp = "spUpdateAnimal";

            //Criando lista de parâmetros e inserindo um a um
            var parametros = new List<SqlParameter>
            {
                new SqlParameter("@id", obj.Id),
                new SqlParameter("@nome", obj.Nome),
                new SqlParameter("@genero", obj.Genero),
                new SqlParameter("@especie", obj.Especie),
                new SqlParameter("@porte", obj.Porte),
                new SqlParameter("@foto", obj.Foto),
                new SqlParameter("@raca", obj.Raca),
                new SqlParameter("@idCliente", obj.Cliente.Id)
            };
            conex.ExecutaNonQuerySP(sp, parametros);
        }
    }
}