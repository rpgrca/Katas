using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Wardrobe.Core
{
    public class WardrobeMaker
    {
        public const string SIZE_LIST_IS_NULL_EXCEPTION = "Size list cannot be null.";
        public const string SIZE_LIST_IS_EMPTY_EXCEPTION = "Size list cannot be empty.";
        public const string SIZE_LIST_CONTAINS_INVALID_VALUES_EXCEPTION = "Size list cannot contain zero or negative numbers.";
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
        }

        public IEnumerable GetCombinations(int i)
        {
            if (i < 0)
            {
                throw new ArgumentException(WALL_SIZE_IS_INVALID_EXCEPTION);
            }
            
            return new List<object>();
        }
    }
}