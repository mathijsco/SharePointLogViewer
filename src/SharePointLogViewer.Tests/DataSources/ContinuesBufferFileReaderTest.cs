using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharePointLogViewer.Common.IO;

namespace SharePointLogViewer.Tests.DataSources
{
    [TestClass]
    public class ContinuesBufferFileReaderTest
    {
        [TestMethod]
        public void ReadFile()
        {
            ContinuesBufferFileReader.InternalBufferSize = 50;
            ContinuesBufferFileReader.BufferSize = 250;

            var reader = new ContinuesBufferFileReader(@"D:\Documenten\Visual Studio 2012\Projects\SharePointLogViewer\SharePointLogViewer.Tests\ExternalFiles\SomeLargeLog.txt");
            
            //var priObj = new PrivateObject(reader);
            
            string prevLine = string.Empty;
            int lines = 24;
            while (!reader.EndOfFile)
            {
                var line = reader.ReadLine();
                Assert.AreNotEqual(prevLine, line);
                Assert.IsTrue(line.EndsWith("."), "The line does not end with a dot.");

                lines--;
            }
            Assert.AreEqual(0, lines);
        }
    }
}
