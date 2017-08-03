using System;
using System.Collections;
using System.Collections.Generic;

namespace Collections
{
    /// <summary>
    /// Works with the Queue structure
    /// </summary>
    /// <typeparam name="T">Type of elements in the queue</typeparam>
    public class Queue<T> : IEnumerable<T>
    {
        private const int DefaultSize = 100;
        private const int ResizingPower = 2;

        private T[] elements;
        private int head;
        private int tail;

        #region Constructors

        /// <summary>
        /// Initializes the queue with a default size
        /// </summary>
        public Queue() : this(DefaultSize) { }

        /// <summary>
        /// Initializes the queue with the specified size
        /// </summary>
        /// <param name="size">Queue size</param>
        public Queue(int size)
        {
            elements = new T[size];
        }

        #endregion

        /// <summary>
        /// Current number of elements in the queue
        /// </summary>
        public int Count { get; private set; }

        /// <summary>
        /// Adds the element to the queue
        /// </summary>
        /// <param name="item">Element to add</param>
        public void Enqueue(T item)
        {
            if (ReferenceEquals(item, null))
                throw new ArgumentNullException($"{nameof(item)} cannot be null!");

            if (Count == elements.Length) Resize();

            if (tail == elements.Length - 1) tail = 0;

            elements[tail] = item;
            tail++;
            Count++;
        }

        /// <summary>
        /// Removes an element from the beginning of the queue
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Returns a queue enumerator
        /// </summary>
        /// <returns>Queue enumerator</returns>
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
