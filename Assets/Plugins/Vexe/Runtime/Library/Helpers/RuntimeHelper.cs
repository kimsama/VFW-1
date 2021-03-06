﻿using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Vexe.Runtime.Types;
using Vexe.Runtime.Extensions;

namespace Vexe.Runtime.Helpers
{
    public static class RuntimeHelper
    {
        public static int GetTargetID(object target)
        {
            var bb = target as BetterBehaviour;
            if (bb != null)
                return bb.Id;

            var bso = target as BetterScriptableObject;
            if (bso != null)
                return bso.Id;

            return target.GetHashCode();
        }

        public static void ResetTarget(object target)
        {
            var type = target.GetType();
            var members = RuntimeMember.CachedWrapMembers(type);
            for (int i = 0; i < members.Count; i++)
			{
				var member = members[i];
				var defAttr = member.Info.GetCustomAttribute<DefaultAttribute>();
				if (defAttr != null)
				{ 
					member.Target = target;
					var value = defAttr.Value;
					if (value == null && !member.Type.IsAbstract) // null means to instantiate a new instance
						value = member.Type.ActivatorInstance();
					member.Value = value;
				}
			}
		}

        public static int CombineHashCodes<Item0, Item1>(Item0 item0, Item1 item1)
        {
            int hash = 17;
            hash = hash * 31 + item0.GetHashCode();
            hash = hash * 31 + item1.GetHashCode();
            return hash;
        }

        public static int CombineHashCodes<Item0, Item1, Item2>(Item0 item0, Item1 item1, Item2 item2)
        {
            int hash = 17;
            hash = hash * 31 + item0.GetHashCode();
            hash = hash * 31 + item1.GetHashCode();
            hash = hash * 31 + item2.GetHashCode();
            return hash;
        }

        public static void Measure(string msg, int nTimes, Action<string> log, Action code)
        {
            log(string.Format("Time took to `{0}` is `{1}` ms", msg, Measure(nTimes, code)));
        }

        public static float Measure(int nTimes, Action code)
        {
            var w = System.Diagnostics.Stopwatch.StartNew();
            for (int i = 0; i < nTimes; i++)
                code();
            return w.ElapsedMilliseconds;
        }

        public static Dictionary<T, U> CreateDictionary<T, U>(IEnumerable<T> keys, IList<U> values)
        {
            return keys.Select((k, i) => new { k, v = values[i] }).ToDictionary(x => x.k, x => x.v);
        }

        public static Texture2D GetTexture(byte r, byte g, byte b, byte a, HideFlags flags)
        {
            var texture = new Texture2D(1, 1);
            texture.SetPixel(0, 0, new Color32(r, g, b, a));
            texture.Apply();
            texture.hideFlags = flags;
            return texture;
        }

        public static Texture2D GetTexture(Color32 c, HideFlags flags)
        {
            return GetTexture(c.r, c.g, c.b, c.a, flags);
        }

        public static Texture2D GetTexture(Color32 c)
        {
            return GetTexture(c, HideFlags.None);
        }

        public static Color HexToColor(string hex)
        {
            byte r = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
            byte g = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
            byte b = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
            return new Color32(r, g, b, 255);
        }
    }
}