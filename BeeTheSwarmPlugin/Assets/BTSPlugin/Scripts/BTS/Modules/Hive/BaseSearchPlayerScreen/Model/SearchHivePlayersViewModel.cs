using UnityEngine;
using System.Collections;
namespace BTS {

    internal class SearchHivePlayersViewModel {
        public readonly ObservableList<UserViewModel> SearchResult = new ObservableList<UserViewModel>();
    }
}