using System;
using System.Threading;

namespace SharePointLogViewer.Common.IO
{
    /// <summary>
    /// Buffer class with a fixed size.
    /// </summary>
    public class StaticBuffer
    {
        private const int DefaultBufferSize = 1024 * 1024 * 50; // 50 MB

        private readonly int _bufferSize;
        private readonly byte[] _buffer;
        private int _currentPosWrite;
        private int _currentPosRead;
        private int _bufferUsed;

        /// <summary>
        /// Creates a new buffer with a 50MB limit.
        /// </summary>
        public StaticBuffer() : this(DefaultBufferSize) { }

        /// <summary>
        /// Creates a new buffer with the specified limit.
        /// </summary>
        /// <param name="bufferSize"></param>
        public StaticBuffer(int bufferSize)
        {
            _buffer = new byte[bufferSize];
            _bufferSize = bufferSize;
        }

        /// <summary>
        /// The size that is currently used by the buffer.
        /// </summary>
        public int Size
        {
            get { return _bufferUsed; }
        }

        /// <summary>
        /// Check if there is space left for the specified amount of bytes.
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public bool CanWrite(int length = 1)
        {
            return _bufferSize - _bufferUsed >= length;
        }

        /// <summary>
        /// Check if the specified length is stored in the buffer.
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public bool CanRead(int length = 1)
        {
            return _bufferUsed >= length;
        }

        /// <summary>
        /// Write the array to the buffer.
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        /// <param name="length"></param>
        public void Write(byte[] array, int index = 0, int length = -1)
        {
            if (length == -1) length = array.Length;

            if (!CanWrite(length))
                throw new ArgumentOutOfRangeException("length", @"The buffer is to small to write the buffer.");
            if (array.Length < index + length)
                throw new ArgumentOutOfRangeException("length", @"The selected range does not match the specified array length.");

            // Copy the data from the internal buffer to the exposable buffer.
            for (int i = index; i < (length + index); )
            {
                var maxRead = _bufferSize - _currentPosWrite;
                if (maxRead > length - i)
                    maxRead = length - i;

                // Copy the data
                Array.Copy(array, i, _buffer, _currentPosWrite, maxRead);
                Interlocked.Add(ref _bufferUsed, maxRead);

                _currentPosWrite = (_currentPosWrite + maxRead) % _bufferSize;
                i += maxRead;
            }
        }

        /// <summary>
        /// Reads a single byte from the buffer.
        /// </summary>
        /// <returns></returns>
        public byte ReadByte()
        {
            if (!CanRead())
                throw new OperationCanceledException(@"Cannot read from the buffer because it's empty.");

            var b = _buffer[_currentPosRead];
            _currentPosRead = (_currentPosRead + 1) % _bufferSize;
            Interlocked.Add(ref _bufferUsed, -1);
            return b;
        }

        /// <summary>
        /// Reads a specific amount of data from the internal buffer and write it to the specified output buffer.
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="index"></param>
        /// <param name="length"></param>
        public void ReadBlock(byte[] buffer, int index = 0, int length = -1)
        {
            if (length == -1) length = buffer.Length;

            if (!CanRead(length))
                throw new OperationCanceledException(@"Cannot read from the buffer because it's empty.");

            for (int i = index; i < (index + length); )
            {
                var maxRead = _bufferSize - _currentPosRead;
                if (maxRead > length - i)
                    maxRead = length - i;
                // Copy the data
                Array.Copy(_buffer, _currentPosRead, buffer, i, maxRead);

                Interlocked.Add(ref _bufferUsed, -maxRead);
                _currentPosRead = (_currentPosRead + maxRead)%_bufferSize;
                i += maxRead;
            }
        }
    }
}
