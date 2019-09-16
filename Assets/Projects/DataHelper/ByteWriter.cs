using System;
using System.Text;

namespace DataHelper {
    public class ByteWriter {
        private byte[] _data;
        private int _pos;

        public byte[] Data {
            get {
                var res = new byte[_pos];
                Array.Copy(_data, res, _pos);
                return res;
            }
        }

        public ByteWriter(int capacity) {
            _data = new byte[capacity];
            _pos = 0;
        }

        private void EnsureCapacity(int size) {
            if (_pos + size <= _data.Length) {
                int newSize = size >= _data.Length ? size*2 : _data.Length*2;
                var data = new byte[newSize];
                Array.Copy(_data, data, _pos);
                _data = data;
            }
        }

        private void Write(byte[] bytes) {
            int size = bytes.Length;
            EnsureCapacity(size);
            Buffer.BlockCopy(bytes, 0, _data, _pos, size);
            _pos += size;
        }

        public void WriteBool(bool value) {
            Write(BitConverter.GetBytes(value));
        }

        public void WriteChar(char value) {
            Write(BitConverter.GetBytes(value));
        }

        public void WriteByte(byte value) {
            EnsureCapacity(1);
            _data[_pos++] = value;
        }

        public void WriteShort(short value) {
            Write(BitConverter.GetBytes(value));
        }

        public void WriteInt(int value) {
            Write(BitConverter.GetBytes(value));
        }

        public void WriteLong(long value) {
            Write(BitConverter.GetBytes(value));
        }

        public void WriteFloat(float value) {
            Write(BitConverter.GetBytes(value));
        }

        public void WriteDouble(double value) {
            Write(BitConverter.GetBytes(value));
        }

        private void WriteString(string s, Encoding enc) {
            var bytes = enc.GetBytes(s);
            WriteInt(bytes.Length);
            Write(bytes);
        }

        public void WriteAsciiString(string value) {
            WriteString(value, Encoding.ASCII);
        }

        public void WriteUtf8String(string value) {
            WriteString(value, Encoding.UTF8);
        }

        public void WriteUtf32String(string value) {
            WriteString(value, Encoding.UTF32);
        }
    }
}
