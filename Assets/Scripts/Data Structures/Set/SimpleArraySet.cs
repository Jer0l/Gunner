using UnityEngine.Rendering;

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

        public SimpleArraySet(ISimpleSet<T> original)
        {
            // Convertimos el Set a un array
            T[] originalArray = original.ToArray();
            // Inicializamos el array interno con el mismo largo que el original
            internalArray = new T[originalArray.Length];
            // Copiamos todos los elementos de uno a otro
            for (int i = 0; i < originalArray.Length; i++)
            {
                internalArray[i] = originalArray[i];
            }
            // Inicializamos count con la cantidad de elementos del array original
            count = originalArray.Length;
        }

        // Si no lo contiene, lo agrega al final y devuelve true
        // Si lo contiene, devuelve false
        public bool Add(T item)
        {
            if (Contains(item)) return false;
            // Antes de agregar, verifica que haya espacio y resizea de ser necesario
            ValidateSize(count);
            internalArray[count] = item;
            count++;
            return true;
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

        // Devuelve un nuevo Set con todos los elementos de este Set que NO estan en el otro
        public ISimpleSet<T> DifferenceWith(ISimpleSet<T> other)
        {
            // Arrancamos con un Set vacio
            ISimpleSet<T> result = new SimpleArraySet<T>();
            // Recorremos nuestro array (garantiza que esten los elementos de este Set)
            for (int i = 0; i < count; i++)
            {
                // Solo agregamos si el otro set NO lo contiene
                if (!other.Contains(internalArray[i]))
                {
                    result.Add(internalArray[i]);
                }
            }
            // Devolvemos el nuevo set
            return result;
        }

        // Devuelve un Set con los elementos en comun
        public ISimpleSet<T> IntersectWith(ISimpleSet<T> other)
        {
            // Arrancamos con un Set vacio
            ISimpleSet<T> result = new SimpleArraySet<T>();
            // Recorremos nuestro array (garantiza que esten los elementos de este Set)
            for (int i = 0; i < count; i++)
            {
                // Solo agregamos si el otro set tambien lo contiene
                if (other.Contains(internalArray[i]))
                {
                    result.Add(internalArray[i]);
                }
            }
            // Devolvemos el nuevo set
            return result;
        }

        public bool Remove(T item)
        {
            // Si NO lo contiene, no lo puede remover y devuelve false
            int itemIndex = IndexOf(item);
            if (itemIndex < 0) return false;
            // A diferencia de List o Queue, no se respeta el orden,
            // Por lo cual no usamos shiftLeft, cuya complejidad es 0(n)
            // Mover un solo elemento tiene complejidad O(1)

            // Si lo contiene, va al indice del elemento y lo remueve
            // Pisamos el indice a remover con el ultimo elemento
            // Salvo que el elemento a remover sea el ultimo
            if (itemIndex != count - 1)
            {
                internalArray[itemIndex] = internalArray[count - 1];
            }
            // Defaulteamos el ultimo indice en cualquier caso:
            // Si era el ultimo, lo borramos
            // Si no era el ultimo, lo borramos para que no quede duplicado
            internalArray[count - 1] = default;
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

        // Devuelve un Set con todos los elementos de ambos sets
        public ISimpleSet<T> UnionWith(ISimpleSet<T> other)
        {
            // Partimos de una copia del otro Set
            ISimpleSet<T> result = new SimpleArraySet<T>(other);
            // Agregamos todos los elementos de este Set
            // Si estan repetidos, Add ya los filtra (no llamamos a Contains)
            for (int i = 0; i < count; i++)
            {
                result.Add(internalArray[i]);
            }
            // Devolvemos el nuevo set
            return result;
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
            for (int i = 0; i < count; i++)
            {
                if (internalArray[i].Equals(item)) return i;
            }
            // Indices menores a 0 son invalidos
            // Usamos -1 como senial de que no estaba
            return -1;
        }
    }
}