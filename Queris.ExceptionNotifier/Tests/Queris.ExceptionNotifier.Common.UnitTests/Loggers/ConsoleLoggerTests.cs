using System;
using System.IO;
using FluentAssertions;
using NUnit.Framework;
using Queris.ExceptionNotifier.Common.Loggers;

namespace Queris.ExceptionNotifier.Common.UnitTests.Loggers
{
    [TestFixture]
    [Category("Queris.ExceptionNotifier.Common.UnitTests")]
    public class ConsoleLoggerTests
    {
        private StringWriter _writer;
        private const string Param = "PARAMETERS TEST";

        [SetUp]
        public void Init()
        {
            _writer = new StringWriter();

            Console.SetOut(_writer);

            _writer.Flush();
        }

        [Test]
        [Category("ConsoleLoggerTests.Info.UnitTests")]
        public void Info_SendInfoWithoutParamList_ReturnInfo()
        {
            var logger = new ConsoleLogger();

            logger.Info("INFO");

            var result = _writer.GetStringBuilder().ToString();

            result.Should().Match("INFO: INFO\r\n");
        }

        [Test]
        [Category("ConsoleLoggerTests.Info.UnitTests")]
        public void Info_SendInfoWithParamList_ReturnInfoWithParam()
        {
            var logger = new ConsoleLogger();

            logger.Info("INFO {0}", Param);

            var result = _writer.GetStringBuilder().ToString();

            result.Should().Match("INFO: INFO PARAMETERS TEST\r\n");
        }

        [Test]
        [Category("ConsoleLoggerTests.Debug.UnitTests")]
        public void Debug_SendDebugWithoutParamList_ReturnDebug()
        {
            var logger = new ConsoleLogger();

            logger.Debug("DEBUG");

            var result = _writer.GetStringBuilder().ToString();

            result.Should().Match("DEBUG: DEBUG\r\n");
        }

        [Test]
        [Category("ConsoleLoggerTests.Debug.UnitTests")]
        public void Debug_SendDebugWithParamList_ReturnDebugWithParam()
        {
            var logger = new ConsoleLogger();

            logger.Debug("DEBUG {0}", Param);

            var result = _writer.GetStringBuilder().ToString();

            result.Should().Match("DEBUG: DEBUG PARAMETERS TEST\r\n");
        }

        [Test]
        [Category("ConsoleLoggerTests.Warning.UnitTests")]
        public void Warning_SendWarningWithoutParamList_ReturnWarning()
        {
            var logger = new ConsoleLogger();

            logger.Warning("WARNING");

            var result = _writer.GetStringBuilder().ToString();

            result.Should().Match("WARNING: WARNING\r\n");
        }

        [Test]
        [Category("ConsoleLoggerTests.Warning.UnitTests")]
        public void Warning_SendWarningWithParamList_ReturnWarningWithParam()
        {
            var logger = new ConsoleLogger();

            logger.Warning("WARNING {0}", Param);

            var result = _writer.GetStringBuilder().ToString();

            result.Should().Match("WARNING: WARNING PARAMETERS TEST\r\n");
        }

        [Test]
        [Category("ConsoleLoggerTests.Error.UnitTests")]
        public void Error_SendErrorWithoutParamList_ReturnError()
        {
            var logger = new ConsoleLogger();

            logger.Error("ERROR");

            var result = _writer.GetStringBuilder().ToString();

            result.Should().Match("ERROR: ERROR\r\n");
        }

        [Test]
        [Category("ConsoleLoggerTests.Error.UnitTests")]
        public void Error_SendErrorWithParamList_ReturnErrorWithParam()
        {
            var logger = new ConsoleLogger();

            logger.Error("ERROR {0}", Param);

            var result = _writer.GetStringBuilder().ToString();

            result.Should().Match("ERROR: ERROR PARAMETERS TEST\r\n");
        }
    }
}
