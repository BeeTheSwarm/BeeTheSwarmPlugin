using System;
using UnityEngine;
using System.Collections;
namespace BTS {
    public class CategoryItemViewModel {
        public event Action<int> OnClick = delegate {  };
        public readonly Observable<Sprite> Image = new Observable<Sprite>();
        public string Title { get; set; }

        public int Id { get; set; }

        public void Click() {
            OnClick.Invoke(Id);
        }
    }

}