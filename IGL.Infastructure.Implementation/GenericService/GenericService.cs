using IGL.Core.Comman.Comman;
using IGL.Core.Repository.GenericRepository;
using IGL.Core.Service.GenericService;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace IGL.Infastructure.Service.GenericService
{
    public class GenericService<TEntity, T> : IGenericService<TEntity, T> where TEntity : class
    {
        private readonly IGenericRepository<TEntity, T> _IGenericRepository;

        public GenericService(IGenericRepository<TEntity, T> _GenericRepository)
        {
            _IGenericRepository = _GenericRepository;
        }
        public async Task<ResponseMessage> Add(params TEntity[] items)
        {
            return await _IGenericRepository.Add(items);
        }

        public async  Task<ResponseMessage> CreateEntity(TEntity model)
        {
            return await  _IGenericRepository.CreateEntity(model);
        }

        public async Task<bool> CreateNewContext()
        {
            return await _IGenericRepository.CreateNewContext();
        }

        public async  Task<ResponseMessage> Delete(params TEntity[] items)
        {
            return await _IGenericRepository.Delete(items);
        }

        public Task<ResponseMessage> DeleteSingle(T id)
        {
            throw new NotImplementedException();
        }

        public async   Task<IList<TEntity>> GetAll(params Expression<Func<TEntity, object>>[] navigationProperties)
        {
            return await _IGenericRepository.GetAll(navigationProperties);
        }

        public async  Task<IList<TEntity>> GetList(Func<TEntity, bool> where, params Expression<Func<TEntity, object>>[] navigationProperties)
        {
            return await _IGenericRepository.GetList(where, navigationProperties);
        }

        public async  Task<TEntity> GetSingle(Func<TEntity, bool> where, params Expression<Func<TEntity, object>>[] navigationProperties)
        {
            return await _IGenericRepository.GetSingle(where, navigationProperties);
        }

        public async  Task<ResponseMessage> Update(params TEntity[] items)
        {
            return await _IGenericRepository.Update(items);
        }
    }
}
