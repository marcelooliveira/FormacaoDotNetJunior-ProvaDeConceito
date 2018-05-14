using CasaDoCodigo.Models;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasaDoCodigo
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var cadastroEntity = modelBuilder.Entity<Cadastro>();
            cadastroEntity.HasKey(t => t.Id);
            cadastroEntity.HasOne(t => t.Pedido);

            var produtoEntity = modelBuilder.Entity<Produto>();
            produtoEntity.HasKey(t => t.Id);

            var pedidoEntity = modelBuilder.Entity<Pedido>();
            pedidoEntity.HasKey(t => t.Id);
            pedidoEntity.HasMany(t => t.Itens).WithOne(t => t.Pedido);
            pedidoEntity.HasOne(t => t.Cadastro).WithOne(t => t.Pedido).IsRequired();

            var itemPedidoEntity = modelBuilder.Entity<ItemPedido>();
            itemPedidoEntity.HasKey(t => t.Id);
            itemPedidoEntity.HasOne(t => t.Pedido);
            itemPedidoEntity.HasOne(t => t.Produto);
        }
    }
}
