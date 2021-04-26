//
//  hyphenateCWrapper.hpp
//  hyphenateCWrapper
//
//  Created by 杜洁鹏 on 2021/4/20.
//  Copyright © 2021 easemob. All rights reserved.
//

#pragma once
#if UNITY_METRO
#define EXPORT_API __declspec(dllexport) __stdcall
#elif UNITY_WIN
#define EXPORT_API __declspec(dllexport)
#else
#define EXPORT_API
#endif
#include <stdio.h>

