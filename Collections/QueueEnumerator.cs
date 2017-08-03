using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Collections
{
    internal class QueueEnumerator<T> : IEnumerator<T>
    {
        private const int EmptyIndex = -1;

        private Queue<T> queue;
        private int currentIndex;

        /// <summary>
        /// Initializes a queue enumerator
        /// </summary>
        /// <param name="queue"></param>
        public QueueEnumerator(Queue<T> queue)
        {
            this.queue = queue;
            currentIndex = EmptyIndex;
        }

        /// <summary>
        /// Returns the current element
        /// </summary>
        public T Current
        {
            get
            {
                if(currentIndex < 0 || currentIndex >= queue.Count())
                    throw new IndexOutOfRangeException($"Index {nameof(currentIndex)} is out of range!");

                return queue[currentIndex];
            }
        }

        object IEnumerator.Current => Current;

        public void Dispose() { }

        /// <summary>
        /// Checks if it is possible to move to the next element
        /// </summary>
        /// <returns>True if the current element is not the last one, false otherwise</returns>
        public bool MoveNext() => ++currentIndex < queue.Count;

        public void Reset() => throw new NotSupportedException();
    }
}
