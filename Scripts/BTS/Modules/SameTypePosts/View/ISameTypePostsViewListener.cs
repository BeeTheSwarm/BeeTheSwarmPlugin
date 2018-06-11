using UnityEngine;
using System.Collections;
namespace BTS
{
    public interface ISameTypePostsViewListener : IViewEventListener {
        void OnScrolledToEnd();
    }
}