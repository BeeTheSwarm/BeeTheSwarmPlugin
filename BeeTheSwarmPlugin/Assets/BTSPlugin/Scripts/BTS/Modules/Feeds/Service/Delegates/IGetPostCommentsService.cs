using UnityEngine;
using System.Collections;
using BTS;
using System;
using System.Collections.Generic;

namespace BTS {
    internal interface IGetPostCommentsService : IService {
        void Execute(int postId, int limit, int offset, Action<List<CommentModel>, int> callback);
    }
}
