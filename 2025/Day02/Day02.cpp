#include "Day02.h"

#include <iostream>
#include <ostream>
#include <vector>

#include "../Utility/Utilitiy.h"

namespace Day02 {
    float firstPart() {
        auto input = Utility::readInput(inputPath)[0];
        auto ranges = Utility::splitString(input, ',');

        long long result = 0;

        for (const auto& range : ranges) {
            auto startAndEnd = Utility::splitString(range, '-');
            std::cout << startAndEnd[0] << std::endl;
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

    float secondPart() {

        return 1;
    }
}
