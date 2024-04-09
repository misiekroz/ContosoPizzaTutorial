using ContosoPizza.Models;
using System.Xml.Linq;

namespace ContosoPizza.Services;

public class ContosoService : IContosoService<IContosoModel>
{
    List<IContosoModel> Models { get; } = [];
    int nextId = 1;
    ContosoService()
    {
    }

    public List<IContosoModel> GetAll() => Models;

    public IContosoModel? Get(int id) => Models.FirstOrDefault<IContosoModel>(p => p.ID == id);

    public void Add(IContosoModel item)
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

    public bool Update(IContosoModel item)
    {
        var index = Models.FindIndex(p => p.ID == item.ID);
        if (index == -1)
            return false;

        Models[index] = item;

        return true;
    }
}