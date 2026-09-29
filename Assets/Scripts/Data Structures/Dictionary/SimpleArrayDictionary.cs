using System;
using System.Collections.Generic;

namespace ED262C
{
    public class SimpleArrayDictionary<TKey, TValue> : ISimpleDictionary<TKey, TValue>
    {
        KeyValuePair<TKey, TValue>[] internalArray;
        int defaultCapacity = 4;
        int count = 0;

        public SimpleArrayDictionary()
        {
            internalArray = new KeyValuePair<TKey, TValue>[defaultCapacity];
        }
        public TValue this[TKey key] { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

        public int Count => count;

        public bool IsEmpty => count == 0;

        public void Add(TKey key, TValue value)
        {
            if (ContainsKey(key))
            {
                throw new ArgumentException("Key is already in Dictionary");
            }
            ExecuteAdd(key, value);
        }

        public void Clear()
        {
            internalArray = new KeyValuePair<TKey, TValue>[count];
            count = 0;
        }
        // Si tiene un indice valido, existe
        public bool ContainsKey(TKey key)
        {
            if(key == null)
            {
                throw new ArgumentNullException("Key is null");
            }
            return IndexOf(key) >= 0;
        }

        public TKey[] Keys()
        {
            // En vez de array de T, es array de TKey
            TKey[] result = new TKey[count];

            // Accedemos al par y usamos .Key
            for (int i = 0; i < count; i++)
                result[i] = internalArray[i].Key;

            // Devolvemos el array completo
            return result;
        }

        public bool Remove(TKey key)
        {
            throw new System.NotImplementedException();
        }

        public bool TryAdd(TKey key, TValue value)
        {
            throw new System.NotImplementedException();
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            throw new System.NotImplementedException();
        }

        public TValue[] Values()
        {
            // En vez de array de T, es array de TValue
            TValue[] result = new TValue[count];

            // Accedemos al par y usamos .Value
            for (int i = 0; i < count; i++)
                result[i] = internalArray[i].Value;

            // Devolvemos el array completo
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
            KeyValuePair<TKey, TValue>[] nextArray = new KeyValuePair<TKey, TValue>[currentLength];

            // Copiamos todo lo que hay en el array actual al nuevo
            for (int i = 0; i < count; i++)
                nextArray[i] = internalArray[i];

            // Reemplazamos el array actual por el nuevo
            internalArray = nextArray;
        }

        int IndexOf(TKey key)
        {
            // Pasamos uno por uno buscando el item
            for (int i = 0; i < count; i++)
            {
                // Si esta, devolvemos el indice donde esta
                if (internalArray[i].Key.Equals(key)) return i;
            }
            // Indices menores a 0 son invalidos
            // Usamos -1 como senial de que no estaba
            return -1;
        }
        // Esta funcion adume que ya fueron hechos los chequeos necesarios
        // La tienen en comun el indexer (set), Add y TryAdd
        void ExecuteAdd(TKey key, TValue value)
        {
            ValidateSize(count);
            internalArray[count] = new KeyValuePair<TKey, TValue>(key, value);
            count++;
        }
    }
}
