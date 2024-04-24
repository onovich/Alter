using System;
using System.Collections.Generic;
using UnityEngine;

namespace Alter {
    public struct BlockShapeModel {

        public int index;
        public Vector2Int[] shape;
        public Vector2Int sizeInt;
        public Vector2 centerFloat;

        public void ForEachCell(Action<Vector2Int> action) {
            foreach (var cell in shape) {
                action(cell);
            }
        }

    }

}