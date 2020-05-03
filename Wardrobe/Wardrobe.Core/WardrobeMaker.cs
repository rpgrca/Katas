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

            _sizes = _sizes
                .Distinct()
                .OrderBy(p => p)
                .ToList();
        }

        private void Combine(int wallSize, int[] wardrobeSizes, int currentCombination, List<List<int>> validCombinations, int[] validCombination, ref int minimumSpaceLeft)
        {
            if (currentCombination < 0)
            {
                if (validCombination.Sum() != 0)
                {
                    var leftOver = wallSize - validCombination.Sum();
                    if (leftOver >= 0 && leftOver < minimumSpaceLeft)
                    {
                        validCombinations.Clear();
                        minimumSpaceLeft = wallSize - validCombination.Sum();
                    }

                    if ((wallSize - validCombination.Sum() == minimumSpaceLeft))
                    {
                        validCombinations.Add(validCombination.ToList());
                    }
                }

                return;
            }

            for (int index = 0; index < wardrobeSizes.Length; index++)
            {
                validCombination[currentCombination] = wardrobeSizes[index];
                Combine(wallSize, wardrobeSizes, currentCombination - 1, validCombinations, validCombination, ref minimumSpaceLeft);
            }
        }
        
        public List<List<int>> GetCombinations(int wallSize)
        {
             var combinations = new List<List<int>>();
 
             if (wallSize < 0)
             {
                 throw new ArgumentException(WALL_SIZE_IS_INVALID_EXCEPTION);
             }
 
             if (wallSize == 0)
             {
                 return combinations;
             }
 
             var maximumCombinations = GetMaximumCombinations(wallSize);
             var sizesThatFitInWalls = _sizes
                 .Where(p => p <= wallSize)
                 .ToList();
             sizesThatFitInWalls.Insert(0, 0);
             var minimumLeftOverSize = wallSize;
             var indexes = new int[sizesThatFitInWalls.Count];

             Combine(wallSize, sizesThatFitInWalls.ToArray(), GetMaximumCombinations(wallSize) - 1, combinations,
                 new int[GetMaximumCombinations(wallSize)], ref minimumLeftOverSize);

             combinations
                 .ForEach(p => p.RemoveAll(q => q == 0));

             return combinations
                 .OrderBy(p => p[0])
                 .ToList()
                 .Distinct(new DistinctListComparer())
                 .ToList();

             /*
             for (var i = 0; i < sizesThatFitInWalls.Count * maximumCombinations; i++)
             {
                 var wardrobeSample = new List<int>();
                 for (var j = 0; j < maximumCombinations; j++)
                 {
                     var c = (j * maximumCombinations);
                     var d = 0;
                     if (c > 0)
                     {
                         d = i / c;
                     }
                     
                     
                     var d = 0;
                     if (c != 0)
                     {
                         d = i / c;
                     }
                     else
                     {
                         d = i;
                     }
                     var e = d % maximumCombinations;
                     
                     wardrobeSample.Add(sizesThatFitInWalls[e]);
                 }

                 if (wardrobeSample.Sum() == 0 || ((wallSize - wardrobeSample.Sum()) > minimumLeftOverSize))
                 {
                     continue;
                 }

                 if ((wallSize - wardrobeSample.Sum()) < minimumLeftOverSize)
                 {
                     combinations.Clear();
                     minimumLeftOverSize = wallSize - wardrobeSample.Sum();
                 }
                 
                 combinations.Add(wardrobeSample);
             }*/

             //return combinations;
        }
        /*
        public List<List<int>> GetCombinations(int wallSize)
        {
            var combinations = new List<List<int>>();

            if (wallSize < 0)
            {
                throw new ArgumentException(WALL_SIZE_IS_INVALID_EXCEPTION);
            }

            if (wallSize == 0)
            {
                return combinations;
            }

            var maximumCombinations = GetMaximumCombinations(wallSize);
            var sizesThatFitInWalls = _sizes.Where(p => p <= wallSize).ToList();
            var minimumLeftOverSize = wallSize;

            foreach (var size1 in sizesThatFitInWalls)
            {
                var wardrobeSample = new List<int>() { size1 };
                var availableSpace = wallSize - size1;
                
                foreach (var size2 in sizesThatFitInWalls)
                {
                    while (availableSpace >= size2)
                    {
                        wardrobeSample.Add(size2);
                        availableSpace -= size2;
                    }
                }

                if (availableSpace > minimumLeftOverSize)
                {
                    continue;
                }
                
                if (availableSpace < minimumLeftOverSize)
                {
                    combinations.Clear();
                }
                
                combinations.Add(wardrobeSample);
                minimumLeftOverSize = availableSpace;
            }
            
 //           foreach (var size in sizesThatFitInWalls)
 //           {
 //               var availableSpace = wallSize;
 //               var wardrobeSample = new List<int>();
//
 //               while (availableSpace >= size)
 //               {
 //                   wardrobeSample.Add(size);
 //                   availableSpace -= size;
  //              }
//
 //               if (availableSpace == 0)
  //              {
   //                 combinations.Add(wardrobeSample);
    //            }
     //       }

            return combinations;
        }*/

        private int GetMaximumCombinations(int wallSize) =>
            wallSize / _sizes[0];
    }
        internal class DistinctListComparer : IEqualityComparer<List<int>>
        {
            public bool Equals(List<int> lhs, List<int> rhs)
            {
                var different = false;
                
                if (lhs.Count == rhs.Count)
                {
                    lhs.Sort();
                    rhs.Sort();

                    for (int index = 0; index < lhs.Count; index++)
                    {
                        if (lhs[index] != rhs[index])
                        {
                            different = true;
                        }
                    }

                    if (!different)
                    {
                        return true;
                    }
                }

                return false;
            }

            public int GetHashCode(List<int> obj)
            {
                return 0;
            }
        }
}