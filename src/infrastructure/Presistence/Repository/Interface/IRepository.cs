namespace Application.Repository.Interface;

public interface IRepository<T> where T : class
{
	public Task CreateAsync(T entity);
	public void UpdateAsync(T entity);
	public Task<T> GetAsync(Guid id);
	public Task<IEnumerable<T>> GetAllAsync();
	public void DeleteAsync(T entity);
}