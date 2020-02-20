namespace Passionproject2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class krina : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bakeries",
                c => new
                    {
                        BakeryID = c.Int(nullable: false, identity: true),
                        BakeryName = c.String(),
                        BakeryAddress = c.String(),
                    })
                .PrimaryKey(t => t.BakeryID);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        CustomerID = c.Int(nullable: false, identity: true),
                        CustomerName = c.String(),
                        Emailid = c.String(),
                        Phone = c.String(),
                        CustomerAddress = c.String(),
                    })
                .PrimaryKey(t => t.CustomerID);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderID = c.Int(nullable: false, identity: true),
                        OrderItem = c.String(),
                        OrderDate = c.String(),
                        OrderItemQty = c.Int(nullable: false),
                        OrderCost = c.Int(nullable: false),
                        CustomerID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.OrderID)
                .ForeignKey("dbo.Customers", t => t.CustomerID, cascadeDelete: true)
                .Index(t => t.CustomerID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "CustomerID", "dbo.Customers");
            DropIndex("dbo.Orders", new[] { "CustomerID" });
            DropTable("dbo.Orders");
            DropTable("dbo.Customers");
            DropTable("dbo.Bakeries");
        }
    }
}
