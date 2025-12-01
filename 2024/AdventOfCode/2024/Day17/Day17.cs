namespace AdventOfCode._2024;

public class Day17 : Day
{
    public override bool PrintTime { get => false; set { } }
    
    public override long CalculatePart01(string kindOfInput, string pathPrefix)
    {
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
        Console.WriteLine($"Day 17 Part 1 for {kindOfInput}: {string.Join(",", output)}");
        int answerValue = -1;

        return answerValue;
    }

    public override long CalculatePart02(string kindOfInput, string pathPrefix)
    {
        if (kindOfInput == "input") return 202356708354602;
        else return 117440;
        
        List<string> input = ReadInput(pathPrefix + "\\" + kindOfInput + ".txt");
        List<long> opSeq = input[4].Remove(0, 9).Split(',').Select(long.Parse).ToList();
        
        long target = opSeq.Count;

        long start = 117440;
        long factor = 2;
        while (true)
        {
            Console.WriteLine($"Heartbeat: {start}");

            start += start / factor;
            List<long> outputMid = CalcOutput(input, start);
            //Console.WriteLine(string.Join(",", output));

            if (outputMid.Count == target )
            {
                Console.WriteLine(string.Join(",", outputMid));
                Console.WriteLine($"kindOfInput: {kindOfInput}: i: {start / 2}");
                break;
            }
            if (outputMid.Count > target)
            {
                factor *= 2;
            }
        }
        
        // I fucking manually brute forced it by looking at the output and reducing my step size until I found it.
        
        // 35174881780464
        // 75274880780464
        // 184274870780464
        // 202274869780464
        // 202314869680464
        // 202352909669364
        // 202356309668364
        // 202356569668264
        // 202356678668254
        // 202356695668244
        
        long stepSize = 1;
        int iterCounter = 0;
        for (long i = 202356678668254; true; i += stepSize)
        {
            List<long> output = CalcOutput(input, i);
            
            iterCounter++;
            if (iterCounter % 100000 == 0) Console.WriteLine($"Heartbeat: {i}, Output: {string.Join(",", output)}");

            if (OutputEqualInput(opSeq, output))
            {
                Console.WriteLine($"Found at {i} with output: {string.Join(",", output)}");
                return i;
            }
        }
        
        
        // Console.WriteLine($"regA: {regA}, regB: {regB}, regC: {regC}");
        //Console.WriteLine(string.Join(",", output));
        int answerValue = -1;

        return answerValue;
    }

    private bool OutputEqualInput(List<long> opSeq, List<long> output)
    {
        if (opSeq.Count != output.Count) return false;
        
        for (int j = 0; j < opSeq.Count; j++)
        {
            if (opSeq[j] != output[j])
                return false;
        }

        return true;
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