using System.Text;
using static LottoGenApi.RandomNumberProcedures;
namespace LottoGenApi;

public class NumbersSetGenerator
{
    public static async Task<List<int[]>> GenerateSetsAsListOfIntArray(int[] min, int[] max, int[] numbersPerGroup, decimal[] divergence, int sets, bool[] sumcheck, bool[] oecheck, bool sort = true)
    {
        //this one takes in multiple groups as input
        int bits = 4;
        List<int[]> results = [];

        await Task.Run(() =>
        {
            //use the maximum number of the request group 
            // as a time saver, this will use the minimum # of bits to represent an integer (e.g. the number 25 only requires 5bits in binary 11001)
            while ((Math.Pow(2, bits)) < max.Max() + 1)
            {
                bits++;
            }
            //get the length from either min, max or numbersPerGroup int array (should all be the same length)
            int groupsCount = numbersPerGroup.Length;

            //outer loop : Sets
            for (int i = 0; i < sets; i++)
            {
                int[] combinedGroups = []; //using Array Append below, so setting this as array with 0 elements

                for (int j = 0; j < groupsCount; j++)
                {
                    int[] NumberSet = ComputeNumberSet2(numbersPerGroup[j], min[j], max[j], bits, divergence[j], sort, sumcheck[j], oecheck[j]);

                    foreach (int number in NumberSet)
                    {
                        combinedGroups = [.. combinedGroups, number]; 
                    }
                }

                results.Add(combinedGroups);
            }
        });
        return results;
    }
}