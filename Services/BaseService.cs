using Microsoft.EntityFrameworkCore;
using ProvaPub.Models;
using ProvaPub.Repository;
using ProvaPub.Repository.Interface;

namespace ProvaPub.Services;

public abstract class BaseService<T> where T : class
{
    private readonly IUnitOfWork _uow;

    public BaseService(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<ModelList<T>> List(int? page)
    {
        var repository = _uow.GetRepository<T>();
        if (!page.HasValue) page = 1;

        var pageSize = 10;

        IList<T> list = await repository.GetManyPaginated(page ?? 1, pageSize);

        var hasNext = list.Count > pageSize;

        if (hasNext)
            list.RemoveAt(pageSize);

        return new ModelList<T>() { HasNext = hasNext, TotalCount = 10, List = list };
    }
}
