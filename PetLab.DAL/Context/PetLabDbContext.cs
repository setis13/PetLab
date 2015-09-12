using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using PetLab.DAL.Contracts.Context;
using PetLab.DAL.Models;

namespace PetLab.DAL.Context {
	public class PetLabDbContext : DbContext, IPetLabDbContext {
		public PetLabDbContext()
			: base("name=PetLab") {
		}

		public virtual DbSet<defect> defects { get; set; }
		public virtual DbSet<equipment> equipments { get; set; }
		public virtual DbSet<material> materials { get; set; }
		public virtual DbSet<order> orders { get; set; }
		public virtual DbSet<order_etalon_color> order_etalon_color { get; set; }
		public virtual DbSet<order_etalon_color_range> order_etalon_color_range { get; set; }
		public virtual DbSet<order_etalon_color_ray> order_etalon_color_ray { get; set; }
		public virtual DbSet<order_etalon_slip> order_etalon_slip { get; set; }
		public virtual DbSet<order_etalon_thickness> order_etalon_thickness { get; set; }
		public virtual DbSet<order_etalon_weight> order_etalon_weight { get; set; }
		public virtual DbSet<pickup> pickups { get; set; }
		public virtual DbSet<pickup_defects> pickup_defects { get; set; }
		public virtual DbSet<pickup_station_cooling> pickup_station_cooling { get; set; }
		public virtual DbSet<shift> shifts { get; set; }
		public virtual DbSet<shift_number> shift_number { get; set; }
		public virtual DbSet<shift_time> shift_time { get; set; }
		public virtual DbSet<user> users { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder) {
			modelBuilder.Entity<defect>()
				.Property(e => e.defect_id)
				.IsFixedLength();

			modelBuilder.Entity<defect>()
				.Property(e => e.name)
				.IsUnicode(false);

			modelBuilder.Entity<defect>()
				.HasMany(e => e.pickup_defects)
				.WithRequired(e => e.defect)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<equipment>()
				.Property(e => e.equipment_id)
				.IsFixedLength();

			modelBuilder.Entity<equipment>()
				.HasMany(e => e.orders)
				.WithRequired(e => e.equipment)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<material>()
				.Property(e => e.material_id)
				.IsFixedLength();

			modelBuilder.Entity<material>()
				.Property(e => e.name)
				.IsUnicode(false);

			modelBuilder.Entity<material>()
				.HasMany(e => e.orders)
				.WithRequired(e => e.material)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<order>()
				.Property(e => e.order_id)
				.IsFixedLength();

			modelBuilder.Entity<order>()
				.Property(e => e.batch_id)
				.IsFixedLength();

			modelBuilder.Entity<order>()
				.Property(e => e.material_id)
				.IsFixedLength();

			modelBuilder.Entity<order>()
				.Property(e => e.dye_name)
				.IsUnicode(false);

			modelBuilder.Entity<order>()
				.Property(e => e.color_shade)
				.IsUnicode(false);

			modelBuilder.Entity<order>()
				.Property(e => e.equipment_id)
				.IsFixedLength();

			modelBuilder.Entity<order>()
				.HasOptional(e => e.order_etalon_color)
				.WithRequired(e => e.order);

			modelBuilder.Entity<order>()
				.HasMany(e => e.order_etalon_color_range)
				.WithRequired(e => e.order)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<order>()
				.HasMany(e => e.order_etalon_color_ray)
				.WithRequired(e => e.order)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<order>()
				.HasOptional(e => e.order_etalon_slip)
				.WithRequired(e => e.order);

			modelBuilder.Entity<order>()
				.HasOptional(e => e.order_etalon_thickness)
				.WithRequired(e => e.order);

			modelBuilder.Entity<order>()
				.HasOptional(e => e.order_etalon_weight)
				.WithRequired(e => e.order);

			modelBuilder.Entity<order>()
				.HasMany(e => e.pickups)
				.WithRequired(e => e.order)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<order_etalon_color>()
				.Property(e => e.order_id)
				.IsFixedLength();

			modelBuilder.Entity<order_etalon_color>()
				.Property(e => e.name)
				.IsUnicode(false);

			modelBuilder.Entity<order_etalon_color>()
				.HasRequired(e => e.order)
				.WithOptional(e => e.order_etalon_color);

			modelBuilder.Entity<order_etalon_color_range>()
				.Property(e => e.order_id)
				.IsFixedLength();

			var properties = new[] {
				modelBuilder.Entity<order_etalon_color_range>().Property(e => e.lim1),
				modelBuilder.Entity<order_etalon_color_range>().Property(e => e.lim2),
				modelBuilder.Entity<order_etalon_color_range>().Property(e => e.lim3),
				modelBuilder.Entity<order_etalon_color_range>().Property(e => e.lim4),
				modelBuilder.Entity<order_etalon_color_range>().Property(e => e.lim5)
			};
			properties.ToList().ForEach(p => p.HasPrecision(6, 2));

			modelBuilder.Entity<order_etalon_color_range>()
				.Property(e => e.name)
				.IsFixedLength();

			modelBuilder.Entity<order_etalon_color_ray>()
				.Property(e => e.order_id)
				.IsFixedLength();

			modelBuilder.Entity<order_etalon_color_ray>()
				.Property(e => e.ray_id)
				.IsFixedLength();

			modelBuilder.Entity<order_etalon_slip>()
				.Property(e => e.order_id)
				.IsFixedLength();

			modelBuilder.Entity<order_etalon_slip>()
				.Property(e => e.name)
				.IsUnicode(false);

			properties = new[] {
				modelBuilder.Entity<order_etalon_slip>().Property(e => e.lim1),
				modelBuilder.Entity<order_etalon_slip>().Property(e => e.lim2),
				modelBuilder.Entity<order_etalon_slip>().Property(e => e.lim3),
				modelBuilder.Entity<order_etalon_slip>().Property(e => e.lim4),
				modelBuilder.Entity<order_etalon_slip>().Property(e => e.lim5)
			};
			properties.ToList().ForEach(p => p.HasPrecision(6, 2));

			modelBuilder.Entity<order_etalon_thickness>()
				.Property(e => e.order_id)
				.IsFixedLength();

			modelBuilder.Entity<order_etalon_thickness>()
				.Property(e => e.name)
				.IsUnicode(false);

			properties = new[] {
				modelBuilder.Entity<order_etalon_thickness>().Property(e => e.lim3),
				modelBuilder.Entity<order_etalon_thickness>().Property(e => e.lim4),
				modelBuilder.Entity<order_etalon_thickness>().Property(e => e.lim5)
			};
			properties.ToList().ForEach(p => p.HasPrecision(6, 2));

			modelBuilder.Entity<order_etalon_weight>()
				.Property(e => e.order_id)
				.IsFixedLength();

			modelBuilder.Entity<order_etalon_weight>()
				.Property(e => e.name)
				.IsUnicode(false);

			properties = new[] {
				modelBuilder.Entity<order_etalon_weight>().Property(e => e.lim1),
				modelBuilder.Entity<order_etalon_weight>().Property(e => e.lim2),
				modelBuilder.Entity<order_etalon_weight>().Property(e => e.lim3),
				modelBuilder.Entity<order_etalon_weight>().Property(e => e.lim4),
				modelBuilder.Entity<order_etalon_weight>().Property(e => e.lim5)
			};
			properties.ToList().ForEach(p => p.HasPrecision(6, 2));

			modelBuilder.Entity<pickup>()
				.Property(e => e.order_id)
				.IsFixedLength();

			modelBuilder.Entity<pickup>()
				.HasMany(e => e.pickup_defects)
				.WithRequired(e => e.pickup)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<pickup>()
				.HasMany(e => e.pickup_etalon_color_ranges)
				.WithRequired(e => e.pickup);

			modelBuilder.Entity<order_etalon_color_range>()
				.HasMany(e => e.pickup_etalon_color_ranges)
				.WithRequired(e => e.order_etalon_color_range);

			modelBuilder.Entity<pickup_defects>()
				.Property(e => e.defect_id)
				.IsFixedLength();

			modelBuilder.Entity<pickup_station_cooling>()
				.Property(e => e.name)
				.IsFixedLength()
				.IsUnicode(false);

			modelBuilder.Entity<pickup_station_cooling>()
				.HasMany(e => e.pickups)
				.WithRequired(e => e.pickup_station_cooling)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<shift>()
				.HasMany(e => e.pickups)
				.WithRequired(e => e.shift)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<shift_number>()
				.HasMany(e => e.shifts)
				.WithRequired(e => e.shift_number1)
				.HasForeignKey(e => e.shift_number)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<shift_time>()
				.Property(e => e.name)
				.IsUnicode(false);

			modelBuilder.Entity<shift_time>()
				.HasMany(e => e.shifts)
				.WithRequired(e => e.shift_time)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<user>()
				.Property(e => e.fio)
				.IsUnicode(false);

			modelBuilder.Entity<user>()
				.HasMany(e => e.shifts)
				.WithRequired(e => e.user)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<user>()
				.HasMany(e => e.shift_number)
				.WithRequired(e => e.user)
				.WillCascadeOnDelete(false);
		}
	}
}