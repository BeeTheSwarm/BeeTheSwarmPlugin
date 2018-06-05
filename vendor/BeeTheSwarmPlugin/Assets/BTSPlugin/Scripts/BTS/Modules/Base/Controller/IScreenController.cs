using UnityEngine;
using System.Collections;

public interface IScreenController : IViewController
{
    bool StoredInHistory { get; }
    void Show();
    void Hide();
    void Restore();
    void OnBackPressed();
}
