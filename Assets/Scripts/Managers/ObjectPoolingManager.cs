using System.Collections.Generic;

public class ObjectPoolingManager : BaseManager<ObjectPoolingManager>
{
    private List<ObjectPoolingController> objectPoolings = new List<ObjectPoolingController>();
    
    public void AddPoolController(ObjectPoolingController objectPoolingController)
    {
        objectPoolings.Add(objectPoolingController);
    }
    //todo do not return main menu level buttons
    public void ReturnAllToPools()
    {
        objectPoolings.ForEach(objectPoolingController => objectPoolingController.ReturnAllToPools());
    }
}
