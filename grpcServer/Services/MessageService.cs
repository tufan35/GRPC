using Grpc.Core;
using grpcMessageServer;

namespace grpcServer.Services;

public class MessageService : Message.MessageBase
{
    public override async Task SendMessage(MessageRequest request, IServerStreamWriter<MessageResponse> responseStream, ServerCallContext context)
    {
        System.Console.WriteLine($"name : {request.Name} | message : {request.Message}");

        for (int i = 0; i < 10; i++)
        {
            await Task.Delay(200);
            await responseStream.WriteAsync(new MessageResponse{
                Message ="Merhaba" + i
            });
        }
    }
}
