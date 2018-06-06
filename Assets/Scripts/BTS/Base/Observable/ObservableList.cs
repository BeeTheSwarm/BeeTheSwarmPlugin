using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ObservableList<DataType>
{
    private List<DataType> m_list = new List<DataType>();

    public event Action OnClear = delegate { };

    public event Action<DataType> OnInsert = delegate { };
    public event Action<DataType> OnAdd = delegate { };
    public event Action<DataType> OnRemove = delegate { };
    public event Action<int> OnCountChanged = delegate { };
    
    public int Count()
    {
        return m_list.Count;
    }

    public List<DataType> Get()
    {
        return m_list.GetRange(0, Count());
    }

    public void Remove(DataType item)
    {
        m_list.Remove(item);
        OnRemove.Invoke(item);
        OnCountChanged.Invoke(Count());
    }
    
    public void Add(DataType item)
    {
        m_list.Add(item);
        OnAdd.Invoke(item);
        OnCountChanged.Invoke(Count());
    }

    public void Insert(DataType item) {
        m_list.Insert(0, item);
        OnInsert.Invoke(item);
        OnCountChanged.Invoke(Count());
    }

    public void Clear()
    {
        m_list.Clear();
        OnClear.Invoke();
        OnCountChanged.Invoke(Count());
    }
    
}
