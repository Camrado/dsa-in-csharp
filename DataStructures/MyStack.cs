using System.Collections;

namespace DataStructures;

public class MyStack <T> : IEnumerable<T> {
    private LinkedList<T> _list = new();
    
    // Create an empty stack
    public MyStack() {
    }
    
    // Create a stack with an initial element
    public MyStack(T firstElem) {
        Push(firstElem);
    }
    
    // Return the number of elements in the stack
    public int Size() {
        return _list.Count;
    }
    
    // Check if the stack is empty
    public bool IsEmpty() {
        return Size() == 0;
    }

    // Push an element on the stack
    public void Push(T elem) {
        _list.AddLast(elem);
    }
    
    // Pop an element off the stack
    public T Pop() {
        if (IsEmpty()) {
            throw new InvalidOperationException("Stack is empty");
        }

        T lastElem = _list.Last();
        _list.RemoveLast();
            
        return lastElem;
    }
    
    // Peek the top of the stack without removing an element
    public T Peek() {
        if (IsEmpty()) {
            throw new InvalidOperationException("Stack is empty");
        }

        return _list.Last();
    }
    
    public IEnumerator<T> GetEnumerator() {
        return _list.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator() {
        return GetEnumerator();
    }
}