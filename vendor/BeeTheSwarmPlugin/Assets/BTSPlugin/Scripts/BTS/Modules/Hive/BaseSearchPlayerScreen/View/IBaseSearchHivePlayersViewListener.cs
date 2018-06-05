using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BTS {

    internal interface ISearchHivePlayersViewListener : IViewEventListener {
        void SearchFieldChanged(string text);
        void OnScrolledToTheEnd();
        void SearchFieldEndEditHandler(string text);
    }
}
