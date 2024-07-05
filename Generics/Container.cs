using WebApi.Abstract;

namespace WebApi.Generics
{
    public class Container<T>
    {
        private List<T> _items = new List<T>();

        // Delegato Action, usato per il messaggio d'informazione
        public Action<string> LogAction { get; set; }

        // filtra e crea una seconda lista con i film che matchano
        public List<T> FindFilms(Predicate<T> match)
        {
            return _items.FindAll(match);
        }


        public void AddItem(T item)
        {
            _items.Add(item);
            LogAction?.Invoke($"Aggiunto {item}");
        }

        public List<T> GetAllItems()
        {
            return _items;
        }

        public void RemoveItem(T item)
        {
            _items.Remove(item);
            LogAction?.Invoke($"Rimosso: {item}");
        }

        public T GetItemById(int id)
        {
            if (id < 0 || id > _items.Count)
            {
                throw new IndexOutOfRangeException("L'indice inserito non è esistente");
            }

            return _items[id];
        }
    }
}
