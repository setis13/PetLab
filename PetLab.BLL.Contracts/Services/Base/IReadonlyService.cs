using System;
using System.Collections.Generic;

namespace PetLab.BLL.Contracts.Services.Base {
    /// <summary>
    /// Readonly service contract
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IReadonlyService<T> : IService {
        /// <summary>
        /// Get all models list
        /// </summary>
        /// <returns>Models list</returns>
        List<T> GetAll();
        /// <summary>
        /// Get model by ID
        /// </summary>
        /// <param name="id">Model ID</param>
        /// <returns>Model</returns>
        T GetById(Guid id);
    }
}
