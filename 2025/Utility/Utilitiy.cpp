#include "Utilitiy.h"

#include <fstream>

namespace Utility {
    std::vector<std::string> readInput(const std::string& path) {
        std::ifstream file(path.c_str());
        std::string str = "";

        std::vector<std::string> input;
        while (std::getline(file, str)) {
            input.push_back(str);
        }

        return input;
    }
}
