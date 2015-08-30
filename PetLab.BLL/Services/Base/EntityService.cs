using System;
using System.Collections.Generic;
using PetLab.BLL.Contracts.Services.Base;
using PetLab.DAL.Contracts;
using PetLab.DAL.Contracts.Models.Base;

namespace PetLab.BLL.Services.Base {

    public class EntityService<TDto, TEntity> : ICrudService<TDto>
        where TDto: class 
        where TEntity: BaseEntity {
       /// <summary>
        /// The unit of work.
        /// </summary>
        protected readonly IUnitOfWork UnitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="StudioService"/> class.
        /// </summary>
        /// <param name="unitOfWork"></param>
        public EntityService(IUnitOfWork unitOfWork) {
            this.UnitOfWork = unitOfWork;
        }


        /// <summary>
        /// The model get by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>The <see cref="TDto"/>.</returns>
        public TDto GetById(Guid id) {
            var store = this.UnitOfWork.GetRepository<TEntity>().GetById(id);
            return AutoMapper.Mapper.Map<TDto>(store);
        }

        /// <summary>
        /// Get all models list
        /// </summary>
        /// <returns>Sessions</returns>
        public List<TDto> GetAll() {
            var store = this.UnitOfWork.GetRepository<TEntity>().GetAll();
            return AutoMapper.Mapper.Map<List<TDto>>(store);
        }

        /// <summary>
        /// Save model to database
        /// </summary>
        /// <param name="model">The model</param>
        public void Save(TDto model) {
            /*var store = this.UnitOfWork.GetRepository<TEntity>().GetById(model.EntityId);

            if (store == null) {
                store = AutoMapper.Mapper.Map<TEntity>(model);
                this.UnitOfWork.GetRepository<TEntity>().Insert(store);
            } else {
                AutoMapper.Mapper.Map(model, store);
                this.UnitOfWork.GetRepository<TEntity>().Update(store);
            }

            this.UnitOfWork.SaveChanges();*/
        }

        /// <summary>
        /// Delete model from database
        /// </summary>
        /// <param name="model">The model</param>
        public void Delete(TDto model) {
            /*this.UnitOfWork.GetRepository<TEntity>().Delete(model.EntityId);
            this.UnitOfWork.SaveChanges();*/
        }

        /// <summary>
        /// Delete model from database by model Id
        /// </summary>
        /// <param name="modelId">The model Id</param>
        public void Delete(Guid modelId) {
            this.UnitOfWork.GetRepository<TEntity>().Delete(modelId);
            this.UnitOfWork.SaveChanges();
        }
    }
}
