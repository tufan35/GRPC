﻿using Google.Protobuf;
using Grpc.Net.Client;
using grpcFileTransportClient;

class Program
{
    public static async Task Main(string[] args)
    {
        var channel = GrpcChannel.ForAddress("http://localhost:5296");

        var client = new FileService.FileServiceClient(channel);

        string file = @"C:\Users\TUFAN\Desktop\document.pdf";

        using FileStream fileStream = new FileStream(file, FileMode.Open);

        var content = new BytesContent
        {
            FileSize = fileStream.Length,
            ReadedByte = 0,
            Info = new grpcFileTransportClient.FileInfo { FileName = Path.GetFileNameWithoutExtension(fileStream.Name), FileExtension = Path.GetExtension(fileStream.Name) }
        };

        var upload = client.FileUpload();
        byte[] buffer = new byte[2048];

        while ((content.ReadedByte = await fileStream.ReadAsync(buffer, 0, buffer.Length)) > 0)
        {
            content.Buffer = ByteString.CopyFrom(buffer);
            await upload.RequestStream.WriteAsync(content);
        }
        await upload.RequestStream.CompleteAsync();
        fileStream.Close();
    }
}