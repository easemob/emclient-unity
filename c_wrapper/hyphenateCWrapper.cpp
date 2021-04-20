//
//  hyphenateCWrapper.cpp
//  hyphenateCWrapper
//
//  Created by 杜洁鹏 on 2021/4/20.
//  Copyright © 2021 easemob. All rights reserved.
//

#include "hyphenateCWrapper.hpp"

extern "C" int EXPORT_API TestAB(int a,int b)
{
    return a+b;
}

extern "C"
{
    int TestAC(int a);

}

int TestAC(int w )
{
    return w * 10;

}

