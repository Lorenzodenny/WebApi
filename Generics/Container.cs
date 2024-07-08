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

        // Metodo che utilizza un Func
        public List<TResult> ApplyFunction<TResult>(Func<T, TResult> func)
        {
            List<TResult> results = new List<TResult>();
            foreach (var item in _items)
            {
                results.Add(func(item));
            }
            return results;
        }

        public void AddItem(T item)
        {
            _items.Add(item);
            LogAction?.Invoke($"Aggiunto {item}");
        }

        public void UpdateItem(int index, T updatedItem)
        {
            if (index >= 0 && index < _items.Count)
            {
                _items[index] = updatedItem;
                LogAction?.Invoke($"Modificato {updatedItem}");
            }
            else
            {
                throw new IndexOutOfRangeException("Indice non valido.");
            }
        }

        public List<T> GetAllItems()
        {
            return _items;
        }

        public void RemoveItem(int index)
        {
            if (index >= 0 && index < _items.Count)
            {
                _items.RemoveAt(index);
                LogAction?.Invoke($"Rimosso: {_items[index]} all'indice {index}");
            }
            else
            {
                throw new IndexOutOfRangeException("Indice non valido.");
            }
        }

        public T GetItemById(int index)
        {
            if (index < 0 || index > _items.Count)
            {
                throw new IndexOutOfRangeException("L'indice inserito non è esistente");
            }

            return _items[index];
        }
    }
}
