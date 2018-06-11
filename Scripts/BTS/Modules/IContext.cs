using UnityEngine;
using System.Collections;
using System;

public interface IContext
{
    event Action OnInited;
    void Show();
    void Hide();
    void StartCoroutine(IEnumerator courutine); 
    T CreateCommand<T>() where T: ICommand, new();
}
