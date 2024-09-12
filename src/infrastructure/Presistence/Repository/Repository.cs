using Application.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using Presistence.Context;

namespace Presistence.Repository;

public class Repository<T> : IRepository<T> where T : class
{
	private readonly DataContext _context;
	private readonly DbSet<T> _set;
	
	public Repository(DataContext context)
	{
		_context = context;
		_set = _context.Set<T>();
	}
	
	public async Task CreateAsync(T entity)
	{
		await _set.AddAsync(entity);
	}

	public void Delete(T entity)
	{
		_set.Remove(entity);
	}

	public async Task<IEnumerable<T>> GetAllAsync()
	{
		return await _set.ToListAsync();
	}

	public async Task<T> GetAsync(Guid id)
	{
		return await _set.FindAsync(id);
	}

	public void Update(T entity)
	{
		_set.Update(entity);
	}
}
