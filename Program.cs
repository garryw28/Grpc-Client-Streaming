using System;
using System.Net.Http;
using System.Threading.Tasks;
using Grpc.Net.Client;
using Grpc.Core;

namespace GrpcClientStreaming
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client =  new Greeter.GreeterClient(channel);
            using (var call = client.CallStream(new StreamRequest{Request = "test"}))
            {
                while(await call.ResponseStream.MoveNext())
                {
                    StreamContent content = call.ResponseStream.Current;
                    Console.WriteLine($"Id : {content.Id}, Name : {content.Name}, Contact : {content.Contact}, Time : {DateTime.Now.ToString("dd-MM-yy HH:mm:ss")}");
                }
            }
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
