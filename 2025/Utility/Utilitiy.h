#ifndef INC_2025_UTILITIY_H
#define INC_2025_UTILITIY_H
#include <string>
#include <vector>

namespace Utility {
    std::vector<std::string> readInput(const std::string& path);

    std::vector<std::string> splitString(const std::string &str, char delimiter);
}

#endif