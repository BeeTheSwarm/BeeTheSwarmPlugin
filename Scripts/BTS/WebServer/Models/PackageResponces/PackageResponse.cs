using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class PackageResponse
{
    public abstract void Parse(Dictionary<string, object> data);
}
