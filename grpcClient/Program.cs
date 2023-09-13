using Grpc.Net.Client;
using grpcMessageClient;
using grpcServer;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var channel = GrpcChannel.ForAddress("http://localhost:5296");
        var messageClient = new Message.MessageClient(channel);
        MessageResponse response = await messageClient.SendMessageAsync(new MessageRequest{
            Name ="Merhaba",
            Message ="Tufan"
        });

        System.Console.WriteLine(response.Message);

    //     var greatClient = new Greeter.GreeterClient(channel);
    //    HelloReply resut =  await greatClient.SayHelloAsync(new HelloRequest
    //     {
    //         Name = "Tufandan selamlar "
    //     });

    //     Console.WriteLine(resut.Message);
    }
}