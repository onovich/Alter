using System;
using UnityEngine;

namespace Alter {

    public static class GameCellDomain {

        public static CellEntity Spawn(GameBusinessContext ctx, Vector2Int pos) {
            var cell = GameFactory.Cell_Spawn(ctx.idRecordService,
                                              ctx.assetsInfraContext,
                                              pos);

            ctx.cellRepo.Add(cell);
            return cell;
        }

        public static void UnSpawn(GameBusinessContext ctx, CellEntity cell) {
            ctx.cellRepo.Remove(cell);
            cell.TearDown();
        }

        public static void ApplyFalling(GameBusinessContext ctx, CellEntity cell) {
            if (!ctx.gameEntity.IsFallingFrame) {
                return;
            }
            var dir = Vector2Int.down;
            var pos = cell.PosInt;
            cell.Pos_SetPos(pos + dir);
        }

        public static void ApplyConstraint(GameBusinessContext ctx) {
            var cellLen = ctx.cellRepo.TakeAllCurrentBlock(out var cellArr);
            var offset = Vector2Int.zero;
            for (int i = 0; i < cellLen; i++) {
                var cell = cellArr[i];
                var pos = cell.PosInt;

                var _offset = cell.Move_GetConstraintOffset(ctx.currentMapEntity.mapSize,
                                                        ctx.currentMapEntity.Pos,
                                                        pos);
                if (Mathf.Abs(_offset.x) >= Mathf.Abs(offset.x)) {
                    offset.x = _offset.x;
                }
                if (Mathf.Abs(_offset.y) >= Mathf.Abs(offset.y)) {
                    offset.y = _offset.y;
                }
            }
            if (offset == Vector2Int.zero) {
                return;
            }
            for (int i = 0; i < cellLen; i++) {
                var cell = cellArr[i];
                var pos = cell.PosInt;
                cell.Pos_SetPos(pos + offset);
            }
        }

        public static void ApplyMove(GameBusinessContext ctx, CellEntity cell, Vector2Int dir) {
            var pos = cell.PosInt;
            cell.Pos_SetPos(pos + dir);
        }

        public static void ApplyAllLand(GameBusinessContext ctx) {
            var cellLen = ctx.cellRepo.TakeAll(out var cellArr);
            for (int i = 0; i < cellLen; i++) {
                var cell = cellArr[i];
                cell.fsmComponent.Landing_Enter();
            }
        }

        public static void ApplyCheckAllCellLanding(GameBusinessContext ctx) {
            if (!ctx.gameEntity.IsFallingFrame) {
                return;
            }
            var cellLen = ctx.cellRepo.TakeAllCurrentBlock(out var cellArr);
            bool notInLand = true;
            for (int i = 0; i < cellLen; i++) {
                var cell = cellArr[i];
                if (cell.fsmComponent.Status == CellFSMStatus.Landing) {
                    continue;
                }
                notInLand &= GameCellDomain.CheckInAir(ctx, cell);
                notInLand &= GameCellDomain.CheckNextIsNotLandingCell(ctx, cell);
                if (!notInLand) {
                    break;
                }
            }

            if (notInLand) {
                return;
            }
            for (int i = 0; i < cellLen; i++) {
                var cell = cellArr[i];
                cell.fsmComponent.Landing_Enter();
                ctx.cellRepo.RemoveCurrentBlock(cell.entityID);
            }
        }

        static bool CheckInAir(GameBusinessContext ctx, CellEntity cell) {
            var dir = Vector2Int.down;
            var pos = cell.PosInt;
            var map = ctx.currentMapEntity;
            var mapSize = map.mapSize;
            var mapPos = map.Pos;
            return cell.Move_CheckInAir(mapSize, mapPos, pos, dir);
        }

        static bool CheckNextIsNotLandingCell(GameBusinessContext ctx, CellEntity cell) {
            var dir = Vector2Int.down;
            var pos = cell.PosInt;
            var nextPos = pos + dir;
            var has = ctx.cellRepo.TryGetBlockByPos(nextPos, out var _);
            if (!has) {
                return true;
            }
            if (cell.fsmComponent.Status == CellFSMStatus.Landing) {
                return false;
            }
            return true;
        }

    }

}