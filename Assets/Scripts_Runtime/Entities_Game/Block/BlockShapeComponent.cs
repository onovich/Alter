using System;
using System.Collections.Generic;
using UnityEngine;

namespace Alter {
    public struct BlockShapeComponent {

        public Vector2Int[] shape;
        public Vector2Int sizeInt;
        public Vector2 centerFloat;

        public void ForEachCell(Action<Vector2Int> action) {
            foreach (var cell in shape) {
                action(cell);
            }
        }

        public int TakeNextShape(out Vector2Int[] nextShape) {
            var len = GridUtils.GetRotateGridsWithoutGC(shape, centerFloat, 90, out nextShape);
            return len;
        }

        public Vector2Int GetNextSize() {
            return new Vector2Int(sizeInt.y, sizeInt.x);
        }

        public Vector2Int[] TurnToNextShape() {
            var len = GridUtils.GetRotateGridsWithoutGC(shape, centerFloat, 90, out var rotatedPoints);
            Array.Copy(rotatedPoints, shape, len);
            sizeInt = new Vector2Int(sizeInt.y, sizeInt.x);
            return shape;
        }

    }

}