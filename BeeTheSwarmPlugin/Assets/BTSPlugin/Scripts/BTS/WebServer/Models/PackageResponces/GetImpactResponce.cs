using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GetImpactResponce : PackageResponse
{
    public float Impact { get; private set; }
    public override void Parse(Dictionary<string, object> data)
    {
        Impact = float.Parse(data["impact"].ToString());
    }
}
