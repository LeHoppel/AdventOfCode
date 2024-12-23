namespace AdventOfCode._2024;

public class Day09 : Day
{
    public override bool PrintTime { get => true; set { } }

    public override long CalculatePart01(string kindOfInput, string pathPrefix)
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
        
        return answerValue;
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

    public override long CalculatePart02(string kindOfInput, string pathPrefix)
    {
        List<string> input = TransformInput(kindOfInput, pathPrefix);
        
        Dictionary<int, int> freeBlocks = FindAllFreeBlocks(input);
        
        for (int i = input.Count - 1; i > 0; i--)
        {
            if (input[i] == "." || input[i] == input[i - 1]) continue;
            
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
                
                // remove/trim used free block
                freeBlocks.Remove(freeBlock.Key);
                if (freeBlock.Value - lengthOfBlockToMove > 0) freeBlocks.Add(freeBlock.Key + lengthOfBlockToMove, freeBlock.Value - lengthOfBlockToMove);
                
                break;
            }
        }
        
        long answerValue = 0;

        for (int i = 0; i < input.Count; i++)
            answerValue += input[i] != "." ? long.Parse(input[i]) * i : 0;

        return answerValue;
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