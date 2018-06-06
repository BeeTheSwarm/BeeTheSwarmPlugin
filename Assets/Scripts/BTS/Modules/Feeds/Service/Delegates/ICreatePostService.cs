using UnityEngine;
using System.Collections;
using BTS;
using System;
using System.Collections.Generic;

namespace BTS {
    internal interface ICreatePostService : IService {
        void Execute(string title, string description, Texture2D image, Action<bool> callback);

    }
}
