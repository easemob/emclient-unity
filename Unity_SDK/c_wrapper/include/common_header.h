#ifndef COMMON_HEADER_H
#define COMMON_HEADER_H

#ifdef _WIN32
#define DLLEXPORT _declspec(dllexport)
#else
#define DLLEXPORT extern
#endif

#endif //COMMON_HEADER_H