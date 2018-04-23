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
        Subcategoria AddSubcategoria(string categoriaNome, string subcategoriaNome);
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

        public Subcategoria AddSubcategoria(string categoriaNome, string subcategoriaNome)
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
                    && s.Nome == subcategoriaNome)
                    .SingleOrDefault();

            if (subcategoriaDB == null)
            {
                subcategoriaDB = new Subcategoria(categoriaDB, subcategoriaNome);
                contexto.Set<Subcategoria>()
                    .Add(subcategoriaDB);
                contexto.SaveChanges();
            }
            return subcategoriaDB;
        }
    }
}
