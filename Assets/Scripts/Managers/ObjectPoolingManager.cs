using System.Collections.Generic;
using System.Linq;

public class ObjectPoolingManager : BaseManager<ObjectPoolingManager>
{
    private List<ObjectPoolingController> objectPoolings = new List<ObjectPoolingController>();
    
    public void AddPoolController(ObjectPoolingController objectPoolingController)
    {
        objectPoolings.Add(objectPoolingController);
    }

    public void ReturnAllToPools()
    {
        foreach (var objectPoolingController in objectPoolings.Where(x => x.IsGameplayPooling))
        {
            objectPoolingController.ReturnAllToPools();
        }
    }
}
