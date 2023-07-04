using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Oshi.Generic;
using UnityEngine;

namespace Oshi.Logic {

    public class L_PropRepo {

        SortedList<int, L_PropEntity> all;

        L_PropEntity[] array;

        List<L_PropEntity> propList_temp;

        public L_PropRepo() {
            all = new SortedList<int, L_PropEntity>();
            array = new L_PropEntity[0];
            propList_temp = new List<L_PropEntity>();
        }

        public void Add(L_PropEntity entity) {
            if (entity == null) {
                OshiLog.Error("AddProp Error: entity is null");
                return;
            }
            bool succ = all.TryAdd(entity.ID, entity);
            if (!succ) {
                OshiLog.Error($"仓库[逻辑层] 添加失败  {entity.ID}");
            } else {
                array = all.Values.ToArray();
            }
        }

        public void Remove(L_PropEntity entity) {
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

        public void ForEach(Predicate<L_PropEntity> match, Action<L_PropEntity> action) {
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
