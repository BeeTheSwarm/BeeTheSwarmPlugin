using UnityEngine;
using System.Collections;
using System;

public interface IPostLoaderCommand : ICommand {
    void Execute(int offset, int limit, Action callback);
}
