using System;
using System.Collections.Generic;
using UnityEngine;

namespace Alter {
    public class BlockCellSlotComponent {

        SortedList<int/*index*/, CellEntity> all;
        SortedList<int/*entityID*/, CellEntity> allEntityID;
        CellEntity[] temp;

        public BlockCellSlotComponent() {
            all = new SortedList<int, CellEntity>();
            allEntityID = new SortedList<int, CellEntity>();
            temp = new CellEntity[10];
        }

        public void Add(CellEntity cell) {
            all.Add(cell.entityID, cell);
        }

        public int TakeAll(out CellEntity[] cells) {
            var count = all.Count;
            if (count > temp.Length) {
                temp = new CellEntity[(int)(count * 1.5f)];
            }
            all.Values.CopyTo(temp, 0);
            cells = temp;
            return count;
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