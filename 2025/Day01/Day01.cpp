#include "Day01.h"

#include <string>
#include <vector>

#include "../Utility/Utilitiy.h"

using namespace Day01;

std::string Day01::firstPart() {
    auto input = Utility::readInput(inputPath);

    int result = 0;
    int dial = 50;

    for (auto inputPart: input) {
        if (inputPart[0] == 'L') inputPart[0] = '-';
        if (inputPart[0] == 'R') inputPart[0] = '+';

        int direction = std::stoi(inputPart);

        dial += direction;
        dial %= 100;

        if (dial < 0) dial += 100;

        if (dial == 0) result++;
    }

    return std::to_string(result);
}

std::string Day01::secondPart() {
    auto input = Utility::readInput(inputPath);

    int result = 0;
    int dial = 50;

    for (auto inputPart: input) {
        int dir = 0;
        if (inputPart[0] == 'L') dir = -1;
        if (inputPart[0] == 'R') dir = 1;
        inputPart[0] = '0';

        int step = std::stoi(inputPart);

        while (step > 0) {
            dial += dir;
            if (dial == 100) {
                dial = 0;
                result++;
            } else if (dial == 0) {
                result++;
            } else if (dial == -1) {
                dial = 99;
            }
            step--;
        }
    }

    return std::to_string(result);
}
