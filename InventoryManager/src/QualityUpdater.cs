namespace InventoryManager.Logic;

public class QualityUpdater
{
    private readonly Item _item;

    public QualityUpdater(Item item) => _item = item;

    public void Update()
    {
        if (_item.Name == "Sulfuras, Hand of Ragnaros")
        {
            return;
        }

        if (_item.Name != "Aged Brie" && _item.Name != "Backstage passes to a TAFKAL80ETC concert")
        {
            if (CanDecreaseQuality())
            {
                DecreaseQuality();
            }
        }
        else
        {
            if (CanIncrementQuality())
            {
                IncreaseQuality();

                if (_item.Name == "Backstage passes to a TAFKAL80ETC concert")
                {
                    if (_item.SellIn < 11)
                    {
                        if (CanIncrementQuality())
                        {
                            IncreaseQuality();
                        }
                    }

                    if (_item.SellIn < 6)
                    {
                        if (CanIncrementQuality())
                        {
                            IncreaseQuality();
                        }
                    }
                }
            }
        }

        DecreaseSellIn();

        if (Expired())
        {
            if (_item.Name != "Aged Brie")
            {
                if (_item.Name != "Backstage passes to a TAFKAL80ETC concert")
                {
                    if (CanDecreaseQuality())
                    {
                        DecreaseQuality();
                    }
                }
                else
                {
                    ResetQuality();

                }
            }
            else
            {
                if (CanIncrementQuality())
                {
                    IncreaseQuality();
                }
            }
        }

    }
    private void IncreaseQuality() => _item.Quality += 1;

    private void DecreaseQuality() => _item.Quality -= 1;

    private void ResetQuality() => _item.Quality = 0;

    private bool CanIncrementQuality() => _item.Quality < 50;

    private bool CanDecreaseQuality() => _item.Quality > 0;

    private void DecreaseSellIn() => _item.SellIn -= 1;

    private bool Expired() => _item.SellIn < 0;
}