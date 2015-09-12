using System;
using System.Data.Entity.Migrations;
using System.Linq;
using PetLab.DAL.Models;

namespace PetLab.DAL.Context {
	public sealed class PetLabDbContextConfiguration : DbMigrationsConfiguration<PetLabDbContext> {
		public PetLabDbContextConfiguration() {
			AutomaticMigrationsEnabled = true;
			AutomaticMigrationDataLossAllowed = true;
		}

		protected override void Seed(PetLabDbContext context) {
			if (context.equipments.Any() == false) {
				context.equipments.Add(new equipment {equipment_id = "PETLIN01"});
				context.equipments.Add(new equipment {equipment_id = "PETLIN02"});
				context.equipments.Add(new equipment {equipment_id = "PETLIN03"});
				context.equipments.Add(new equipment {equipment_id = "PETLIN04"});
				context.equipments.Add(new equipment {equipment_id = "PETLIN05"});
				context.equipments.Add(new equipment {equipment_id = "PETLIN06"});
				context.equipments.Add(new equipment {equipment_id = "PETLIN07"});
				context.equipments.Add(new equipment {equipment_id = "PETLIN08"});
				context.equipments.Add(new equipment {equipment_id = "PETLIN09"});
				context.equipments.Add(new equipment {equipment_id = "PETLIN10"});
				context.equipments.Add(new equipment {equipment_id = "PETLIN11"});
			}
			if (context.materials.Any() == false) {
				context.materials.Add(new material {material_id = "9999999999", name = "тестовый материал"});
			}
			if (context.defects.Any() == false) {
				context.defects.Add(new defect {defect_id = "9999", name = "тестовый дефект"});
			}
			if (context.users.Any() == false) {
				var user = new user {fio = "тестовый пользользователь"};
				context.users.Add(user);
				if (context.shift_number.Any() == false) {
					context.shift_number.Add(new shift_number {number = 1, user = user});
					context.shift_number.Add(new shift_number {number = 2, user = user});
					context.shift_number.Add(new shift_number {number = 3, user = user});
					context.shift_number.Add(new shift_number {number = 4, user = user});
				}
				if (context.shift_time.Any() == false) {
					context.shift_time.Add(new shift_time {
						time_id = 1,
						name = "дневная",
						begin = new TimeSpan(8, 0, 0),
						end = new TimeSpan(20, 0, 0)
					});
					context.shift_time.Add(new shift_time {
						time_id = 2,
						name = "ночная",
						begin = new TimeSpan(20, 0, 0),
						end = new TimeSpan(8, 0, 0)
					});
				}
				if (context.shifts.Any() == false) {
					context.shifts.Add(
						new shift {
							user = user,
							time_id = 1,
							datetime = DateTime.Now,
							shift_number = 1
						});
				}
				if (context.pickup_station_cooling.Any() == false) {
					context.pickup_station_cooling.Add(new pickup_station_cooling() {station_id = 1, name = "A"});
					context.pickup_station_cooling.Add(new pickup_station_cooling() {station_id = 2, name = "B"});
					context.pickup_station_cooling.Add(new pickup_station_cooling() {station_id = 3, name = "C"});
					context.pickup_station_cooling.Add(new pickup_station_cooling() {station_id = 4, name = "D"});
				}
				base.Seed(context);
			}
		}
	}
}