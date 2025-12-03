#include "Day03.h"

#include <iostream>
#include <ostream>
#include <vector>

#include "../Utility/Utilitiy.h"

using namespace Day03;

std::string Day03::firstPart() {
    auto input = Utility::readInput(inputPath);
    int result = 0;

    for (auto bank: input) {
        std::string joltage = bank.substr(0, 2);

        for (int i = 2; i < bank.size(); i++) {
            std::string temp = "";
            temp.push_back(joltage[1]);
            temp.push_back(bank[i]);

            if (std::stoi(temp) > std::stoi(joltage)) {
                joltage = temp;
                continue;
            }

            temp = "";
            temp.push_back(joltage[0]);
            temp.push_back(bank[i]);
            if (std::stoi(temp) > std::stoi(joltage)) {
                joltage = temp;
            }
        }

        result += std::stoi(joltage);
    }

    return std::to_string(result);
}

std::string Day03::secondPart() {
    auto input = Utility::readInput(inputPath);
    long long result = 0;

    for (auto bank: input) {
        std::string joltage = bank.substr(0, 12);
        std::string bestCase = joltage;

        for (int i = 12; i < bank.size(); i++) {
            for (int j = 0; j < 12; j++) {
                std::string temp = joltage;
                temp.push_back(bank[i]);
                temp.erase(j, 1);

                if (std::stoll(temp) > std::stoll(bestCase)) {
                    bestCase = temp;
                }
            }

            if (std::stoll(bestCase) > std::stoll(joltage))
                joltage = bestCase;
        }

        result += std::stoll(joltage);
    }

    return std::to_string(result);
}
