using UnityEngine;
using System.Collections;

namespace BTS {
    internal interface IAddBeesService: IService{
        void Execute(int count);
    }
}
