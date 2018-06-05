using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BTS_Error
{

    public string Description { get; private set; }
    public bool Status { get; private set; }
    public int Code { get; private set; }

    public BTS_Error()
    {
        Description = string.Empty;
        Status = false;
        Code = 0;
    }

    public void ParseJSON(Dictionary<string, object> data)
    {
        Status = (bool)data["status"];
        if (Status)
        {
            Description = data["message"].ToString();
            Code = int.Parse(data["code"].ToString());
        }
    }
}
