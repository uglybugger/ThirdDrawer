using System;

namespace ThirdDrawer
{
    public class ThreadSafeLazy<T>
    {
        private readonly Func<T> _initialize;
        private bool _hasValue;
        private T _value;
        private readonly object _mutex = new object();

        public ThreadSafeLazy(Func<T> initialize)
        {
            _initialize = initialize;
        }

        public bool HasValue
        {
            get { return _hasValue; }
        }

        public T Value
        {
            get
            {
                if (HasValue) return _value;
                lock (_mutex)
                {
                    if (HasValue) return _value;

                    _value = _initialize();
                    _hasValue = true;
                }
                return _value;
            }
        }

        public static implicit operator T(ThreadSafeLazy<T> lazy)
        {
            return lazy.Value;
        }
    }
}