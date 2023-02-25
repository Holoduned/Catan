using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Catan.Views;

namespace Catan.Models
{
    public class Server
    {
        HttpListener httpListener = new HttpListener();
        public Socket socket;

        public ServerStatus status = ServerStatus.Stop;
        private ServerSettings serverSettings = new ServerSettings();
        public async void Start()
        {
            if (status == ServerStatus.Start) return;

            httpListener.Prefixes.Add($"http://localhost:" + serverSettings.Port + "/");

            httpListener.Start();
            status = ServerStatus.Start;
            await Listening();
        }

        private async Task Listening()
        {
            try
            {
                if (httpListener == null)
                {
                    ServerMenu.Output("ERROR: Listener is null");
                    return;
                }

                ServerMenu.Output("Waiting for connection...");

                while (status == ServerStatus.Start)
                {
                    var context = await httpListener.GetContextAsync();
                    HttpListenerRequest request = context.Request;
                    HttpListenerResponse response = context.Response;

                    var body = request.InputStream;
                    var encoding = request.ContentEncoding;
                    var reader = new StreamReader(body, encoding);
                    var bodyContent = await reader.ReadToEndAsync();

                    ServerMenu.Output(bodyContent);

                    var responseText = "Sample request";
                    byte[] responseBuffer = Encoding.UTF8.GetBytes(responseText);

                    Stream output = response.OutputStream;
                    await output.WriteAsync(responseBuffer);

                    output.Close();
                    context.Response.Close();
                }
            }
            catch (HttpListenerException error)
            {
                ServerMenu.Output(error.Message);
            }
        }

        protected internal void Stop()
        {
            if (status == ServerStatus.Stop) return;

            httpListener.Stop();
            Console.WriteLine($"Server stopped");
            status = ServerStatus.Stop;
        }
    }
}
