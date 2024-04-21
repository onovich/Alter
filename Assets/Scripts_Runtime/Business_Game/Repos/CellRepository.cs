using System;
using System.Collections.Generic;
using UnityEngine;

namespace Alter {

    public class CellRepository {

        Dictionary<int, CellEntity> all;
        Dictionary<Vector2Int, CellEntity> posMap;
        CellEntity[] temp;

        public CellRepository() {
            all = new Dictionary<int, CellEntity>();
            posMap = new Dictionary<Vector2Int, CellEntity>();
            temp = new CellEntity[1000];
        }

        public void Add(CellEntity cell) {
            all.Add(cell.entityID, cell);
            posMap.Add(cell.PosInt, cell);
        }

        public bool Has(Vector2Int pos) {
            return posMap.ContainsKey(pos);
        }

        public bool HasDifferent(Vector2Int pos, int index) {
            var has = posMap.TryGetValue(pos, out var cell);
            return has && cell.entityID != index;
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
            all.Remove(cell.entityID);
            posMap.Remove(cell.PosInt);
        }

        public bool TryGetBlock(int index, out CellEntity cell) {
            return all.TryGetValue(index, out cell);
        }

        public bool TryGetBlockByPos(Vector2Int pos, out CellEntity cell) {
            return posMap.TryGetValue(pos, out cell);
        }

        public bool IsInRange(int entityID, in Vector2 pos, float range) {
            bool has = TryGetBlock(entityID, out var cell);
            if (!has) {
                return false;
            }
            return Vector2.SqrMagnitude(cell.Pos - pos) <= range * range;
        }

        public void ForEach(Action<CellEntity> action) {
            foreach (var cell in all.Values) {
                action(cell);
            }
        }

        public CellEntity GetNeareast(Vector2 pos, float radius) {
            CellEntity nearestBlock = null;
            float nearestDist = float.MaxValue;
            float radiusSqr = radius * radius;
            foreach (var cell in all.Values) {
                float dist = Vector2.SqrMagnitude(cell.Pos - pos);
                if (dist <= radiusSqr && dist < nearestDist) {
                    nearestDist = dist;
                    nearestBlock = cell;
                }
            }
            return nearestBlock;
        }

        public void Clear() {
            all.Clear();
            posMap.Clear();
            Array.Clear(temp, 0, temp.Length);
        }

    }

}