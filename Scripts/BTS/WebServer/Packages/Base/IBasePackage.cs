using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface IBasePackage {
    bool AuthenticationRequired { get;}
    string Id { get; }
    BTS_Error Error { get; }

    Dictionary<string, object> GenerateData();
    void ParseResponse(string text);
    void HandleResponce();
}
