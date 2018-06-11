using UnityEngine;
using UnityEditor;

public interface IViewController: IInjectTarget  {
    void SetView(IView view);
}