public static class Arrays
{
    /// <summary>
    /// This function will produce an array of size 'length' starting with 'number' followed by multiples of 'number'.  For 
    /// example, MultiplesOf(7, 5) will result in: {7, 14, 21, 28, 35}.  Assume that length is a positive
    /// integer greater than 0.
    /// </summary>
    /// <returns>array of doubles that are the multiples of the supplied number</returns>
    public static double[] MultiplesOf(double number, int length)
    {
        // TODO Problem 1 Start
        // Remember: Using comments in your program, write down your process for solving this problem
        // step by step before you write the code. The plan should be clear enough that it could
        // be implemented by another person.

        /*
        PLAN FOR MultiplesOf:
        1. Create a new double array with size equal to 'length'
        2. Loop from i = 0 to i < length
        3. For each iteration, calculate the multiple: number * (i + 1)
           - When i = 0, multiple = number * 1 (first multiple)
           - When i = 1, multiple = number * 2 (second multiple)
           - etc.
        4. Store each calculated multiple in the array at position i
        5. Return the completed array
        */

        // Create an array of the specified length
        double[] result = new double[length];

        // Fill the array with multiples
        for (int i = 0; i < length; i++)
        {
            // Calculate the (i+1)th multiple
            result[i] = number * (i + 1);
        }

        return result;
    }

    /// <summary>
    /// Rotate the 'data' to the right by the 'amount'.  For example, if the data is 
    /// List<int>{1, 2, 3, 4, 5, 6, 7, 8, 9} and an amount is 3 then the list after the function runs should be 
    /// List<int>{7, 8, 9, 1, 2, 3, 4, 5, 6}.  The value of amount will be in the range of 1 to data.Count, inclusive.
    ///
    /// Because a list is dynamic, this function will modify the existing data list rather than returning a new list.
    /// </summary>
    public static void RotateListRight(List<int> data, int amount)
    {
        // TODO Problem 2 Start
        // Remember: Using comments in your program, write down your process for solving this problem
        // step by step before you write the code. The plan should be clear enough that it could
        // be implemented by another person.

        /*
        PLAN FOR RotateListRight:
        1. Calculate the effective amount to rotate (amount may be larger than data.Count)
           - Use modulo: amount %= data.Count to handle cases where amount > data.Count
        2. If amount is 0, no rotation is needed (return early)
        3. Identify the split point: data.Count - amount
           - This is where we'll split the list into two parts
        4. Extract the last 'amount' elements using GetRange():
           - rightPart = data.GetRange(splitPoint, amount)
        5. Extract the first 'splitPoint' elements using GetRange():
           - leftPart = data.GetRange(0, splitPoint)
        6. Clear the original list: data.Clear()
        7. Add the rightPart first, then the leftPart using AddRange()
           - This effectively rotates the list to the right
        8. The original list is now modified in place
        */

        // Handle cases where amount might be larger than the list
        amount = amount % data.Count;

        // If amount is 0, no rotation needed
        if (amount == 0 || data.Count == 0)
        {
            return;
        }

        // Find the split point
        int splitPoint = data.Count - amount;

        // Get the last 'amount' elements (these will move to the front)
        List<int> rightPart = data.GetRange(splitPoint, amount);

        // Get the first 'splitPoint' elements (these will move to the back)
        List<int> leftPart = data.GetRange(0, splitPoint);

        // Clear the original list
        data.Clear();

        // Add the right part first, then the left part
        data.AddRange(rightPart);
        data.AddRange(leftPart);
    }
}