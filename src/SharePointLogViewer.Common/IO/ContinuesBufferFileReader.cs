using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SharePointLogViewer.Common.IO
{
    /// <summary>
    /// File reader that has an internal buffer that will be filled while waiting for reading the next data by the system.
    /// </summary>
    public class ContinuesBufferFileReader
    {
        public static int InternalBufferSize = 1024 * 1024 * 5; // 5 MB
        public static int BufferSize = 1024 * 1024 * 50; // 50 MB

        private readonly string _filePath;
        private readonly StaticBuffer _buffer;
        private readonly ManualResetEvent _bufferingResetEvent;
        private readonly ManualResetEvent _readingResetEvent;
        private Task _currentTask;
        private bool _internalReaderEndOfFile;

        /// <summary>
        /// Initializes a new instance of the ContinuesBufferFileReader class.
        /// </summary>
        /// <param name="filePath"></param>
        public ContinuesBufferFileReader(string filePath)
        {
            _filePath = filePath;
            _buffer = new StaticBuffer(BufferSize);
            _bufferingResetEvent = new ManualResetEvent(true);
            _readingResetEvent = new ManualResetEvent(false);
        }

        /// <summary>
        /// Value indicating whether the end of the file is reached.
        /// </summary>
        public bool EndOfFile { get; private set; }

        /// <summary>
        /// Read a single line from the file.
        /// </summary>
        /// <returns></returns>
        public string ReadLine()
        {
            if (_currentTask == null)
                StartBuffering();

            var localBuffer = new List<byte>(512); // 512 bytes
            
            byte currentByte;
            do
            {
                // Wait for the buffer to fill
                if (!_buffer.CanRead())
                {
                    if (_internalReaderEndOfFile)
                    {
                        this.EndOfFile = true;
                        break;
                    }
                    _readingResetEvent.Reset();
                    _readingResetEvent.WaitOne();
                }

                currentByte = _buffer.ReadByte();
                localBuffer.Add(currentByte);

            } while (currentByte != '\n');

            _bufferingResetEvent.Set();

            return Encoding.UTF8.GetString(localBuffer.ToArray()).TrimEnd('\r', '\n');
        }

        /// <summary>
        /// Starts reading the file.
        /// </summary>
        private void StartBuffering()
        {
            _currentTask = Task.Factory.StartNew(ReadContents);
        }

        /// <summary>
        /// Read the contents from the file.
        /// </summary>
        private void ReadContents()
        {
            Trace.WriteLine("Start reading contents from '" + _filePath + "'...");

            _internalReaderEndOfFile = false;
            this.EndOfFile = false;

            var buffer = new byte[InternalBufferSize];
            int length;

            using (var stream = new FileStream(_filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                while ((length = stream.Read(buffer, 0, InternalBufferSize)) > 0)
                {
                    // Wait if there is not enough room in the buffer
                    while (!_buffer.CanWrite(length))
                    {
                        _bufferingResetEvent.Reset();
                        // TODO: Set an timeout on this WAITING. If no time out, then the file can be locked for a long time.
                        _bufferingResetEvent.WaitOne();
                    }
                    // Copy the data from the internal buffer to the exposable buffer.
                    _buffer.Write(buffer, 0, length);

                    // The application can read now from the bufer.
                    _readingResetEvent.Set();
                }
                _internalReaderEndOfFile = true;
            }
            Trace.WriteLine("Done reading the contents.");
        }
    }
}
