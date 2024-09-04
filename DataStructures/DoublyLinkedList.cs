using System.Collections;
#pragma warning disable CS0693 // Type parameter has the same name as the type parameter from outer type

namespace DataStructures;

public class DoublyLinkedList <T> : IEnumerable<T> {
    private int _size = 0;
    private Node<T>? _head = null;
    private Node<T>? _tail = null;
    
    // Internal node class to represent data
    private class Node<T>(T? value, Node<T>? previous, Node<T>? next) {
        public T? Value { get; set; } = value;
        public Node<T>? Previous { get; set; } = previous;
        public Node<T>? Next { get; set; } = next;
    }
    
    // Empty this linked list, O(n)
    public void Clear() {
        Node<T>? trav = _head;

        while (trav != null) {
            Node<T>? next = trav.Next;
            trav.Previous = trav.Next = null;
            trav.Value = default;
            trav = next;
        }

        _head = _tail = null;
        _size = 0;
    }
    
    // Return the size of this linked list
    public int Size() {
        return _size;
    }
    
    // Is this linked list empty?
    public bool IsEmpty() {
        return Size() == 0;
    }
    
    // Add an element to the tail of the linked list, O(1)
    public void Add(T elem) {
        AddLast(elem);
    }

    // Add an element to the beginning of this linked list, O(1)
    public void AddFirst(T elem) {
        if (IsEmpty()) {
            _head = _tail = new Node<T>(elem, null, null);
        }
        else {
            _head.Previous = new Node<T>(elem, null, _head);
            _head = _head.Previous;
        }

        _size++;
    }

    // Add a node to the end of the linked list, O(1)
    public void AddLast(T elem) {
        if (IsEmpty()) {
            _head = _tail = new Node<T>(elem, null, null);
        }
        else {
            _tail.Next = new Node<T>(elem, _tail, null);
            _tail = _tail.Next;
        }

        _size++;
    }
    
    // Check the value of the first node if it exists, O(1)
    public T PeekFirst() {
        if (IsEmpty()) throw new InvalidOperationException("Empty list");
        return _head.Value;
    }
    
    // Check the value of the last node if it exists, O(1)
    public T PeekLast() {
        if (IsEmpty()) throw new InvalidOperationException("Empty list");
        return _tail.Value;
    }
    
    // Remove the first value at the head of the linked list, O(1)
    public T removeFirst() {
        if (IsEmpty()) throw new InvalidOperationException("Empty list");

        T first = _head.Value;
        _head = _head.Next;
        _size--;

        if (IsEmpty()) _tail = null;
        else _head.Previous = null;

        return first;
    }

    // Remove the last value at the tail of the linked list, O(1)
    public T removeLast() {
        if (IsEmpty()) throw new InvalidOperationException("Empty list");
        
        T last = _tail.Value;
        _tail = _tail.Previous;
        _size--;
        
        if(IsEmpty()) _head = null;
        else _tail.Next = null;
        
        return last;
    }
    
    // Remove an arbitrary node from the linked list, O(1)
    private T Remove(Node<T> node) {
        if (node.Previous is null) return removeFirst();
        if (node.Next is null) return removeLast();

        node.Next.Previous = node.Previous;
        node.Previous.Next = node.Next;

        T value = node.Value;

        // Memory cleanup
        node.Value = default;
        node = node.Previous = node.Next = null;

        _size--;
        
        return value;
    }

    // Remove a node at a particular index, O(n)
    public T RemoveAt(int index) {
        if (index < 0 || index >= _size) throw new ArgumentOutOfRangeException("Index out of bounds");

        Node<T> trav;
        
        if (index < _size / 2) {
            trav = _head;
            for (int i = 0; i != index; i++) {
                trav = trav.Next;
            }
        } else {
            trav = _tail;
            for (int i = _size - 1; i != index; i--) {
                trav = trav.Previous;
            }
        }

        return Remove(trav);
    }
    
    // Remove a particular value in the linked list, O(n)
    public bool Remove(T? obj) {
        Node<T> trav = _head;
        
        // Support searching for null
        if (obj is null) {
            for (trav = _head; trav is not null; trav = trav.Next) {
                if (trav.Value is null) {
                    Remove(trav);
                    return true;
                }
            }
        } 
        // Search for non null object
        else {
            for (trav = _head; trav is not null; trav = trav.Next) {
                if (obj.Equals(trav.Value)) {
                    Remove(trav);
                    return true;
                }
            }
        }

        return false;
    }

    public int IndexOf(T? obj) {
        int index = 0;
        Node<T> trav = _head;

        // Support searching for null
        if (obj is null) {
            for (trav = _head; trav is not null; trav = trav.Next, index++) {
                if (trav.Value is null)
                    return index;
            }
        }
        // Search for non null object
        else {
            for (trav = _head; trav is not null; trav = trav.Next, index++) {
                if (obj.Equals(trav.Value))
                    return index;
            }
        }

        return -1;
    }

    public bool Contains(T? obj) {
        return IndexOf(obj) != -1;
    }

    public IEnumerator<T> GetEnumerator() {
        Node<T>? trav = _head;
        while (trav is not null) {
            yield return trav.Value;
            trav = trav.Next;
        }
    }
    
    IEnumerator IEnumerable.GetEnumerator() {
        return GetEnumerator();
    }
}