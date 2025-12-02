#include "Day02.h"

#include <iostream>
#include <ostream>
#include <vector>

#include "../Utility/Utilitiy.h"

using namespace Day02;
double Day02::firstPart() {
    auto input = Utility::readInput(inputPath)[0];
    auto ranges = Utility::splitString(input, ',');

    long long result = 0;

    for (const auto& range : ranges) {
        auto startAndEnd = Utility::splitString(range, '-');

        long long start = std::stoll(startAndEnd[0]);
        long long end = std::stoll(startAndEnd[1]);

        for (long long i = start; i <= end; i++) {
            std::string current = std::to_string(i);
            if (current.length() % 2 == 1) continue;

            int half = current.length() / 2;

            bool invalid = true;
            for (int j = 0; j < half; j++) {
                if (current[j] != current[j+half]) {
                    invalid = false;
                    break;
                }
            }

            if (invalid) result += i;
        }
    }

    std::cout << result << std::endl;
    return result;
}

double Day02::secondPart() {
    auto input = Utility::readInput(inputPath)[0];

    auto ranges = Utility::splitString(input, ',');

    long long result = 0;

    for (const auto& range : ranges) {
        auto startAndEnd = Utility::splitString(range, '-');

        long long start = std::stoll(startAndEnd[0]);
        long long end = std::stoll(startAndEnd[1]);

        for (long long i = start; i <= end; i++) {
            std::string current = std::to_string(i);

            for (int sequenceLength = 1; sequenceLength <= current.length() / 2; sequenceLength++) {
                if (current.length() % sequenceLength != 0) continue;

                int fitting = current.length() / sequenceLength;

                bool invalidSequence = true;

                // j = index in the sequence
                // k = current multiple of the sequence
                for (int j = 0; j < sequenceLength; j++) {
                    for (int k = 1; k < fitting; k++) {
                        if (current[j] != current[j + k*sequenceLength])
                            invalidSequence = false;
                    }
                }
                if (invalidSequence) {
                    result += i;

                    break;
                }
            }
        }
    }

    std::cout << result << std::endl;
    return result;
}
