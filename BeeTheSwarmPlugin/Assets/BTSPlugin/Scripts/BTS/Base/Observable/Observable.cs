using UnityEngine;
using System.Collections;
using System;

public sealed class Observable<DataType>
{
    private DataType m_data;
    private event Action<DataType> m_updateListener = delegate { };
    public void Set(DataType value)
    {
        m_data = value;
        m_updateListener.Invoke(m_data);
    }

    public DataType Get()
    {
        return m_data;
    }

    public void Subscribe(Action<DataType> listener)
    {
        m_updateListener += listener;
        listener.Invoke(m_data);
    }

    public void Unsubscribe(Action<DataType> listener)
    {
        m_updateListener -= listener;
    }

}
