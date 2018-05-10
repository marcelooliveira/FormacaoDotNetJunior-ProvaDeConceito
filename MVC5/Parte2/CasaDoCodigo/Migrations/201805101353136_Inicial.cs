namespace CasaDoCodigo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Inicial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Produto",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Codigo = c.String(nullable: false),
                        Nome = c.String(nullable: false),
                        Preco = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Pedido",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Cadastro_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cadastro", t => t.Cadastro_Id, cascadeDelete: true)
                .Index(t => t.Cadastro_Id);
            
            CreateTable(
                "dbo.Cadastro",
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
                .ForeignKey("dbo.Pedido", t => t.Pedido_Id)
                .Index(t => t.Pedido_Id);
            
            CreateTable(
                "dbo.ItemPedido",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Quantidade = c.Int(nullable: false),
                        PrecoUnitario = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Produto_Id = c.Int(nullable: false),
                        Pedido_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Produto", t => t.Produto_Id, cascadeDelete: true)
                .ForeignKey("dbo.Pedido", t => t.Pedido_Id, cascadeDelete: true)
                .Index(t => t.Produto_Id)
                .Index(t => t.Pedido_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ItemPedido", "Pedido_Id", "dbo.Pedido");
            DropForeignKey("dbo.ItemPedido", "Produto_Id", "dbo.Produto");
            DropForeignKey("dbo.Pedido", "Cadastro_Id", "dbo.Cadastro");
            DropForeignKey("dbo.Cadastro", "Pedido_Id", "dbo.Pedido");
            DropIndex("dbo.ItemPedido", new[] { "Pedido_Id" });
            DropIndex("dbo.ItemPedido", new[] { "Produto_Id" });
            DropIndex("dbo.Cadastro", new[] { "Pedido_Id" });
            DropIndex("dbo.Pedido", new[] { "Cadastro_Id" });
            DropTable("dbo.ItemPedido");
            DropTable("dbo.Cadastro");
            DropTable("dbo.Pedido");
            DropTable("dbo.Produto");
        }
    }
}
