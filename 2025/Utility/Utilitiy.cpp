#include "Utilitiy.h"

#include <fstream>
#include <sstream>

namespace Utility {
    std::vector<std::string> readInput(const std::string &path) {
        std::ifstream file(path.c_str());
        std::string str = "";

        std::vector<std::string> input;
        while (std::getline(file, str)) {
            input.push_back(str);
        }

        return input;
    }

    std::vector<std::string> splitString(const std::string &str, char delimiter) {
        std::vector<std::string> result;
        std::stringstream stringStream(str);
        std::string item;

        while (getline(stringStream, item, delimiter)) {
            result.push_back(item);
        }

        return result;
    }
}
