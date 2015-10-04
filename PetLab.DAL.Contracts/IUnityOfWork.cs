﻿using PetLab.DAL.Contracts.Context;
using PetLab.DAL.Contracts.Models.Base;
using PetLab.DAL.Contracts.Repositories.Base;
using PetLab.DAL.Contracts.Scanners;
using PetLab.DAL.Contracts.Scanners.Base;

namespace PetLab.DAL.Contracts {
	public interface IUnitOfWork {
		/// <summary>
		/// Register custom repository
		/// </summary>
		/// <param name="xmlRepository">Repository instance</param>
		void RegisterXmlRepository<T>(T xmlRepository) where T : IXmlRepository;
		/// <summary>
		/// Gets application context instance
		/// </summary>
		IPetLabDbContext Context { get; }
		/// <summary>
		/// Gets application xml context instance
		/// </summary>
		IPetLabXmlContext XmlContext { get; }
		/// <summary>
		/// Get repository by entity type
		/// </summary>
		/// <typeparam name="T">Entity type</typeparam>
		/// <returns>Repository instance</returns>
		IEntityRepository<T> GetRepository<T>() where T : BaseEntity;
		/// <summary>
		/// Get repository by repository type
		/// </summary>
		/// <typeparam name="T">Custom repository type</typeparam>
		/// <returns>Custom repository instance</returns>
		T GetXmlRepository<T>() where T : IXmlRepository;
		/// <summary>
		/// Get scanner by scanner type
		/// </summary>
		/// <typeparam name="T">Scanner type</typeparam>
		/// <returns>scanner instance</returns>
		T GetScanner<T>() where T : IScanner;
		/// <summary>
		/// Rollback uncommited changes
		/// </summary>
		void RollBack();
		/// <summary>
		/// Commit changes
		/// </summary>
		void SaveChanges();
	}
}
