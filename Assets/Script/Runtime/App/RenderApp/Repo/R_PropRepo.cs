using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Oshi.Generic;
using UnityEngine;

namespace Oshi.Render {

    public class R_PropRepo {

        SortedList<int, R_PropEntity> all;

        R_PropEntity[] array;

        List<R_PropEntity> propList_temp;

        public R_PropRepo() {
            all = new SortedList<int, R_PropEntity>();
            array = new R_PropEntity[0];
            propList_temp = new List<R_PropEntity>();
        }

        public void Add(R_PropEntity entity) {
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

        public void Remove(R_PropEntity entity) {
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

        public void ForEach(Predicate<R_PropEntity> match, Action<R_PropEntity> action) {
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