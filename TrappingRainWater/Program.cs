/* Given an array of integers representing an elevation map where the width of each bar is 1,
 * return how much rainwater can be trapped
 * 
 * The sides are not walls
 * No negative integers
 */

/*
 * Solution without code:
 * Find the first non-zero value, this will be the left wall
 * Find the next non-zero value as the right wall
 * Out of the two walls, the smallest value will limit the height of the water
 * The current water = min(maxL, maxR) - current Height
 */

namespace TrappingRainWater
{
    class Program
    {
        /// <summary>
        /// Brute force solution
        /// </summary>
        /// <param name="input"></param>
        /// <returns>The amount of water that can be trapped for the given array</returns>
        static int trapped_water_bf(int[] input)
        {
            int size = input.Length;
            int water = 0;
            // Need more than 2 points to collect water
            if(size >3)
            {
                // Traverse the entire input calculating what the current amount of water amounts to
                for(int a = 0; a < size; a++)
                {
                    int max_left, max_right = 0;

                    // If at the beginning, you can't hold water on the left but you can find the highest right value
                    if(a == 0)
                    {
                        max_left = 0;
                        max_right = input.Skip(1).Max();
                    }
                    // Cannot hold water on the left most edge
                    else if(a == size - 1)
                    {
                        max_right = 0;
                        max_left = input.Reverse().Skip(1).Max();
                    }
                    // The first valid position that could hold water
                    else if (a == 1)
                    {
                        max_left = input[0];
                        max_right = input.Skip(a+1).Max();
                    }
                    // At any point other than the edges
                    else
                    {
                        max_left = input.Reverse().Skip(a+1).Max();
                        max_right = input.Skip(a + 1).Max();
                    }
                    // The formula to calculate the current amount of water at point a is found by
                    // using the smallest of the two walls and subtracting the current value at that index
                    int result = Math.Min(max_left, max_right) - input[a];
                    // If the current value for index a can hold water, add it to the water variable
                    if (result > 0)
                        water += result;
                }
            }
            // If not enough points then no water can be collected
            else
                water = 0;

            return water;
        }

        /// <summary>
        /// Optimized solution that uses the two pointer approach to calculate water level 
        /// </summary>
        /// <param name="input"></param>
        /// <returns>The amount of water that can be trapped for the given array</returns>
        static int trapped_water(int[] input)
        {
            // Initialize variables
            int water = 0, max_left =0, max_right = 0, result = 0;
            // Left and right pointers that start on opposite ends of the array
            int left = 0;
            int right = input.Length - 1;
            
            //While we can move the pointers inward
            while(left < right)
            {
                // If the left wall is smaller
                if(input[left] <= input[right])
                {
                    // If the current value for max_left is smaller than the value at index left, then replace max_left
                    if (input[left] > max_left)
                        max_left = input[left];
                    // The result calculates the current water height for the left side based on the wall to the left
                    // minus the current value at index [left]
                    result = max_left - input[left];
                    // Move pointer towards the right
                    left +=1;
                }
                // If the right wall is smaller
                else
                {
                    // If the current value for max_right is smaller than the value at index right, then replace max_right
                    if (input[right] > max_right)
                        max_right=input[right];
                    //The result calculates the current water height for the right side based on the wall to the right
                    // minus the current value at index [right]
                    result = max_right - input[right];
                    right -= 1;
                }
                // If the result of both calculations is a number > 0 then add it to the accumulated water
                if (result > 0)
                    water += result;
            }
            return water;
        }
        static void Main(string[] args)
        {
            int[] input = new int[] { 0, 1, 0, 2, 1, 0, 3, 1, 0, 1, 2 };
            Console.WriteLine(trapped_water_bf(input));

            Console.WriteLine(trapped_water(input));
        }
    }
}