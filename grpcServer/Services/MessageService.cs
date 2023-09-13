using Grpc.Core;
using grpcMessageServer;

namespace grpcServer.Services;

public class MessageService : Message.MessageBase
{
    public override async Task<MessageResponse> SendMessage(MessageRequest request, ServerCallContext context)
    {
        System.Console.WriteLine($" name : {request.Name} | Message : {request.Message}");
        return new MessageResponse
        {
            Message = " Mesaj başarıyla alındı."
        };
    }
}
