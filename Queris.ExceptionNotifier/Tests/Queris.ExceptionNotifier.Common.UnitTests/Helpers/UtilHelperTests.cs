using FluentAssertions;
using NUnit.Framework;
using Queris.ExceptionNotifier.Common.Enums;
using Queris.ExceptionNotifier.Common.Helpers;
using Queris.ExceptionNotifier.Common.Loggers;
using System;
using System.IO;
using System.Reflection;

namespace Queris.ExceptionNotifier.Common.UnitTests.Helpers
{
    [TestFixture]
    [Category("Queris.ExceptionNotifier.Common.UnitTests")]
    public class UtilHelperTests
    {
        private StringWriter _writer;

        [SetUp]
        public void Init()
        {
            _writer = new StringWriter();

            Console.SetOut(_writer);

            _writer.Flush();
        }

        [Test]
        [Category("UtilHelperTests.GetMethodName.UnitTests")]
        public void GetMethodName_MethodNameIsNull_ReturnUnknownMethod()
        {
            UtilHelper.GetMethodName(null).Should().Match("!MethodUnknown!");
        }

        [Test]
        [Category("UtilHelperTests.GetMethodName.UnitTests")]
        public void GetMethodName_MethodNameIsNotNull_ReturnMethodName()
        {
            UtilHelper.GetMethodName(MethodBase.GetCurrentMethod()).Should()
                .Match("Queris.ExceptionNotifier.Common.UnitTests.Helpers.UtilHelperTests::GetMethodName_MethodNameIsNotNull_ReturnMethodName");
        }

        [Test]
        [Category("UtilHelperTests.GetAssemblyName.UnitTests")]
        public void GetAssemblyName_AssemblyName_ReturnAssemblyName()
        {
            UtilHelper.GetAssemblyName<UtilHelperTests>(AssemblyValue.Name).Should().Match("UtilHelperTests");
        }

        [Test]
        [Category("UtilHelperTests.GetAssemblyName.UnitTests")]
        public void GetAssemblyName_AssemblyFullName_ReturnAssemblyFullName()
        {
            UtilHelper.GetAssemblyName<UtilHelperTests>(AssemblyValue.FullName).Should()
                .Match("Queris.ExceptionNotifier.Common.UnitTests.Helpers.UtilHelperTests");
        }

        [Test]
        [Category("UtilHelperTests.IsAppRunning.UnitTests")]
        public void IsAppRunning_AppNotRunning_ReturnFalse()
        {
            var logger = new ConsoleLogger();

            UtilHelper.IsAppRunning(logger).Should().BeFalse();
        }
    }
}
