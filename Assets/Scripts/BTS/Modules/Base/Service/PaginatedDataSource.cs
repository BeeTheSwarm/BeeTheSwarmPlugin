using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class PaginatedDataSource<T> {

    public event Action OnUpdated = delegate { };
    private List<T> m_data = new List<T>();
    private Action<int, int, Action<List<T>>> m_loader; 
    private int m_totalItems = -1;
    private bool m_waitingResponse = false;
    public bool IsFullyLoaded () {
        return m_totalItems == m_data.Count;
    }

    public void LoadNext(int count) {
        m_waitingResponse = true;
        m_loader.Invoke(m_data.Count, count, (result) => {
            if (result == null) {
                // error
            } else {
                if (result.Count == 0) {
                    m_totalItems = m_data.Count;
                }
                m_data.AddRange(result);
            }
            m_waitingResponse = false;
        });
    }
}
