#include <chrono>
#include <iostream>

#include "Day01/Day01.h"
#include "Day02/Day02.h"
#include "Day03/Day03.h"
#include "Day04/Day04.h"

int main() {
    auto start = std::chrono::high_resolution_clock::now();
    auto firstPart = Day04::firstPart();
    auto finish = std::chrono::high_resolution_clock::now();
    auto milliseconds = std::chrono::duration_cast<std::chrono::milliseconds>(finish - start);

    std::cout << "Part 1: " << firstPart << std::endl;
    std::cout << std::format("Calculation time: {} ms.", milliseconds.count()) << std::endl << std::endl;

    start = std::chrono::high_resolution_clock::now();
    auto secondPart = Day04::secondPart();
    finish = std::chrono::high_resolution_clock::now();
    milliseconds = std::chrono::duration_cast<std::chrono::milliseconds>(finish-start);

    std::cout << "Part 2: " << secondPart << std::endl;
    std::cout << std::format("Calculation time: {} ms.", milliseconds.count()) << std::endl << std::endl;
}
