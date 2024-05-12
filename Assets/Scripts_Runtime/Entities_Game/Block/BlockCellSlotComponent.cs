using System;
using System.Collections.Generic;
using UnityEngine;

namespace Alter {
    public class BlockCellSlotComponent {

        SortedList<int/*index*/, CellEntity> all;
        CellEntity[] temp;

        public BlockCellSlotComponent() {
            all = new SortedList<int, CellEntity>();
            temp = new CellEntity[10];
        }

        public void Add(CellEntity cell) {
            all.Add(cell.index, cell);
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

        public void Clear() {
            all.Clear();
        }

    }

}