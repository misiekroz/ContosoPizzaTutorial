using ContosoPizza.Models;
using System.Xml.Linq;

namespace ContosoPizza.Services;

public class ContosoService<TModel> : IContosoService<TModel> where TModel : IContosoModel 
{
    List<TModel> Models { get; } = [];
    int nextId = 1;
    public ContosoService()
    {
    }

    public List<TModel> GetAll() => Models;

    public TModel? Get(int id) => Models.FirstOrDefault<TModel>(p => p.ID == id);

    public void Add(TModel item)
    {
        item.ID = nextId++;
        Models.Add(item);
    }

    public void Delete(int id)
    {
        var item = Get(id);
        if (item is null)
            return;

        Models.Remove(item);
    }

    public bool Update(TModel item)
    {
        var index = Models.FindIndex(p => p.ID == item.ID);
        if (index == -1)
            return false;

        Models[index] = item;

        return true;
    }
}