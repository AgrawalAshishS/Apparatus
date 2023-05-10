using Apparatus;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace ApparatusTests
{

    public class TaskExtensionTests
    {
        private async Task<int> ResultTestMethod()
        {
            return 1;
        }

        private async Task<int> ErrorTestMethod()
        {
            throw new MyException("my test");
        }

        [Fact]
        public void TaskResultPassCorrectly()
        {
            Assert.Equal(1, ResultTestMethod().Await());
        }

        [Fact]
        public void TaskExceptionIsVisibleCorrectly()
        {
            Assert.ThrowsAsync<MyException>(() => ErrorTestMethod());

            //Assert.That(() => .Await(),
            //    Throws.TypeOf<MyException>().With.Message.EqualTo("my test")
            //    );
        }

        [Fact]
        public void TaskWithHttpClient()
        {
            HttpClient client = new HttpClient();
            var result = CallYahoo(client).Await();
            Assert.NotNull(result);
            var content = ReadContent(result).Await();
            Assert.NotEmpty(content);
        }

        //[Fact]
        //public void SendGridEmailTrail()
        //{
        //    var client = new SendGrid.SendGridClient(""); // pull api key from somewhere.
        //    var msg = new SendGrid.Helpers.Mail.SendGridMessage();
        //    msg.AddTo("ashish@ideatoworking.com");
        //    msg.SetFrom("test@bluesombrero.com");
        //    msg.SetGlobalSubject("this is test");
        //    msg.HtmlContent = "this is content";

        //    //msg.SetSandBoxMode(true);

        //    SendGrid.Response response = client.SendEmailAsync(msg).Await();
        //    Assert.Equal("Accepted", response.StatusCode.ToString());
        //}

        private async Task<HttpResponseMessage> CallYahoo(HttpClient client)
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, "http://www.yahoo.com/");
            return await client.SendAsync(httpRequest).ConfigureAwait(false);
        }

        private async Task<string> ReadContent(HttpResponseMessage result)
        {
            return await result.Content.ReadAsStringAsync().ConfigureAwait(false);
        }
    }


    [System.Serializable]
    public class MyException : Exception
    {
        public MyException() { }
        public MyException(string message) : base(message) { }
        public MyException(string message, Exception inner) : base(message, inner) { }
        protected MyException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
