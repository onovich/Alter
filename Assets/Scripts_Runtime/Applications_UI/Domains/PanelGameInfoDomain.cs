using System;
using UnityEngine;
using UnityEngine.UI;

namespace Alter.UI {

    public static class PanelGameInfoDomain {

        public static void Open(UIAppContext ctx) {
            var has = ctx.UniquePanel_TryGet<Panel_GameInfo>(out var panel);
            if (has) {
                return;
            }

            panel = ctx.uiCore.UniquePanel_Open<Panel_GameInfo>();
            panel.Ctor();

            panel.OnRestartBtnClickHandle += () => {
                ctx.evt.GameInfo_OnRestartBtnClick();
            };
        }

        public static void RefreshTime(UIAppContext ctx, float time) {
            var has = ctx.UniquePanel_TryGet<Panel_GameInfo>(out var panel);
            if (!has) {
                return;
            }
            panel.RefreshTime(time);
        }

        public static void RefreshStep(UIAppContext ctx, int step) {
            var has = ctx.UniquePanel_TryGet<Panel_GameInfo>(out var panel);
            if (!has) {
                return;
            }
            panel.RefreshStep(step);
        }

        public static void RefreshScore(UIAppContext ctx, int score) {
            var has = ctx.UniquePanel_TryGet<Panel_GameInfo>(out var panel);
            if (!has) {
                return;
            }
            panel.RefreshScore(score);
        }

        public static void RefreshNextScore(UIAppContext ctx, int score, Color color) {
            var has = ctx.UniquePanel_TryGet<Panel_GameInfo>(out var panel);
            if (!has) {
                return;
            }
            panel.RefreshNextScore(score, color);
        }

        public static void Close(UIAppContext ctx) {
            var has = ctx.UniquePanel_TryGet<Panel_GameOver>(out var panel);
            if (!has) {
                return;
            }
            ctx.uiCore.UniquePanel_Close<Panel_GameOver>();
        }

    }

}