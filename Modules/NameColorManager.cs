using BepInEx;
using BepInEx.Configuration;
using BepInEx.IL2CPP;
using System;
using HarmonyLib;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnhollowerBaseLib;
using TownOfHost;
using Hazel;
using System.Linq;
using System.Threading.Tasks;

namespace TownOfHost
{
    public class NameColorManager {
        public static NameColorManager Instance;

        public List<NameColorData> NameColors;
        public NameColorData DefaultData;

        public List<NameColorData> GetDatasBySeer(byte seerId)
            => NameColors.Where(data => data.seerId == seerId).ToList();
        
        public NameColorData GetData(byte seerId, byte targetId) {
            NameColorData data = NameColors.Where(data => data.seerId == seerId && data.targetId == targetId).FirstOrDefault();
            if(data == null) data = DefaultData;
            return data;
        }
        public void Add(byte seerId, byte targetId, string color) {
            NameColors.Add(new NameColorData(seerId, targetId, color));
        }
        public void Add(NameColorData data) {
            NameColors.Add(data);
        }

        public NameColorManager() {
            NameColors = new List<NameColorData>();
            DefaultData = new NameColorData(0,0,null);
        }
    }
    public class NameColorData {
        public byte seerId;
        public byte targetId;
        public string color;
        public NameColorData(byte seerId, byte targetId, string color) {
            this.seerId = seerId;
            this.targetId = targetId;
            this.color = color.StartsWith('#') ? color : "#" + color;
        }
        public string OpenTag => color != null ? $"<color={color}>" : "";
        public string CloseTag => color != null ? "</color>" : "";
    }
}