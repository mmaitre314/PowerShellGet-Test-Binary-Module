using System;
using System.Management.Automation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Module;
using Moq;

namespace Tests
{
    [TestClass]
    public class HelloWorldCmdletTests
    {
        [TestMethod]
        public void Test()
        {
            var mock = new Mock<ICommandRuntime>();

            var cmdlet = new HelloWorldCmdlet
            {
                CommandRuntime = mock.Object
            };
            new PrivateObject(cmdlet).Invoke("BeginProcessing");

            mock.Verify(runtime => runtime.WriteObject("Hello, World!"), Times.Once);
        }
    }
}
