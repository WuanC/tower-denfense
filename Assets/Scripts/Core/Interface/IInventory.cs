using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInventory<T>
{
    void AddItem(T item);
    bool RemoveItem(T item);
    bool ContainsItem(T item);
    List<T> GetItems();
    void Clear();

}
