using System;
using System.Text;

namespace DataHelper {
    public class ByteReader {
        private readonly byte[] _data;
        private int _pos;
        public readonly int Length;

        public ByteReader(byte[] data) {
            _data = data;
            Length = _data.Length;
        }

        private T Read<T>(Func<byte[], int, T> toType, int size) {
            var res = toType(_data, _pos);
            _pos += size;
            return res;
        }

        public bool ReadBool() {
            return Read(BitConverter.ToBoolean, 1);
        }

        public char ReadChar() {
            return Read(BitConverter.ToChar, 2);
        }

        public byte ReadByte() {
            return _data[_pos++];
        }

        public short ReadShort() {
            return Read(BitConverter.ToInt16, 2);
        }

        public int ReadInt() {
            return Read(BitConverter.ToInt32, 4);
        }

        public long ReadLong() {
            return Read(BitConverter.ToInt64, 8);
        }

        public float ReadFloat() {
            return Read(BitConverter.ToSingle, 4);
        }

        public double ReadDouble() {
            return Read(BitConverter.ToDouble, 8);
        }

        public string ReadString(Encoding enc) {
            int l = ReadInt();
            string res = enc.GetString(_data, _pos, l);
            _pos += l;
            return res;
        }

        public string ReadAsciiString() {
            return ReadString(Encoding.ASCII);
        }

        public string ReadUtf8String() {
            return ReadString(Encoding.UTF8);
        }

        public string ReadUtf32String() {
            return ReadString(Encoding.UTF32);
        }
    }
}
