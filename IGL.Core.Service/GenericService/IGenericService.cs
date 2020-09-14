using IGL.Core.Comman.Comman;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IGL.Core.Service.GenericService
{
    public interface IGenericService<TEntity, T> where TEntity : class
    {
        Task<IList<TEntity>> GetAll(params Expression<Func<TEntity, object>>[] navigationProperties);
        Task<IList<TEntity>> GetList(Func<TEntity, bool> where, params Expression<Func<TEntity, object>>[] navigationProperties);
        Task<TEntity> GetSingle(Func<TEntity, bool> where, params Expression<Func<TEntity, object>>[] navigationProperties);
        Task<ResponseMessage> Add(params TEntity[] items);
        Task<ResponseMessage> Update(params TEntity[] items);
        Task<ResponseMessage> Delete(params TEntity[] items);
        Task<ResponseMessage> CreateEntity(TEntity model);
        Task<ResponseMessage> DeleteSingle(T id);
        Task<bool> CreateNewContext();
    }
}
