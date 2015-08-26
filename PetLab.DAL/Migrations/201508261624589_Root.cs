namespace PetLab.DAL.Context
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Root : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.defect",
                c => new
                    {
                        defect_id = c.String(nullable: false, maxLength: 4, fixedLength: true),
                        name = c.String(nullable: false, maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.defect_id);
            
            CreateTable(
                "dbo.pickup_defects",
                c => new
                    {
                        socket = c.Byte(nullable: false),
                        defect_id = c.String(nullable: false, maxLength: 4, fixedLength: true),
                        pickup_id = c.Int(nullable: false),
                        grade = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => new { t.socket, t.defect_id, t.pickup_id })
                .ForeignKey("dbo.pickup", t => t.pickup_id)
                .ForeignKey("dbo.defect", t => t.defect_id)
                .Index(t => t.defect_id)
                .Index(t => t.pickup_id);
            
            CreateTable(
                "dbo.pickup",
                c => new
                    {
                        pickup_id = c.Int(nullable: false, identity: true),
                        order_id = c.String(nullable: false, maxLength: 10, fixedLength: true),
                        shift_id = c.Int(nullable: false),
                        datetime_take = c.DateTime(nullable: false, storeType: "smalldatetime"),
                        datetime_create = c.DateTime(nullable: false, storeType: "smalldatetime"),
                        datetime_close = c.DateTime(storeType: "smalldatetime"),
                        box_id = c.Int(),
                        station_id = c.Byte(nullable: false),
                        number = c.Byte(nullable: false),
                        etalon_match = c.Boolean(nullable: false),
                        visual_match = c.Boolean(nullable: false),
                        export = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.pickup_id)
                .ForeignKey("dbo.order", t => t.order_id)
                .ForeignKey("dbo.shift", t => t.shift_id)
                .ForeignKey("dbo.pickup_station_cooling", t => t.station_id)
                .Index(t => t.order_id)
                .Index(t => t.shift_id)
                .Index(t => t.station_id);
            
            CreateTable(
                "dbo.equipment",
                c => new
                    {
                        equipment_id = c.String(nullable: false, maxLength: 8, fixedLength: true),
                        pickup_id = c.Int(),
                    })
                .PrimaryKey(t => t.equipment_id)
                .ForeignKey("dbo.pickup", t => t.pickup_id)
                .Index(t => t.pickup_id);
            
            CreateTable(
                "dbo.order",
                c => new
                    {
                        order_id = c.String(nullable: false, maxLength: 10, fixedLength: true),
                        batch_id = c.String(nullable: false, maxLength: 10, fixedLength: true),
                        material_id = c.String(nullable: false, maxLength: 10, fixedLength: true),
                        shift_id = c.Int(nullable: false),
                        dye_name = c.String(nullable: false, maxLength: 50, unicode: false),
                        color_shade = c.String(nullable: false, maxLength: 50, unicode: false),
                        count_socket = c.Short(nullable: false),
                        equipment_id = c.String(nullable: false, maxLength: 8, fixedLength: true),
                    })
                .PrimaryKey(t => t.order_id)
                .ForeignKey("dbo.material", t => t.material_id)
                .ForeignKey("dbo.shift", t => t.shift_id)
                .ForeignKey("dbo.equipment", t => t.equipment_id)
                .Index(t => t.material_id)
                .Index(t => t.shift_id)
                .Index(t => t.equipment_id);
            
            CreateTable(
                "dbo.material",
                c => new
                    {
                        material_id = c.String(nullable: false, maxLength: 10, fixedLength: true),
                        name = c.String(nullable: false, maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.material_id);
            
            CreateTable(
                "dbo.order_etalon_color",
                c => new
                    {
                        order_id = c.String(nullable: false, maxLength: 10, fixedLength: true),
                        name = c.String(nullable: false, maxLength: 50, unicode: false),
                        socket_number = c.Byte(nullable: false),
                        pivkup_mode = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.order_id)
                .ForeignKey("dbo.order", t => t.order_id)
                .Index(t => t.order_id);
            
            CreateTable(
                "dbo.order_etalon_color_range",
                c => new
                    {
                        order_id = c.String(nullable: false, maxLength: 10, fixedLength: true),
                        name = c.String(nullable: false, maxLength: 20, fixedLength: true),
                        lim1 = c.Decimal(nullable: false, precision: 6, scale: 2),
                        lim2 = c.Decimal(nullable: false, precision: 6, scale: 2),
                        lim3 = c.Decimal(nullable: false, precision: 6, scale: 2),
                        lim4 = c.Decimal(nullable: false, precision: 6, scale: 2),
                        lim5 = c.Decimal(nullable: false, precision: 6, scale: 2),
                    })
                .PrimaryKey(t => new { t.order_id, t.name })
                .ForeignKey("dbo.order", t => t.order_id)
                .Index(t => t.order_id);
            
            CreateTable(
                "dbo.order_etalon_color_ray",
                c => new
                    {
                        order_id = c.String(nullable: false, maxLength: 10, fixedLength: true),
                        ray_id = c.String(nullable: false, maxLength: 10, fixedLength: true),
                        value = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.order_id, t.ray_id })
                .ForeignKey("dbo.order", t => t.order_id)
                .Index(t => t.order_id);
            
            CreateTable(
                "dbo.order_etalon_slip",
                c => new
                    {
                        order_id = c.String(nullable: false, maxLength: 10, fixedLength: true),
                        name = c.String(nullable: false, maxLength: 50, unicode: false),
                        lim1 = c.Decimal(nullable: false, precision: 6, scale: 2),
                        lim2 = c.Decimal(nullable: false, precision: 6, scale: 2),
                        lim3 = c.Decimal(nullable: false, precision: 6, scale: 2),
                        lim4 = c.Decimal(nullable: false, precision: 6, scale: 2),
                        lim5 = c.Decimal(nullable: false, precision: 6, scale: 2),
                    })
                .PrimaryKey(t => t.order_id)
                .ForeignKey("dbo.order", t => t.order_id)
                .Index(t => t.order_id);
            
            CreateTable(
                "dbo.order_etalon_thickness",
                c => new
                    {
                        order_id = c.String(nullable: false, maxLength: 10, fixedLength: true),
                        name = c.String(nullable: false, maxLength: 50, unicode: false),
                        lim3 = c.Decimal(nullable: false, precision: 6, scale: 2),
                        lim4 = c.Decimal(nullable: false, precision: 6, scale: 2),
                        lim5 = c.Decimal(nullable: false, precision: 6, scale: 2),
                    })
                .PrimaryKey(t => t.order_id)
                .ForeignKey("dbo.order", t => t.order_id)
                .Index(t => t.order_id);
            
            CreateTable(
                "dbo.order_etalon_weight",
                c => new
                    {
                        order_id = c.String(nullable: false, maxLength: 10, fixedLength: true),
                        name = c.String(nullable: false, maxLength: 50, unicode: false),
                        lim1 = c.Decimal(nullable: false, precision: 6, scale: 2),
                        lim2 = c.Decimal(nullable: false, precision: 6, scale: 2),
                        lim3 = c.Decimal(nullable: false, precision: 6, scale: 2),
                        lim4 = c.Decimal(nullable: false, precision: 6, scale: 2),
                        lim5 = c.Decimal(nullable: false, precision: 6, scale: 2),
                    })
                .PrimaryKey(t => t.order_id)
                .ForeignKey("dbo.order", t => t.order_id)
                .Index(t => t.order_id);
            
            CreateTable(
                "dbo.shift",
                c => new
                    {
                        shift_id = c.Int(nullable: false, identity: true),
                        datetime = c.DateTime(nullable: false, storeType: "smalldatetime"),
                        shift_number = c.Byte(nullable: false),
                        time_id = c.Byte(nullable: false),
                        user_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.shift_id)
                .ForeignKey("dbo.shift_number", t => t.shift_number)
                .ForeignKey("dbo.user", t => t.user_id)
                .ForeignKey("dbo.shift_time", t => t.time_id)
                .Index(t => t.shift_number)
                .Index(t => t.time_id)
                .Index(t => t.user_id);
            
            CreateTable(
                "dbo.shift_number",
                c => new
                    {
                        number = c.Byte(nullable: false),
                        user_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.number)
                .ForeignKey("dbo.user", t => t.user_id)
                .Index(t => t.user_id);
            
            CreateTable(
                "dbo.user",
                c => new
                    {
                        user_id = c.Int(nullable: false, identity: true),
                        fio = c.String(nullable: false, maxLength: 255, unicode: false),
                    })
                .PrimaryKey(t => t.user_id);
            
            CreateTable(
                "dbo.shift_time",
                c => new
                    {
                        time_id = c.Byte(nullable: false),
                        name = c.String(nullable: false, maxLength: 8, unicode: false),
                        begin = c.Time(nullable: false, precision: 7),
                        end = c.Time(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.time_id);
            
            CreateTable(
                "dbo.pickup_station_cooling",
                c => new
                    {
                        station_id = c.Byte(nullable: false),
                        name = c.String(nullable: false, maxLength: 1, fixedLength: true, unicode: false),
                    })
                .PrimaryKey(t => t.station_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.pickup_defects", "defect_id", "dbo.defect");
            DropForeignKey("dbo.pickup", "station_id", "dbo.pickup_station_cooling");
            DropForeignKey("dbo.pickup_defects", "pickup_id", "dbo.pickup");
            DropForeignKey("dbo.equipment", "pickup_id", "dbo.pickup");
            DropForeignKey("dbo.order", "equipment_id", "dbo.equipment");
            DropForeignKey("dbo.shift", "time_id", "dbo.shift_time");
            DropForeignKey("dbo.shift", "user_id", "dbo.user");
            DropForeignKey("dbo.shift_number", "user_id", "dbo.user");
            DropForeignKey("dbo.shift", "shift_number", "dbo.shift_number");
            DropForeignKey("dbo.pickup", "shift_id", "dbo.shift");
            DropForeignKey("dbo.order", "shift_id", "dbo.shift");
            DropForeignKey("dbo.pickup", "order_id", "dbo.order");
            DropForeignKey("dbo.order_etalon_weight", "order_id", "dbo.order");
            DropForeignKey("dbo.order_etalon_thickness", "order_id", "dbo.order");
            DropForeignKey("dbo.order_etalon_slip", "order_id", "dbo.order");
            DropForeignKey("dbo.order_etalon_color_ray", "order_id", "dbo.order");
            DropForeignKey("dbo.order_etalon_color_range", "order_id", "dbo.order");
            DropForeignKey("dbo.order_etalon_color", "order_id", "dbo.order");
            DropForeignKey("dbo.order", "material_id", "dbo.material");
            DropIndex("dbo.shift_number", new[] { "user_id" });
            DropIndex("dbo.shift", new[] { "user_id" });
            DropIndex("dbo.shift", new[] { "time_id" });
            DropIndex("dbo.shift", new[] { "shift_number" });
            DropIndex("dbo.order_etalon_weight", new[] { "order_id" });
            DropIndex("dbo.order_etalon_thickness", new[] { "order_id" });
            DropIndex("dbo.order_etalon_slip", new[] { "order_id" });
            DropIndex("dbo.order_etalon_color_ray", new[] { "order_id" });
            DropIndex("dbo.order_etalon_color_range", new[] { "order_id" });
            DropIndex("dbo.order_etalon_color", new[] { "order_id" });
            DropIndex("dbo.order", new[] { "equipment_id" });
            DropIndex("dbo.order", new[] { "shift_id" });
            DropIndex("dbo.order", new[] { "material_id" });
            DropIndex("dbo.equipment", new[] { "pickup_id" });
            DropIndex("dbo.pickup", new[] { "station_id" });
            DropIndex("dbo.pickup", new[] { "shift_id" });
            DropIndex("dbo.pickup", new[] { "order_id" });
            DropIndex("dbo.pickup_defects", new[] { "pickup_id" });
            DropIndex("dbo.pickup_defects", new[] { "defect_id" });
            DropTable("dbo.pickup_station_cooling");
            DropTable("dbo.shift_time");
            DropTable("dbo.user");
            DropTable("dbo.shift_number");
            DropTable("dbo.shift");
            DropTable("dbo.order_etalon_weight");
            DropTable("dbo.order_etalon_thickness");
            DropTable("dbo.order_etalon_slip");
            DropTable("dbo.order_etalon_color_ray");
            DropTable("dbo.order_etalon_color_range");
            DropTable("dbo.order_etalon_color");
            DropTable("dbo.material");
            DropTable("dbo.order");
            DropTable("dbo.equipment");
            DropTable("dbo.pickup");
            DropTable("dbo.pickup_defects");
            DropTable("dbo.defect");
        }
    }
}
