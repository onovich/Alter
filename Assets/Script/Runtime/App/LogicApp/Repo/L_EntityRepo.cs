using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Oshi.Generic;
using UnityEngine;

namespace Oshi.Logic {

    public class L_EntityRepo {

        SortedList<int, L_Entity> all;

        L_Entity[] array;

        List<L_Entity> roleList_temp;

        public L_EntityRepo() {
            all = new SortedList<int, L_Entity>();
            array = new L_Entity[0];
            roleList_temp = new List<L_Entity>();
        }

        public void Add(L_Entity entity) {
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

        public void Remove(L_Entity entity) {
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

        public void ForEach(Predicate<L_Entity> match, Action<L_Entity> action) {
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
