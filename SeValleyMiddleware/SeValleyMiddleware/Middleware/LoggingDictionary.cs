using System.Collections;
using System.Collections.Generic;

namespace SeValleyMiddleware.Middleware
{
    public class LoggingDictionary<TKey, TValue> : IDictionary<TKey, TValue>
    {
        public LoggingDictionary(IDictionary<TKey, TValue> dictionaryImplementation)
        {
            _dictionaryImplementation = dictionaryImplementation;
        }

        private IDictionary<TKey, TValue> _dictionaryImplementation;
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return _dictionaryImplementation.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable) _dictionaryImplementation).GetEnumerator();
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            _dictionaryImplementation.Add(item);
        }

        public void Clear()
        {
            _dictionaryImplementation.Clear();
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return _dictionaryImplementation.Contains(item);
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            _dictionaryImplementation.CopyTo(array, arrayIndex);
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            return _dictionaryImplementation.Remove(item);
        }

        public int Count => _dictionaryImplementation.Count;

        public bool IsReadOnly => _dictionaryImplementation.IsReadOnly;

        public bool ContainsKey(TKey key)
        {
            return _dictionaryImplementation.ContainsKey(key);
        }

        public void Add(TKey key, TValue value)
        {
            // do my logging
            _dictionaryImplementation.Add(key, value);
        }

        public bool Remove(TKey key)
        {
            return _dictionaryImplementation.Remove(key);
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            return _dictionaryImplementation.TryGetValue(key, out value);
        }

        public TValue this[TKey key]
        {
            get => _dictionaryImplementation[key];
            set => _dictionaryImplementation[key] = value;
        }

        public ICollection<TKey> Keys => _dictionaryImplementation.Keys;

        public ICollection<TValue> Values => _dictionaryImplementation.Values;
    }
}