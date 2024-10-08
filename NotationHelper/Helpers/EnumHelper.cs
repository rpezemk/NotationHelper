﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicDataModel.DataModel.Structure;

namespace MusicDataModel.Helpers
{

    public static class EnumHelper
    {
        public static void CalculateResult(this IEnumerable<IWidthable> list, double arbitrary)
        {
            var weightSum = list.Select(list => list.Weight).Sum();
            foreach(var weightable in list)
            {
                weightable.ResValue = arbitrary * (weightable.Weight / weightSum);
            }
        }

        public static void DivideSet<T>(this List<T> inputValues, out List<T> outLeft, out List<T> outRight)
        {
            outLeft = new List<T>();
            outRight = new List<T>();

            var cnt = inputValues.Count;
            var half = (int)Math.Ceiling(cnt / 2.0);

            for (var i = 0; i < half; i++)
            {
                outLeft.Add(inputValues[i]);
            }

            for(var i = half; i < cnt; i++)
            {
                outRight.Add(inputValues[i]);
            }
        }

        public static void DivideSet<T>(this List<T> inputValues, int maxCount, out List<List<T>> resGroups, out int nResGroups)
        {
            resGroups = new List<List<T>>();
            var cnt = inputValues.Count;


            nResGroups = (int)Math.Ceiling(cnt / (float)maxCount);
            
            var matrixPlaces = nResGroups * maxCount;
            var lastDiff = matrixPlaces - cnt;
            var lastGroupCount = cnt - (nResGroups - 1) * maxCount;
            var currIdx = 0;
            // full groups but not last:
            for(int groupNo  = 0; groupNo < nResGroups -1 ; groupNo++)
            {
                var group = new List<T>();
                for (var i = 0; i < maxCount; i++) 
                {
                    group.Add(inputValues[groupNo * maxCount + i]);
                }
                resGroups.Add(group);
            }

            // last group:
            var lastGroup = new List<T>();
            for (var i = 0; i < lastGroupCount; i++)
            {
                lastGroup.Add(inputValues[(nResGroups - 1) * maxCount + i]);
            }
            resGroups.Add(lastGroup);
        }

        public static void ForEach<T>(this IEnumerable<T> values, Action<T> actionT)
        {
            if (actionT == null)
                return;
            foreach(var v in values) { actionT(v); }
        }

        public static T? Is<T>(object o) where T : class
        {
            if (o == null)
                return null;
            if(o.GetType() == typeof(T))
                return (T)o;
            return null;
        }

        public static List<T> ButNotIn<T>(this List<T> firstSet, List<T> secondSet)
        {
            var res = firstSet.Where(ps => !secondSet.Contains(ps)).ToList();
            return res;
        }

        public static bool IsSameSet<T>(this List<T> values1, List<T> values2)
        {
            var res = values1.All(v1 => values2.Contains(v1)) && values2.All(v2 => values1.Contains(v2));
            return res;
        }
    }
}
