using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyMessages;
using MyServer;
using NServiceBus.Testing;
using NServiceBus;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        public UnitTest1()
        {
        //Initialize the test in the constructor
            Test.Initialize();
        
        }




        [TestMethod]
            public void TestHandler()
        {
           // Test.Initialize();

            var dataId = Guid.NewGuid();
            var str = "hello";
            WireEncryptedString secret = "secret";

            Test.Handler<RequestDataMessageHandler>()
                .SetIncomingHeader("Test", "abc")
                .ExpectReply<DataResponseMessage>(m => m.DataId == dataId && m.String == str)
                .OnMessage<RequestDataMessage>(m => { m.DataId = dataId; m.String = str; });
        }


        [TestMethod]
        public void TestHeaderOverride()
        {
            

            var dataId = Guid.NewGuid();
            var str = "hello";
            WireEncryptedString secret = "secret";

            Test.Handler<RequestDataMessageHandler>()
                .SetIncomingHeader("Test", "override")
                .ExpectReply<DataResponseMessage>(m => m.DataId == dataId && m.String == "override")
                .OnMessage<RequestDataMessage>(m => { m.DataId = dataId; m.String = str; });
        }

    }
}
