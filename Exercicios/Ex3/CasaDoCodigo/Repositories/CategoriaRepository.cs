using CasaDoCodigo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasaDoCodigo.Repositories
{
    public interface ICategoriaRepository
    {
        Categoria AddCategoria(string categoriaNome);
    }

    public class CategoriaRepository : BaseRepository<Categoria>, ICategoriaRepository
    {
        public CategoriaRepository(ApplicationContext contexto) : base(contexto)
        {
        }

        public Categoria AddCategoria(string categoriaNome)
        {
            var categoriaDB =
                dbSet
                    .Where(c => c.Nome == categoriaNome)
                    .SingleOrDefault();

            if (categoriaDB == null)
            {
                categoriaDB = new Categoria(categoriaNome);
                dbSet.Add(categoriaDB);
                contexto.SaveChanges();
            }
            return categoriaDB;
        }

    }
}
