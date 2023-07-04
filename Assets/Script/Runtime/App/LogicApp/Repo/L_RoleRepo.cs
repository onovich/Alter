using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Oshi.Generic;
using UnityEngine;

namespace Oshi.Logic {

    public class L_RoleRepo {

        SortedList<int, L_RoleEntity> all;

        L_RoleEntity[] array;

        List<L_RoleEntity> roleList_temp;

        public L_RoleRepo() {
            all = new SortedList<int, L_RoleEntity>();
            array = new L_RoleEntity[0];
            roleList_temp = new List<L_RoleEntity>();
        }

        public void Add(L_RoleEntity entity) {
            if (entity == null) {
                OshiLog.Error("AddRole Error: entity is null");
                return;
            }
            bool succ = all.TryAdd(entity.ID, entity);
            if (!succ) {
                OshiLog.Error($"实体仓库[逻辑层] 添加失败  {entity.ID}");
            } else {
                array = all.Values.ToArray();
            }
        }

        public void Remove(L_RoleEntity entity) {
            if (all.Remove(entity.ID)) {
                array = all.Values.ToArray();
                OshiLog.Log($"实体仓库[逻辑层] 移除 {entity.ID}");
            }
        }

        public void Clear() {
            for (int i = 0; i < all.Count; i++) {
                var entity = all.Values[i];
                entity.TearDown();
            }
            all.Clear();
        }

        public void ForEach(Predicate<L_RoleEntity> match, Action<L_RoleEntity> action) {
            var values = all.Values;
            for (int i = 0; i < values.Count; i += 1) {
                var v = values[i];
                if (match.Invoke(v)) {
                    action.Invoke(v);
                }
            }
        }

    }

}
