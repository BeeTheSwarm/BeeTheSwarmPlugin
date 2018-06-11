using UnityEngine;
using System.Collections;
namespace BTS {
    public class ErrorPopupItemModel : PopupItemModel {
        public override PopupTypes Type {
            get {
                return PopupTypes.Error;
            }
        }
        public string Message { get; private set; }
        public ErrorPopupItemModel(string message) {
            Message = message;
        }

        public bool Equals(ErrorPopupItemModel obj) {
            return Message.Equals(obj.Message);
        }
    }
}