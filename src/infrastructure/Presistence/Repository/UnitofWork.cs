using Presistence.Context;
using Presistence.Repository.Interface;

namespace Presistence.Repository;

public class UnitofWork : IUnitofWork
{
	private readonly DataContext _context;
	
	public UnitofWork(DataContext context)
	{
		_context = context;
	}

	public async Task SaveChangeAsync()
	{
		await _context.SaveChangesAsync();
	}

	public IRepository<T> Repository<T>() where T : class
	{
		return  new Repository<T>(_context);
	}
}
