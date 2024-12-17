namespace AdventOfCode._2024;

public class Day17 : Day
{
    public override bool PrintTime { get => true; set { } }
    
    public override long CalculatePart01(string kindOfInput, string pathPrefix)
    {
        return -1;
        List<string> input = ReadInput(pathPrefix + "\\" + kindOfInput + ".txt");
        int regA = int.Parse(input[0].Remove(0, 12));
        int regB = int.Parse(input[1].Remove(0, 12));
        int regC = int.Parse(input[2].Remove(0, 12));
        
        List<short> opSeq = input[4].Remove(0, 9).Split(',').Select(short.Parse).ToList();

        List<int> output = new();
        int instrucPointer = 0;
        while (instrucPointer < opSeq.Count)
        {
            short opcode = opSeq[instrucPointer];
            int operand = opSeq[instrucPointer + 1];
            int literalOperand = operand;
            int comboOperand = operand == 4 ? regA : (operand == 5 ? regB : (operand == 6 ? regC : operand));
            
            //Console.WriteLine($"regA: {regA}, regB: {regB}, regC: {regC}, opcode: {opcode}, operand: {operand}, instrucPointer: {instrucPointer}");
            
            instrucPointer += 2;

            switch (opcode)
            {
                case 0:
                    regA /= (int) Math.Floor(Math.Pow(2, comboOperand));
                    break;
                case 1:
                    regB = regB ^ literalOperand;
                    break;
                case 2:
                    regB = comboOperand % 8;
                    break;
                case 3:
                    if (regA != 0) instrucPointer = literalOperand;
                    break;
                case 4:
                    regB = regB ^ regC;
                    break;
                case 5:
                    output.Add(comboOperand % 8);
                    break;
                case 6:
                    regB = regA / (int) Math.Floor(Math.Pow(2, comboOperand));
                    break;
                case 7:
                    regC = regA / (int) Math.Floor(Math.Pow(2, comboOperand));
                    break;
            }
        }

        // Console.WriteLine($"regA: {regA}, regB: {regB}, regC: {regC}");
        Console.WriteLine(string.Join(",", output));
        int answerValue = 0;

        return answerValue;
    }

    public override long CalculatePart02(string kindOfInput, string pathPrefix)
    {
        if (kindOfInput == "input") return -1;
        
        List<string> input = ReadInput(pathPrefix + "\\" + kindOfInput + ".txt");
        List<int> opSeq = input[4].Remove(0, 9).Split(',').Select(int.Parse).ToList();
        
        for (long i = 0; i < int.MaxValue; i++)
        {
            if (i % 1000000 == 0) Console.WriteLine($"Heartbeat: {i}");
            List<long> output = CalcOutput(input, i);
            //Console.WriteLine(string.Join(",", output));
            
            if (opSeq.Count != output.Count) continue;
            bool equal = true;
            for (int j = 0; j < opSeq.Count; j++)
            {
                if (opSeq[j] != output[j])
                {
                    equal = false;
                    break;
                }
            }

            if (equal)
            {
                Console.WriteLine($"kindOfInput: {kindOfInput}: i: {i}");
                break;
            }
        }
        

        // Console.WriteLine($"regA: {regA}, regB: {regB}, regC: {regC}");
        //Console.WriteLine(string.Join(",", output));
        int answerValue = -1;

        return answerValue;
    }

    private static List<long> CalcOutput(List<string> input, long regA)
    {
        long regB = 0;
        long regC = 0;
        
        List<short> opSeq = input[4].Remove(0, 9).Split(',').Select(short.Parse).ToList();
        
        List<long> output = new();
        int instrucPointer = 0;
        while (instrucPointer < opSeq.Count)
        {
            short opcode = opSeq[instrucPointer];
            int operand = opSeq[instrucPointer + 1];
            int literalOperand = operand;
            long comboOperand = operand == 4 ? regA : (operand == 5 ? regB : (operand == 6 ? regC : operand));
            
            //Console.WriteLine($"regA: {regA}, regB: {regB}, regC: {regC}, opcode: {opcode}, operand: {operand}, instrucPointer: {instrucPointer}");
            
            instrucPointer += 2;

            switch (opcode)
            {
                case 0:
                    regA /= (int) Math.Floor(Math.Pow(2, comboOperand));
                    break;
                case 1:
                    regB = regB ^ literalOperand;
                    break;
                case 2:
                    regB = comboOperand % 8;
                    break;
                case 3:
                    if (regA != 0) instrucPointer = literalOperand;
                    break;
                case 4:
                    regB = regB ^ regC;
                    break;
                case 5:
                    output.Add(comboOperand % 8);
                    break;
                case 6:
                    regB = regA / (int) Math.Floor(Math.Pow(2, comboOperand));
                    break;
                case 7:
                    regC = regA / (int) Math.Floor(Math.Pow(2, comboOperand));
                    break;
            }
        }
        return output;
    }
}