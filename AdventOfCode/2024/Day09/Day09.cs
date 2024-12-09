namespace AdventOfCode._2024;

public class Day09 : Day
{
    public override int CalculatePart01(string kindOfInput, string pathPrefix)
    {
        List<string> input = TransformInput(kindOfInput, pathPrefix);

        for (int i = input.Count - 1; i >= 0; i--)
        {
            int firstFreeIndex = input.IndexOf(".");

            if (firstFreeIndex >= i) break;

            input[firstFreeIndex] = input[i];
            input[i] = ".";
        }
        
        long answerValue = 0;

        for (int i = 0; i < input.Count; i++)
            answerValue += input[i] != "." ? long.Parse(input[i]) * i : 0;
        
        Console.WriteLine($"Day 9 part 1 {kindOfInput}: {answerValue}");
        
        return -1;
    }

    private List<string> TransformInput(string kindOfInput, string pathPrefix)
    {
        List<string> input = ReadInput(pathPrefix + "\\" + kindOfInput + ".txt");
        List<string> transformedInput = new();

        for (int i = 0; i < input[0].Length; i++)
        {
            int lengthOfBlock = int.Parse(input[0][i].ToString());
            int iD = i / 2;
            
            if (i % 2 == 0)
                for (int j = 0; j < lengthOfBlock; j++)
                    transformedInput.Add(iD.ToString());
            else
                for (int j = 0; j < lengthOfBlock; j++)
                    transformedInput.Add(".");
        }

        return transformedInput;
    }

    public override int CalculatePart02(string kindOfInput, string pathPrefix)
    {
        List<string> input = TransformInput(kindOfInput, pathPrefix);
        
        for (int i = input.Count - 1; i >= 0; i--)
        {
            if (input[i] == ".") continue;
            if (i > 0 && input[i] == input[i - 1]) continue;
            
            Dictionary<int, int> freeBlocks = FindAllFreeBlocks(input);
            
            int lengthOfBlockToMove = 0;
            for (int j = i; j < input.Count; j++)
            {
                if (input[j] == input[i]) lengthOfBlockToMove++;
                else break;
            }
            
            foreach (KeyValuePair<int, int> freeBlock in freeBlocks)
            {
                if (freeBlock.Key > i || freeBlock.Value < lengthOfBlockToMove) continue;
                
                List<string> blockToMove = input.GetRange(i, lengthOfBlockToMove);
                // remove free block (e.g. ".....") and insert block to move
                input.RemoveRange(freeBlock.Key, lengthOfBlockToMove);
                input.InsertRange(freeBlock.Key, blockToMove);
                
                // remove block that was moved and insert free block (e.g. ".....")
                input.RemoveRange(i, lengthOfBlockToMove);
                input.InsertRange(i, blockToMove.ConvertAll(x => "."));
                break;
            }
            
        }
        
        long answerValue = 0;

        for (int i = 0; i < input.Count; i++)
            answerValue += input[i] != "." ? long.Parse(input[i]) * i : 0;
        
        Console.WriteLine($"Day 9 part 2 {kindOfInput}: {answerValue}");

        return -1;
    }


    private Dictionary<int, int> FindAllFreeBlocks(List<string> input)
    {
        Dictionary<int, int> freeBlocks = new();

        for (int i = 0; i < input.Count; i++)
        {
            if (input[i] != ".") continue;

            int lengthOfFreeBlock = 0;
            for (int j = i; j < input.Count; j++)
            {
                if (input[j] == input[i]) lengthOfFreeBlock++;
                else break;
            }
            
            freeBlocks.Add(i, lengthOfFreeBlock);

            i += lengthOfFreeBlock;
        }

        return freeBlocks;
    }
}