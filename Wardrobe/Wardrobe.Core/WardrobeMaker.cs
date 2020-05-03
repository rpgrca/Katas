using System;
using System.Collections.Generic;
using System.Linq;

namespace Wardrobe.Core
{
    public class WardrobeMaker
    {
        private class DistinctListComparer : IEqualityComparer<List<int>>
        {
            public bool Equals(List<int> lhs, List<int> rhs)
            {
                if (lhs is null) return (rhs is null);
                if (rhs is null) return false;
                return lhs.Count == rhs.Count && lhs.SequenceEqual(rhs);
            }

            public int GetHashCode(List<int> obj)
            {
                return 0;
            }
        }

        public const string SIZE_LIST_IS_NULL_EXCEPTION = "Size list cannot be null.";
        public const string SIZE_LIST_IS_EMPTY_EXCEPTION = "Size list cannot be empty.";

        public const string SIZE_LIST_CONTAINS_INVALID_VALUES_EXCEPTION =
            "Size list cannot contain zero or negative numbers.";

        public const string WALL_SIZE_IS_INVALID_EXCEPTION = "Wall size cannot be negative.";

        private readonly List<int> _sizes;

        public WardrobeMaker(List<int> sizes)
        {
            _sizes = sizes ?? throw new ArgumentException(SIZE_LIST_IS_NULL_EXCEPTION);
            if (_sizes.Count == 0)
            {
                throw new ArgumentException(SIZE_LIST_IS_EMPTY_EXCEPTION);
            }

            if (_sizes.Any(p => p <= 0))
            {
                throw new ArgumentException(SIZE_LIST_CONTAINS_INVALID_VALUES_EXCEPTION);
            }

            _sizes = _sizes
                .Distinct()
                .OrderBy(p => p)
                .ToList();
        }

        private static void Combine(int wallSize, int[] wardrobeSizes, int currentIndex,
            ICollection<List<int>> validCombinations, IList<int> currentCombination, ref int minimumSpaceLeft)
        {
            if (currentIndex < 0)
            {
                if (currentCombination.Sum() == 0) return;

                var leftOver = wallSize - currentCombination.Sum();
                if (leftOver >= 0 && leftOver < minimumSpaceLeft)
                {
                    validCombinations.Clear();
                    minimumSpaceLeft = wallSize - currentCombination.Sum();
                }

                if ((wallSize - currentCombination.Sum() == minimumSpaceLeft))
                {
                    validCombinations.Add(currentCombination.ToList());
                }

                return;
            }

            foreach (var wardrobeSize in wardrobeSizes)
            {
                currentCombination[currentIndex] = wardrobeSize;
                Combine(wallSize, wardrobeSizes, currentIndex - 1, validCombinations, currentCombination,
                    ref minimumSpaceLeft);
            }
        }

        public List<List<int>> GetCombinations(int wallSize)
        {
            if (wallSize < 0) throw new ArgumentException(WALL_SIZE_IS_INVALID_EXCEPTION);

            var combinations = new List<List<int>>();
            if (wallSize == 0) return combinations;

            var maximumCombinations = GetMaximumCombinations(wallSize);
            var sizesThatFitInWalls = GetSizesThatFitInWall(wallSize);

            AddZeroPlaceholder(sizesThatFitInWalls);
            var minimumLeftOverSize = wallSize;

            Combine(wallSize, sizesThatFitInWalls.ToArray(), maximumCombinations - 1, combinations,
                new int[maximumCombinations], ref minimumLeftOverSize);

            RemoveZeroPlaceholders(combinations);
            return RemoveDuplicatedSolutions(combinations);
        }

        private IList<int> GetSizesThatFitInWall(int wallSize) =>
            _sizes
                .Where(p => p <= wallSize)
                .ToList(); 

        private static void AddZeroPlaceholder(IList<int> sizesThatFitInWalls) =>
            sizesThatFitInWalls.Insert(0, 0);

        private static List<List<int>> RemoveDuplicatedSolutions(IEnumerable<List<int>> combinations) =>
            combinations
                .OrderBy(p => p[0])
                .ToList()
                .Distinct(new DistinctListComparer())
                .ToList();

        private static void RemoveZeroPlaceholders(List<List<int>> combinations) =>
            combinations.ForEach(p => p.RemoveAll(q => q == 0));

        private int GetMaximumCombinations(int wallSize) =>
            wallSize / _sizes[0];
    }
}