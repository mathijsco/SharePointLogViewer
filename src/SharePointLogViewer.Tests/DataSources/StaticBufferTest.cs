using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharePointLogViewer.Common.IO;

namespace SharePointLogViewer.Tests.DataSources
{
    [TestClass]
    public class StaticBufferTest
    {
        [TestMethod]
        public void ReachSimpleLimit()
        {
            var buffer = new StaticBuffer(10);
            
            Assert.IsFalse(buffer.CanRead());
            buffer.Write(new byte[] { 0x05 }, 0, 1);
            Assert.IsTrue(buffer.CanRead());
            Assert.AreEqual(1, buffer.Size);
            Assert.AreEqual((byte)0x05, buffer.ReadByte());
            Assert.IsFalse(buffer.CanRead());
            Assert.AreEqual(0, buffer.Size);
        }

        [TestMethod]
        public void CanRead()
        {
            var buffer = new StaticBuffer(3);
            Assert.IsFalse(buffer.CanRead());
            buffer.Write(new byte[] { 0x01 }, 0, 1);
            Assert.IsTrue(buffer.CanRead());
            buffer.ReadByte();
            Assert.IsFalse(buffer.CanRead());
            buffer.Write(new byte[] { 0x01 }, 0, 1);
            buffer.Write(new byte[] { 0x01 }, 0, 1);
            Assert.IsTrue(buffer.CanRead());
            buffer.ReadByte();
            Assert.IsTrue(buffer.CanRead());
            buffer.ReadByte();
            Assert.IsFalse(buffer.CanRead());
            buffer.Write(new byte[] { 0x01 }, 0, 1);
            Assert.IsTrue(buffer.CanRead());
            buffer.ReadByte();
            Assert.IsFalse(buffer.CanRead());
        }

        [TestMethod]
        public void Write()
        {
            var buffer = new StaticBuffer(10);

            var b = new byte[]
            {
                0x00, 0x01, 0x02, 0x04, 0x08, 0x10, 0x20, 0x40, 0x80, 0xFF
            };

            buffer.Write(b, 0, b.Length);
            Assert.AreEqual(10, buffer.Size);
            // Try to write one more
            Assert.IsFalse(buffer.CanWrite(1));

            // Make place for 2 places
            buffer.ReadByte();
            buffer.ReadByte();
            Assert.AreEqual(8, buffer.Size);

            // Can write 2 bytes and not 3
            Assert.IsTrue(buffer.CanWrite(2));
            Assert.IsFalse(buffer.CanWrite(3));

            // Write 1
            buffer.Write(new byte[] { 0x12 }, 0, 1);
            Assert.IsFalse(buffer.CanWrite(2));

            // Check the outcome
            var priObj = new PrivateObject(buffer);
            var originalBuffer = (byte[])priObj.GetField("_buffer");
            Assert.IsTrue(new byte[] { 0x12, 0x01, 0x02, 0x04, 0x08, 0x10, 0x20, 0x40, 0x80, 0xFF }.SequenceEqual(originalBuffer));
            Assert.AreEqual(1, (int)priObj.GetField("_currentPosWrite"));
            Assert.AreEqual(2, (int)priObj.GetField("_currentPosRead"));
            Assert.AreEqual(9, buffer.Size);
        }

        [TestMethod]
        public void ReadBlock()
        {
            var buffer = new StaticBuffer(10);
            buffer.Write(new byte[] { 0x09, 0x09, 0x09, 0x09, 0x02, 0x01, 0x01, 0x01, 0x01 }, 0, 9);
            buffer.ReadByte();
            buffer.ReadByte();
            buffer.ReadByte();
            buffer.ReadByte();

            buffer.Write(new byte[] { 0x01, 0x01, 0x02, 0x03 }, 0, 4);

            var newBuffer = new byte[8];
            buffer.ReadBlock(newBuffer, 0, 8);
            Assert.IsTrue(newBuffer.SequenceEqual(new byte[] {0x02, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x02}));
        }
    }
}
