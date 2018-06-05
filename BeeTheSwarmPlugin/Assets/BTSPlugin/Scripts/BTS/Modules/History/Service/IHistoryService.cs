using UnityEngine;
using System.Collections;

public interface IHistoryService : IService
{
    void AddItem(IScreenController controller);
    void BackPressedItem(IScreenController controller);
}
