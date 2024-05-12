using System;
using System.Collections.Generic;
using UnityEngine;

namespace Alter {

    public class CellRepository {

        Dictionary<int, CellEntity> all;
        Dictionary<Vector2Int, CellEntity> posMap;
        SortedList<int, Queue<CellEntity>> clearingTaskQueue;
        CellEntity[] temp;

        public CellRepository() {
            all = new Dictionary<int, CellEntity>();
            posMap = new Dictionary<Vector2Int, CellEntity>();
            clearingTaskQueue = new SortedList<int, Queue<CellEntity>>();
            temp = new CellEntity[220];
        }

        public int GetFirstNotEmptyRow() {
            foreach (var pair in clearingTaskQueue) {
                if (pair.Value.Count > 0) {
                    return pair.Key;
                }
            }
            return -1;
        }

        public int GetCountOfClearingTask(int row) {
            if (!clearingTaskQueue.TryGetValue(row, out var queue)) {
                return 0;
            }
            return queue.Count;
        }

        public void EnqueueClearingTask(CellEntity cell, int row) {
            if (!clearingTaskQueue.TryGetValue(row, out var queue)) {
                queue = new Queue<CellEntity>();
                clearingTaskQueue.Add(row, queue);
            }
            queue.Enqueue(cell);
        }

        public CellEntity DequeueClearingTask(int row) {
            return clearingTaskQueue[row].Dequeue();
        }

        public void Add(CellEntity cell, int entityID) {
            all.Add(entityID, cell);
            posMap.Add(cell.PosInt, cell);
        }

        public bool Has(Vector2Int pos) {
            return posMap.ContainsKey(pos);
        }

        public int TakeAll(out CellEntity[] cells) {
            int count = all.Count;
            if (count > temp.Length) {
                temp = new CellEntity[(int)(count * 1.5f)];
            }
            all.Values.CopyTo(temp, 0);
            cells = temp;
            return count;
        }

        public void ClearPosMap() {
            posMap.Clear();
        }

        public void UpdatePos(Vector2Int oldPos, CellEntity cell) {
            posMap.Remove(oldPos);
            posMap.Add(cell.PosInt, cell);
        }

        public void Remove(CellEntity cell) {
            all.Remove(cell.index);
            posMap.Remove(cell.PosInt);
        }

        public bool TryGetCellByPos(Vector2Int pos, out CellEntity cell) {
            return posMap.TryGetValue(pos, out cell);
        }

        public void Clear() {
            all.Clear();
            posMap.Clear();
            Array.Clear(temp, 0, temp.Length);
        }

    }

}