using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEditCampaignView : IControlledView
{
    void ShowAtPosition(Vector3 position);
}
