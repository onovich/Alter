using System;
using System.Collections.Generic;
using UnityEngine;

namespace Alter {
    public class BlockCellSlotComponent {

        SortedList<int/*index*/, CellEntity> all;
        SortedList<int/*entityID*/, CellEntity> allEntityID;

        public BlockCellSlotComponent() {
            all = new SortedList<int, CellEntity>();
            allEntityID = new SortedList<int, CellEntity>();
        }

        public void Add(CellEntity cell) {
            all.Add(cell.entityID, cell);
        }

        public CellEntity Get(int index) {
            if (all.ContainsKey(index)) {
                return all[index];
            }
            return null;
        }

        public bool TryGetByEntityID(int entityID, out CellEntity cell) {
            return all.TryGetValue(entityID, out cell);
        }

        public void Clear() {
            all.Clear();
            allEntityID.Clear();
        }

    }

}