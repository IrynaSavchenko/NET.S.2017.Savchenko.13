using System;
using System.Collections;
using System.Collections.Generic;

namespace Collections
{
    public class Queue<T> : IEnumerable<T>
    {
        private const int DefaultSize = 100;
        private const int ResizingPower = 2;

        private T[] elements;
        private int head;
        private int tail;

        public Queue() : this(DefaultSize) { }

        public Queue(int size)
        {
            elements = new T[size];
        }

        public int Count { get; private set; }

        public void Enqueue(T item)
        {
            if (ReferenceEquals(item, null))
                throw new ArgumentNullException($"{nameof(item)} cannot be null");

            if (Count == elements.Length) Resize();

            if (tail == elements.Length - 1) tail = 0;

            elements[tail] = item;
            tail++;
            Count++;
        }

        public T Dequeue()
        {
            if (Count == 0)
                throw new InvalidOperationException("Cannot delete from an empty queue!");
            T result = elements[head];
            head++;
            Count--;

            if (head == elements.Length) head = 0;

            return result;
        }

        public IEnumerator<T> GetEnumerator() => new QueueEnumerator<T>(this);

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        internal T this[int index] => elements[index];

        private void Resize()
        {
            T[] newQueue = new T[elements.Length * ResizingPower];
            if (head < tail) Array.Copy(elements, head, newQueue, 0, tail - head);
            if (head > tail)
            {
                Array.Copy(elements, head, newQueue, 0, elements.Length - head);
                Array.Copy(elements, 0, newQueue, elements.Length - head + 1, tail);
            }

            head = 0;
            tail = Count;

            elements = newQueue;
        }
    }
}
