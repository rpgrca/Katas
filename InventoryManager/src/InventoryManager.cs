namespace InventoryManager.Logic;

public class InventoryManager
{
    public void UpdateQuality(Item[] items)
    {
        foreach (var item in items)
        {
            var qualityUpdater = new QualityUpdater(item);
            qualityUpdater.Update();
        }
    }
}