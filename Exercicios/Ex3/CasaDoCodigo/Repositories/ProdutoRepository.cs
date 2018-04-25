using CasaDoCodigo.Models;
using CasaDoCodigo.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasaDoCodigo.Repositories
{
    public interface IProdutoRepository
    {
        void SaveProdutos(List<Livro> livros);
        IList<Produto> GetProdutos();
        BuscaProdutosViewModel GetProdutos(string pesquisa);
    }

    public class ProdutoRepository : BaseRepository<Produto>, IProdutoRepository
    {
        private readonly ICategoriaRepository categoriaRepository;

        public ProdutoRepository(ApplicationContext contexto,
            ICategoriaRepository categoriaRepository) : base(contexto)
        {
            this.categoriaRepository = categoriaRepository;
        }

        public IList<Produto> GetProdutos()
        {
            return dbSet
                .Include(prod => prod.Categoria)
            .ToList();
        }

        public BuscaProdutosViewModel GetProdutos(string pesquisa)
        {
            IQueryable<Produto> query = dbSet;

            if (!string.IsNullOrEmpty(pesquisa))
            {
                query = query.Where(q => q.Nome.Contains(pesquisa));
            }

            query = query
                .Include(prod => prod.Categoria);

            return new BuscaProdutosViewModel(query.ToList(), pesquisa);
        }

        public void SaveProdutos(List<Livro> livros)
        {
            foreach (var livro in livros)
            {
                var categoria = categoriaRepository.AddCategoria(livro.Categoria);

                if (!dbSet.Where(p => p.Codigo == livro.Codigo).Any())
                {
                    dbSet.Add(new Produto(livro.Codigo, livro.Nome, livro.Preco, categoria));
                }
            }
            contexto.SaveChanges();
        }
    }

    public class Livro
    {
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public string Categoria { get; set; }
        public string Subcategoria { get; set; }
        public decimal Preco { get; set; }
    }
}
