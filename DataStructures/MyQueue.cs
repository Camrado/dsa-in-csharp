using System.Collections;

namespace DataStructures;

public class MyQueue<T> : IEnumerable<T> {
    private LinkedList<T> _list = new();
    
    public MyQueue() {
    }
    
    public MyQueue(T firstElem) {
        Enqueue(firstElem);
    }
    
    public int Size() {
        return _list.Count;
    }
    
    public bool IsEmpty() {
        return Size() == 0;
    }
    
    public T Peek() {
        if (IsEmpty()) {
            throw new InvalidOperationException("Queue is empty");
        }

        return _list.First();
    }

    public T Dequeue() {
        if (IsEmpty()) 
            throw new InvalidOperationException("Queue is empty");

        T first = _list.First();
        _list.RemoveFirst();
        
        return first;
    }
    
    public void Enqueue(T elem) {
        _list.AddLast(elem);
    }
    
    public IEnumerator<T> GetEnumerator() {
        return _list.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator() {
        return GetEnumerator();
    }
}