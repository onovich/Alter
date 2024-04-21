using System;
using System.Collections.Generic;
using UnityEngine;

namespace Alter {

    public class CellRepository {

        Dictionary<int, CellSubEntity> all;
        Dictionary<Vector2Int, CellSubEntity> posMap;
        CellSubEntity[] temp;

        public CellRepository() {
            all = new Dictionary<int, CellSubEntity>();
            posMap = new Dictionary<Vector2Int, CellSubEntity>();
            temp = new CellSubEntity[1000];
        }

        public void Add(CellSubEntity cell) {
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

        public int TakeAll(out CellSubEntity[] cells) {
            int count = all.Count;
            if (count > temp.Length) {
                temp = new CellSubEntity[(int)(count * 1.5f)];
            }
            all.Values.CopyTo(temp, 0);
            cells = temp;
            return count;
        }

        public void UpdatePos(Vector2Int oldPos, CellSubEntity cell) {
            posMap.Remove(oldPos);
            posMap.Add(cell.PosInt, cell);
        }

        public void Remove(CellSubEntity cell) {
            all.Remove(cell.entityID);
            posMap.Remove(cell.PosInt);
        }

        public bool TryGetBlock(int index, out CellSubEntity cell) {
            return all.TryGetValue(index, out cell);
        }

        public bool TryGetBlockByPos(Vector2Int pos, out CellSubEntity cell) {
            return posMap.TryGetValue(pos, out cell);
        }

        public bool IsInRange(int entityID, in Vector2 pos, float range) {
            bool has = TryGetBlock(entityID, out var cell);
            if (!has) {
                return false;
            }
            return Vector2.SqrMagnitude(cell.Pos - pos) <= range * range;
        }

        public void ForEach(Action<CellSubEntity> action) {
            foreach (var cell in all.Values) {
                action(cell);
            }
        }

        public CellSubEntity GetNeareast(Vector2 pos, float radius) {
            CellSubEntity nearestBlock = null;
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