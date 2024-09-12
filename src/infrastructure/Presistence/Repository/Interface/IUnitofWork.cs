using Application.Repository.Interface;

namespace Presistence.Repository.Interface;

public interface IUnitofWork
{
	public Task SaveChangeAsync();
	public IRepository<T> Repository<T>() where T : class;
}
