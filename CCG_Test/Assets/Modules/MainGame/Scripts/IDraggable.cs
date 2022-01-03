using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MainGame {
    public interface IDraggable {
        void OnDrag();
        void OnBeginDrag();
        void OnEndDrag();
       
        RectTransform RectTransform { get; }
    }
}
