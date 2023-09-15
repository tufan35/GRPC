// See https://aka.ms/new-console-template for more information

//kurulcak kütüphaneler
//dotnet add package Google.Protobuf --version 3.24.3
//dotnet add package Grpc.Net.Client --version 2.57.0
//dotnet add package Grpc.Tools --version 2.58.0


using Grpc.Net.Client;
using grpcFileTransportDownloadClient;

namespace Program
{
    class Program
    {

        static async Task Main(string[] args)
        {
            var channel = GrpcChannel.ForAddress("http://localhost:5296");
            var client = new FileService.FileServiceClient(channel);

            string downloadPath = @"E:\Dokumanlarim\C#\Grpc\grpcDownloadClient\DownloadFiles";

            var fileInfo = new grpcFileTransportDownloadClient.FileInfo
            {
                FileExtension = ".pdf",
                FileName = "document"
            };

            FileStream fileStream = null;
            var request = client.FileDownload(fileInfo);

            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

            int count = 0;
            decimal chunkSize =0;
            while (await request.ResponseStream.MoveNext(cancellationTokenSource.Token))
            {
                if (count++ == 0)
                {
                    fileStream = new FileStream(@$"{request.ResponseStream.Current.Info.FileName}{request.ResponseStream.Current.Info.FileExtension}", FileMode.CreateNew);
                    fileStream.SetLength(request.ResponseStream.Current.FileSize);
                }
                var buffer = request.ResponseStream.Current.Buffer.ToByteArray();
                await fileStream.WriteAsync(buffer, 0, request.ResponseStream.Current.ReadedByte);

                  System.Console.WriteLine($"{Math.Round(((chunkSize += request.ResponseStream.Current.ReadedByte) * 100)) / request.ResponseStream.Current.FileSize}%");
            }

            System.Console.WriteLine("Yüklendi ...");
            await fileStream.DisposeAsync();
            fileStream.Close();
        }
    }
}


