namespace ED262C
{
    public class SimpleArraySet<T> : ISimpleSet<T>
    {
        // Internamente, la lista contiene un array para guardar los datos
        T[] internalArray;

        // La lista lleva la cuenta de cuantos elementos hay
        int count = 0;

        // Si no especificamos, arranca el array con 4 elementos
        int defaultCapacity = 4;

        // Propiedad publica que muestra cuantos elementos hay (solo lectura)
        public int Count { get => count; }

        public bool IsEmpty => count == 0;

        public SimpleArraySet() => internalArray = new T[defaultCapacity];

        // Si no lo contiene, lo agrega al final y devuelve true
        // Si lo contiene, devuelve false
        public bool Add(T item)
        {
            if (Contains(item)) return false; 
            ValidateSize(count + 1);
            internalArray[count] = item;
            count++; return true;
        }

        public void Clear()
        {
            internalArray = new T[internalArray.Length];
            count = 0;
        }

        // Busca el elemento y devuelve true si esta
        public bool Contains(T item)
        {
            return IndexOf(item) >= 0;
        }

        public ISimpleSet<T> DifferenceWith(ISimpleSet<T> other)
        {
            throw new System.NotImplementedException();
        }

        public ISimpleSet<T> IntersectWith(ISimpleSet<T> other)
        {
            throw new System.NotImplementedException();
        }

        public bool Remove(T item)
        {
            int itemIndex = IndexOf(item);
            if (itemIndex < 0) return false;
            return true;
        }

        public T[] ToArray()
        {
            // Creo un array con la cantidad de elementos que estan ocupados en la lista
            T[] result = new T[count];

            // Copiamos uno por uno todos los elementos al nuevo array
            for (int i = 0; i < count; i++)
                result[i] = internalArray[i];

            // Devolvemos el array completo
            return result;
        }

        public ISimpleSet<T> UnionWith(ISimpleSet<T> other)
        {
            throw new System.NotImplementedException();
        }

        void ValidateSize(int nextIndex)
        {
            if (nextIndex >= internalArray.Length) Resize(nextIndex);
        }

        // Le pasamos cuantos elementos va a tener despues de agregar
        void Resize(int targetAmount)
        {
            // Guaramos el largo del array al principio
            int currentLength = internalArray.Length;

            // Vamos a duplicar ese tamaño mientras sea mas chico que la cantidad que queremos
            while (targetAmount > currentLength)
                currentLength *= 2;

            // Creamos un array del doble de largo que el actual
            T[] nextArray = new T[currentLength];

            // Copiamos todo lo que hay en el array actual al nuevo
            for (int i = 0; i < count; i++)
                nextArray[i] = internalArray[i];

            // Reemplazamos el array actual por el nuevo
            internalArray = nextArray;
        }

        int IndexOf(T item)
        {
            // Pasamos uno por uno buscando el item
            for(int i = 0; i < count; i++)
            {
                if (internalArray[i].Equals(item)) return i;
            }
            // Indices menores a 0 son invalidos
            // Usamos -1 como senial de que no estaba
            return -1;
        }
    }
}
