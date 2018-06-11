using UnityEngine;
using System.Collections;
namespace BTS
{
    public interface ISameTypePostsController: IScreenController
    {
        void Show(PostTypes type);
    }
}