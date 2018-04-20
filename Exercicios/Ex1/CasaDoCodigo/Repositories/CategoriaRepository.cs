using CasaDoCodigo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasaDoCodigo.Repositories
{
    public interface ICategoriaRepository
    {
        int AddCategoria(string categoriaNome);
        int AddSubcategoria(string categoriaNome, string subcategoriaNome);
    }

    public class CategoriaRepository : BaseRepository<Categoria>, ICategoriaRepository
    {
        public CategoriaRepository(ApplicationContext contexto) : base(contexto)
        {
        }

        public int AddCategoria(string categoriaNome)
        {
            var categoriaDB =
                dbSet
                    .Where(c => c.Nome == categoriaNome)
                    .SingleOrDefault();

            if (categoriaDB != null)
            {
                dbSet.Add(categoriaDB);
                contexto.SaveChanges();
            }
            return categoriaDB.Id;
        }

        public int AddSubcategoria(string categoriaNome, string subcategoriaNome)
        {
            var categoriaDB =
                dbSet
                    .Where(c => c.Nome == categoriaNome)
                    .SingleOrDefault();

            if (categoriaDB == null)
            {
                throw new ArgumentException("Categoria não encontrada");
            }

            var subcategoriaDB =
                contexto.Set<Subcategoria>()
                    .Where(s =>
                    s.Categoria.Id == categoriaDB.Id
                    && s.Nome == categoriaNome)
                    .SingleOrDefault();

            if (subcategoriaDB != null)
            {
                contexto.Set<Subcategoria>().Add(new Subcategoria());
                contexto.SaveChanges();
            }
            return categoriaDB.Id;
        }
    }
}
