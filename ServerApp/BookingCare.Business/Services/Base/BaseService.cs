using BookingCare.Data.Infrastructure;
using Microsoft.Extensions.Logging;

namespace BookingCare.Business.Services.Base
{
    public class BaseService<T> : IBaseService<T> where T : class
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly ILogger<BaseService<T>> _logger;
        public BaseService(ILogger<BaseService<T>> logger,
            IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task<int> AddAsync(T entity)
        {
            if (entity != null)
            {
                _unitOfWork.GenericRepository<T>().Add(entity);
                return await _unitOfWork.SaveChangesAsync();
            }

            _logger.LogError("Entity is null!");
            throw new ArgumentNullException(nameof(entity));
        }

        public virtual bool Delete(int id)
        {
            _unitOfWork.GenericRepository<T>().Delete(id);

            return _unitOfWork.SaveChanges() > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            _unitOfWork.GenericRepository<T>().Delete(id);

            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(T entity)
        {
            _unitOfWork.GenericRepository<T>().Delete(entity);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _unitOfWork.GenericRepository<T>().GetAllAsync();
        }

        //public Task<PaginatedResult<T>> GetAsync(Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, string includeProperties = "", int pageIndex = 1, int pageSize = 10)
        //{
        //    throw new NotImplementedException();
        //}

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _unitOfWork.GenericRepository<T>().GetByIdAsync(id);
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            _unitOfWork.GenericRepository<T>().Update(entity);

            return await _unitOfWork.SaveChangesAsync() > 0;
        }
    }
}
