using Grpc.Core;
using grpcMessageServer;

namespace grpcServer.Services;

public class MessageService : Message.MessageBase
{
    // public override async Task SendMessage(MessageRequest request, IServerStreamWriter<MessageResponse> responseStream, ServerCallContext context)
    // {
    //     System.Console.WriteLine($"name : {request.Name} | message : {request.Message}");

    //     for (int i = 0; i < 10; i++)
    //     {
    //         await Task.Delay(200);
    //         await responseStream.WriteAsync(new MessageResponse{
    //             Message ="Merhaba" + i
    //         });
    //     }
    // }

    public override async Task<MessageResponse> SendMessage(IAsyncStreamReader<MessageRequest> requestStream, ServerCallContext context)
    {

        while (await requestStream.MoveNext(context.CancellationToken))
        {
            System.Console.WriteLine($"name : {requestStream.Current.Name} | message : {requestStream.Current.Message}");
        }

        return new MessageResponse{
            Message ="veri alındı"
        };
    }
}
