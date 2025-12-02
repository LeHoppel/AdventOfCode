#include <iostream>

#include "Day01/Day01.h"
#include "Day02/Day02.h"

int main() {
    auto firstPart = Day02::firstPart();
    auto secondPart = Day02::secondPart();
    std::cout << "Part 1: " << firstPart << std::endl;

    std::cout << "Part 2: " << secondPart << std::endl;
}
