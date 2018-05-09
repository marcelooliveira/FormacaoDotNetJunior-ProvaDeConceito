namespace CasaDoCodigo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Inicial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Produtoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Codigo = c.String(nullable: false),
                        Nome = c.String(nullable: false),
                        Preco = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Pedidoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Cadastro_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cadastroes", t => t.Cadastro_Id, cascadeDelete: true)
                .Index(t => t.Cadastro_Id);
            
            CreateTable(
                "dbo.Cadastroes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                        Email = c.String(),
                        Telefone = c.String(),
                        Endereco = c.String(),
                        Complemento = c.String(),
                        Bairro = c.String(),
                        Municipio = c.String(),
                        UF = c.String(),
                        CEP = c.String(),
                        Pedido_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Pedidoes", t => t.Pedido_Id)
                .Index(t => t.Pedido_Id);
            
            CreateTable(
                "dbo.ItemPedidoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Quantidade = c.Int(nullable: false),
                        PrecoUnitario = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Produto_Id = c.Int(nullable: false),
                        Pedido_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Produtoes", t => t.Produto_Id, cascadeDelete: true)
                .ForeignKey("dbo.Pedidoes", t => t.Pedido_Id, cascadeDelete: true)
                .Index(t => t.Produto_Id)
                .Index(t => t.Pedido_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ItemPedidoes", "Pedido_Id", "dbo.Pedidoes");
            DropForeignKey("dbo.ItemPedidoes", "Produto_Id", "dbo.Produtoes");
            DropForeignKey("dbo.Pedidoes", "Cadastro_Id", "dbo.Cadastroes");
            DropForeignKey("dbo.Cadastroes", "Pedido_Id", "dbo.Pedidoes");
            DropIndex("dbo.ItemPedidoes", new[] { "Pedido_Id" });
            DropIndex("dbo.ItemPedidoes", new[] { "Produto_Id" });
            DropIndex("dbo.Cadastroes", new[] { "Pedido_Id" });
            DropIndex("dbo.Pedidoes", new[] { "Cadastro_Id" });
            DropTable("dbo.ItemPedidoes");
            DropTable("dbo.Cadastroes");
            DropTable("dbo.Pedidoes");
            DropTable("dbo.Produtoes");
        }
    }
}
