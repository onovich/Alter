using System;
using System.Collections.Generic;
using UnityEngine;

namespace Alter {
    public class BlockShapeComponent {

        SortedList<int/*index*/, BlockShapeModel> all;

        public BlockShapeComponent() {
            all = new SortedList<int, BlockShapeModel>();
        }

        public void Add(BlockShapeModel model) {
            all.Add(model.index, model);
        }

        public BlockShapeModel Get(int index) {
            if (all.ContainsKey(index)) {
                return all[index];
            }
            return default;
        }

        public void Clear() {
            all.Clear();
        }

    }

}