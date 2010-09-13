// Type: StructureMap.BasicExtensions
// Assembly: StructureMap, Version=2.6.2.0, Culture=neutral, PublicKeyToken=e60ad81abae3c223
// Assembly location: C:\Development\RazorLib\lib\structuremap\StructureMap.dll

using StructureMap.Pipeline;
using System;
using System.Collections.Generic;

namespace StructureMap
{
    internal static class BasicExtensions
    {
        public static string ToName(this ILifecycle lifecycle);
        public static void Fill<T>(this IList<T> list, T value);
        public static void SafeDispose(this object target);

        public static void TryGet<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key,
                                                Action<TValue> action);

        internal static T As<T>(this object target) where T : class;
        public static bool IsIn<T>(this T target, IList<T> list);
    }
}
