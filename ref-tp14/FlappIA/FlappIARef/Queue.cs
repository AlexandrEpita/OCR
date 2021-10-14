using System;
using System.Collections;

namespace tp14Ref
{
    public class Queue<T> : IEnumerable
    {
        private Node _head;
        private Node _tail;

        public class Node
        {
            public readonly T Data;
            public Node Next;

            public Node(T data)
            {
                Data = data;
                Next = null;
            }
        }
        
        public Queue()
        {
            _head = null;
            _tail = null;
        }

        /// <summary>
        ///     Get an enumerator to use foreach on the list
        /// </summary>
        /// <returns>An enumerator on all the elements</returns>
        public IEnumerator GetEnumerator()
        {
            for (var current = _head; current != null; current = current.Next) 
                yield return current.Data;
        }

        /// <summary>
        ///     Get the element at the front of the list
        /// </summary>
        /// <returns>The element at the front</returns>
        public T PeekFront()
        {
            if (_head == null) 
                throw new ArgumentOutOfRangeException();

            return _head.Data;
        }
        
        /// <summary>
        ///     Get the second element at the front of the list
        /// </summary>
        /// <returns>The element at the front</returns>
        public T PeekSecond()
        {
            if (_head?.Next == null) 
                throw new ArgumentOutOfRangeException();

            return _head.Next.Data;
        }

        /// <summary>
        ///     Get the element at the back of the list
        /// </summary>
        /// <returns>The element at the back</returns>
        public T PeekBack()
        {
            if (_tail == null) 
                throw new ArgumentOutOfRangeException();

            return _tail.Data;
        }

        /// <summary>
        ///     Remove the element at the front
        /// </summary>
        public void PopFront()
        {
            if (_head == null)
                throw new ArgumentOutOfRangeException();

            _head = _head.Next;
            if (_head == null)
                _tail = null;
        }

        /// <summary>
        ///     Add a new element to the back
        /// </summary>
        /// <param name="element">The element to add</param>
        public void PushBack(T element)
        {
            var newNode = new Node(element);
            
            if (_tail != null)
                _tail.Next = newNode;
            else
                _head = newNode;

            _tail = newNode;
        }

        /// <summary>
        ///     Check if the queue is empty
        /// </summary>
        /// <returns></returns>
        public bool IsEmpty()
        {
            return _head == null;
        }
    }
}