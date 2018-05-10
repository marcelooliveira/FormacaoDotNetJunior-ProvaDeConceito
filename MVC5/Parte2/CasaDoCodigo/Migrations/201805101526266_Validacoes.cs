namespace CasaDoCodigo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Validacoes : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Cadastro", "Nome", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Cadastro", "Email", c => c.String(nullable: false));
            AlterColumn("dbo.Cadastro", "Telefone", c => c.String(nullable: false));
            AlterColumn("dbo.Cadastro", "Endereco", c => c.String(nullable: false));
            AlterColumn("dbo.Cadastro", "Complemento", c => c.String(nullable: false));
            AlterColumn("dbo.Cadastro", "Bairro", c => c.String(nullable: false));
            AlterColumn("dbo.Cadastro", "Municipio", c => c.String(nullable: false));
            AlterColumn("dbo.Cadastro", "UF", c => c.String(nullable: false));
            AlterColumn("dbo.Cadastro", "CEP", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Cadastro", "CEP", c => c.String());
            AlterColumn("dbo.Cadastro", "UF", c => c.String());
            AlterColumn("dbo.Cadastro", "Municipio", c => c.String());
            AlterColumn("dbo.Cadastro", "Bairro", c => c.String());
            AlterColumn("dbo.Cadastro", "Complemento", c => c.String());
            AlterColumn("dbo.Cadastro", "Endereco", c => c.String());
            AlterColumn("dbo.Cadastro", "Telefone", c => c.String());
            AlterColumn("dbo.Cadastro", "Email", c => c.String());
            AlterColumn("dbo.Cadastro", "Nome", c => c.String());
        }
    }
}
