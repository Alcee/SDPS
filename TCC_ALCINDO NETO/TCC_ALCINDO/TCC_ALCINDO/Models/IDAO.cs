using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TCC_ALCINDO.Models
{
    public interface IDAO<T>
    {
        void Salvar(T obj);
        void Insert(T obj);
        void Delete(T obj);
        void Update(T obj);
        T Buscar(int id);
        List<T> Listar();
        List<T> Listar(string pesquisa);
    }
}