using UnityEngine;
using System.Collections;
namespace BTS {
    public class RequestsScreenViewModel {
        public readonly ObservableList<RequestItemViewModel> Requests = new ObservableList<RequestItemViewModel>();

        public void RemoveRequest(InvitationModel obj) {
            var viewModel = Requests.Get().Find(r => r.Id == obj.Id);
            if (viewModel != null) {
                Requests.Remove(viewModel);
            }
        }
    }
}