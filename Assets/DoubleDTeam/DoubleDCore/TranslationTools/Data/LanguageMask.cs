using System;

namespace DoubleDCore.TranslationTools.Data
{
    [Flags]
    public enum LanguageMask
    {
        Nothing = 0,
        Everything = -1,
        Ru = 1 << 0,
        En = 1 << 1,
        //Tr = 1 << 2
    }
}