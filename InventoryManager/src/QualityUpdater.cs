namespace InventoryManager.Logic;

public class QualityUpdater
{
    private readonly Item _item;

    public QualityUpdater(Item item) => _item = item;

    public void Update()
    {
        if (_item.Name != "Aged Brie" && _item.Name != "Backstage passes to a TAFKAL80ETC concert")
        {
            if (_item.Quality > 0)
            {
                if (_item.Name != "Sulfuras, Hand of Ragnaros")
                {
                    _item.Quality = _item.Quality - 1;
                }
            }
        }
        else
        {
            if (_item.Quality < 50)
            {
                _item.Quality = _item.Quality + 1;

                if (_item.Name == "Backstage passes to a TAFKAL80ETC concert")
                {
                    if (_item.SellIn < 11)
                    {
                        if (_item.Quality < 50)
                        {
                            _item.Quality = _item.Quality + 1;
                        }
                    }

                    if (_item.SellIn < 6)
                    {
                        if (_item.Quality < 50)
                        {
                            _item.Quality = _item.Quality + 1;
                        }
                    }
                }
            }
        }

        if (_item.Name != "Sulfuras, Hand of Ragnaros")
        {
            _item.SellIn = _item.SellIn - 1;
        }

        if (_item.SellIn < 0)
        {
            if (_item.Name != "Aged Brie")
            {
                if (_item.Name != "Backstage passes to a TAFKAL80ETC concert")
                {
                    if (_item.Quality > 0)
                    {
                        if (_item.Name != "Sulfuras, Hand of Ragnaros")
                        {
                            _item.Quality = _item.Quality - 1;
                        }
                    }
                }
                else
                {
                    _item.Quality = _item.Quality - _item.Quality;
                }
            }
            else
            {
                if (_item.Quality < 50)
                {
                    _item.Quality = _item.Quality + 1;
                }
            }
        }
    }
}