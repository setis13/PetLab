using System;

namespace PetLab.BLL.Contracts.Services.Base {
    /// <summary>
    /// Generic CRUD service contract
    /// </summary>
    /// <typeparam name="T">Model type</typeparam>
    public interface ICrudService<T> : IReadonlyService<T> {
        /// <summary>
        /// Save model
        /// </summary>
        /// <param name="model"></param>
        void Save(T model);
        /// <summary>
        /// Delete model
        /// </summary>
        /// <param name="model">Model</param>
        void Delete(T model);
        /// <summary>
        /// Delete mnodel by Id
        /// </summary>
        /// <param name="modelId">Model id</param>
        void Delete(Guid modelId);
    }
}
