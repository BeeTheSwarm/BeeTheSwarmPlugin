﻿using UnityEngine;
using System.Collections;
using BTS;
using System;
using System.Collections.Generic;

namespace BTS {
    internal interface ICreateCommentService : IService {
        void Execute(int postId, string text);
    }
}
