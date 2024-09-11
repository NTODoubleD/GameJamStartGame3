using System;
using System.Collections.Generic;

namespace DoubleDCore.Cryptography
{
    public class CRC32
    {
        private readonly uint[] _table;
        private const uint Poly = 0xedb88320;

        public CRC32()
        {
            _table = new uint[256];
            for (uint i = 0; i < _table.Length; ++i)
            {
                var temp = i;
                for (var j = 8; j > 0; --j)
                    if ((temp & 1) == 1)
                        temp = (temp >> 1) ^ Poly;
                    else
                        temp >>= 1;
                _table[i] = temp;
            }
        }

        public IEnumerable<byte> ComputeHash(IEnumerable<byte> bytes)
        {
            return BitConverter.GetBytes(_ComputeHash(bytes));
        }

        private uint _ComputeHash(IEnumerable<byte> bytes)
        {
            var crc = 0xffffffff;
            foreach (var t in bytes)
            {
                var index = (byte)((crc & 0xff) ^ t);
                crc = (crc >> 8) ^ _table[index];
            }

            return ~crc;
        }
    }
}