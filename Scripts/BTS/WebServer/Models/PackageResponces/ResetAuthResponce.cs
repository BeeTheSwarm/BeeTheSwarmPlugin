using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ResetAuthResponce: PackageResponse
{
    public string AuthToken { get; private set; }
    public override void Parse(Dictionary<string, object> data)
    {
        AuthToken = data["auth_token"].ToString();
    }
}
