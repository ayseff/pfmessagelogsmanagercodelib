//****************************************************************************************************
//
// Copyright © ProFast Computing 2012-2014
//
//****************************************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PFRandomData
{
#pragma warning disable 1591

    public enum enRandomNumberType
    {
        enInt = 0,
        enUInt = 1,
        enLong = 2,
        enULong = 3,
        enShort = 4,
        enUShort = 5,
        enSByte = 6,
        enByte = 7,
        enDouble = 8,
        enFloat = 9,
        enDecimal = 10,
        enUnknown = 99
    }

    public enum enRandomOffsetType
    {
        enYears = 0,
        enMonths = 1,
        enDays = 2,
        enMinutes = 3,
        enSeconds = 4,
        enUnknown = 99
    }

    public enum enRandomStringType
    {
        enAN = 0,
        enANUC = 1,
        enANLC = 2,
        enANX = 3,
        enAL = 4,
        enLC = 5,
        enUC = 6,
        enDEC = 7,
        enHEX = 8,
        enUnknown = 99
    }

    public enum enRandomSyllableStringType
    {
        enUCLC = 0,
        enLC = 1,
        enUC = 2,
        enUnknown = 99
    }

#pragma warning restore 1591

}//end namespace