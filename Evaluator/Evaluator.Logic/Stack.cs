using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evaluator.Logic
{
    public class Stack
    {

        private T[] _stack;
        private int _stop;
        

        public Stack(int n)
        {
            _N = n;
            _stack = new T[n];  
        }

        public int N { get; }

        public bool IsEmpty() => _stop == 0;

        public bool IsFull() => _stop == _N;

        public void Push(T item)
        { 
           {
            if (IsFull())
                throw new Exception("Stack full.");
           }
        
        

           _stack[_stop] = item;
           _stop++;
            
         }
            public T Pop()
        {
            if (IsEmpty)
            {  
                throw new Exception("Stack empty.");
            
            }
            var item = _stack[_stop];
            _stop--;
            return item;

        }














        public QueueUsingArray(int capacity)
        {
            elements = new T[capacity];
            front = 0;
            rear = -1;
            count = 0;
        }

        public void Enqueue(T item)
        {
            if (count == elements.Length)
                throw new InvalidOperationException("Queue is full.");
            rear = (rear + 1) % elements.Length;
            elements[rear] = item;
            count++;
        }

        public T Dequeue()
        {
            if (count == 0)
                throw new InvalidOperationException("Queue is empty.");
            T item = elements[front];
            front = (front + 1) % elements.Length;
            count--;
            return item;
        }

        public T Peek()
        {
            if (count == 0)
                throw new InvalidOperationException("Queue is empty.");
            return elements[front];
        }
    }
}
}
