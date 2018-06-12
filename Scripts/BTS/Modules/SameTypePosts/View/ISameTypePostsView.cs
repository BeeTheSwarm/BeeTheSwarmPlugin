using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace BTS
{
    public interface ISameTypePostsView : ITopPanelContainer
    {
        IPostlistContainer GetPostlistContainer();
    }
}