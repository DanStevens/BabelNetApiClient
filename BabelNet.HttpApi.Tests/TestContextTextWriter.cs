using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabelNet.HttpApi.Tests
{
    internal class TestContextTextWriter : ITextWriter
    {
        public void Write(bool value) => TestContext.Write(value);

        public void Write(char value) => TestContext.Write(value);

        public void Write(char[] buffer) => TestContext.Write(buffer);

        public void Write(char[] buffer, int index, int count) => TestContext.Write(new string(buffer, index, count));

        public void Write(decimal value) => TestContext.Write(value);

        public void Write(double value) => TestContext.Write(value);

        public void Write(int value) => TestContext.Write(value);

        public void Write(long value) => TestContext.Write(value);

        public void Write(object value) => TestContext.Write(value);

        public void Write(ReadOnlySpan<char> buffer) => TestContext.Write(new string(buffer));

        public void Write(float value) => TestContext.Write(value);

        public void Write(string value) => TestContext.Write(value);

        public void Write(string format, object arg0) => TestContext.Write(format, arg0);

        public void Write(string format, object arg0, object arg1) => TestContext.Write(format, arg0, arg1);

        public void Write(string format, object arg0, object arg1, object arg2) => TestContext.Write(format, arg0, arg1, arg2);

        public void Write(string format, params object[] arg) => TestContext.Write(format, arg);

        public void Write(uint value) => TestContext.Write(value);

        public void Write(ulong value) => TestContext.Write(value);

        public void WriteLine() => TestContext.WriteLine();

        public void WriteLine(bool value) => TestContext.WriteLine(value);

        public void WriteLine(char value) => TestContext.WriteLine(value);

        public void WriteLine(char[] buffer) => TestContext.WriteLine(buffer);

        public void WriteLine(char[] buffer, int index, int count) => TestContext.WriteLine(new string(buffer, index, count));

        public void WriteLine(decimal value) => TestContext.WriteLine(value);

        public void WriteLine(double value) => TestContext.WriteLine(value);

        public void WriteLine(int value) => TestContext.WriteLine(value);

        public void WriteLine(long value) => TestContext.WriteLine(value);

        public void WriteLine(object value) => TestContext.WriteLine(value);

        public void WriteLine(ReadOnlySpan<char> buffer) => TestContext.WriteLine(new string(buffer));

        public void WriteLine(float value) => TestContext.WriteLine(value);

        public void WriteLine(string value) => TestContext.WriteLine(value);

        public void WriteLine(string format, object arg0) => TestContext.WriteLine(format, arg0);

        public void WriteLine(string format, object arg0, object arg1) => TestContext.WriteLine(format, arg0, arg1);

        public void WriteLine(string format, object arg0, object arg1, object arg2) => TestContext.WriteLine(format, arg0, arg1, arg2);

        public void WriteLine(string format, params object[] arg) => TestContext.WriteLine(format, arg);

        public void WriteLine(uint value) => TestContext.WriteLine(value);

        public void WriteLine(ulong value) => TestContext.WriteLine(value);
    }
}
