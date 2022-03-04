using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using Utils.ExtensionMethods.IEnumerable;

namespace Utils.Benchmark
{
    public class SortingBenchmark
    {
        static Func<int, int, int> NumberComparer => (int a, int b) => a - b;

        private IEnumerable<int> array10K { get; } = Enumerable.Range(0, 10000).Select(i => new Random().Next(10000)).ToArray();

        private IEnumerable<int> array20K { get; } = Enumerable.Range(0, 20000).Select(i => new Random().Next(10000)).ToArray();

        private IEnumerable<int> array10KInverted { get; } = Enumerable.Range(0, 10000).Select(i => new Random().Next(10000)).OrderByDescending(i => i).ToArray();

        private IEnumerable<int> array20KInverted { get; } = Enumerable.Range(0, 20000).Select(i => new Random().Next(10000)).OrderByDescending(i => i).ToArray();

        [Benchmark]
        public int[] Bubble_Sort_20K_Random() => array20K.Sort(SortingStrategy.bubble, NumberComparer).ToArray();

        [Benchmark]
        public int[] Ins_Sort_10K_Random() => array10K.Sort(SortingStrategy.insertion, NumberComparer).ToArray();

        [Benchmark]
        public int[] Merge_Sort_20K_Random() => array20K.Sort(SortingStrategy.merge, NumberComparer).ToArray();

        [Benchmark]
        public int[] Quick_Sort_20K_Random() => array20K.Sort(SortingStrategy.quick, NumberComparer).ToArray();

        [Benchmark]
        public int[] Heap_Sort_20K_Random() => array20K.Sort(SortingStrategy.heap, NumberComparer).ToArray();

        [Benchmark]
        public int[] Bubble_Sort_20K_Inverted() => array20KInverted.Sort(SortingStrategy.bubble, NumberComparer).ToArray();

        [Benchmark]
        public int[] Ins_Sort_10K_Inverted() => array10KInverted.Sort(SortingStrategy.insertion, NumberComparer).ToArray();

        [Benchmark]
        public int[] Merge_Sort_20K_Inverted() => array20KInverted.Sort(SortingStrategy.merge, NumberComparer).ToArray();

        [Benchmark]
        public int[] Quick_Sort_20K_Inverted() => array20KInverted.Sort(SortingStrategy.quick, NumberComparer).ToArray();

        [Benchmark]
        public int[] Heap_Sort_20K_Inverted() => array20KInverted.Sort(SortingStrategy.heap, NumberComparer).ToArray();
    }
}
