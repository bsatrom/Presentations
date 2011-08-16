// <copyright file="Client.cs" company="Microsoft Corporation">
//   Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>

namespace Microsoft.ServiceModel.WebSockets.Samples
{
    using System;
    using System.Diagnostics;
    using System.Globalization;
    using System.IO;
    using System.Net.Sockets;
    using System.Text;
    using System.Threading;

    /// <summary>
    /// Simple WebSocket chat client
    /// </summary>
    internal class Client : IDisposable
    {
        private static string longGreeting = "01234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789";

        private static byte[][] v75 = new byte[][]
        {
            Encoding.UTF8.GetBytes("\r"),
            Encoding.UTF8.GetBytes("\n")
        };

        private static byte[][] v76 = new byte[][]
        {
            Encoding.UTF8.GetBytes("Sec-WebSocket-Key1: 18x 6]8vM;54 *(5:  {   U1]8  z [  8\r\n"),
            Encoding.UTF8.GetBytes("Sec-WebSocket-Key2: 1_ tx7X d  <  nw  334J702) 7]o}` 0\r\n"),
            Encoding.UTF8.GetBytes("\r"),
            Encoding.UTF8.GetBytes("\n"),

            new byte[] { 0x57, 0x6A, 0x4E, 0x7D },
            new byte[] { 0x7C, 0x4D },
            new byte[] { 0x28, 0x36 },
        };

        private static byte[][] v03 = new byte[][]
        {
            Encoding.UTF8.GetBytes("Origin: http://value:8080/foo/bar\r\n"),
            Encoding.UTF8.GetBytes("Sec-WebSocket-Draft: 3\r\n"),
            Encoding.UTF8.GetBytes("Sec-WebSocket-Nonce: A23F2BCA452DDE01\r\n"),
            Encoding.UTF8.GetBytes("\r"),
            Encoding.UTF8.GetBytes("\n"),

            new byte[] { 0x8F, 0x10, 0x33, 0x12, 0x20, 0x35, 0xD1, 0x12, 0x58, 0xFD, 0x7C, 0x76, 0x43, 0x14, 0x58, 0x0e, 0x53, 0x9c }
         };

        private static byte[][] intro = new byte[][]
        {
            new byte[] { 0x00, 0x22, 0x41, 0x22, 0xFF, 0x00 },
            new byte[] { 0x22, 0x42, 0x22, 0xFF, 0x00, 0x22, 0x43, 0x22, 0xFF, 0x00, 0x22, 0x44, 0x44, 0x44, 0x44, 0x44 },
            new byte[] { 0x44, 0x44, 0x44, 0x44, 0x44, 0x22, 0xFF, 0x00, 0x22, 0x45, 0x45, 0x22, 0xFF },

            new byte[] { 0x00, 0x22, 0x46, 0x47, 0x48 },
            new byte[] { 0x49 },
            new byte[] { 0x4A },
            new byte[] { 0x4B },
            new byte[] { 0x4C },
            new byte[] { 0x22, 0xFF },

            new byte[] { 0x00, 0x22, 0x4D, 0x4D, 0x22, 0xFF },
            new byte[] { 0x00, 0x22, 0x4E, 0x4E, 0x4E, 0x22, 0xFF },
            new byte[] { 0x00, 0x22, 0x4F, 0x4F, 0x4F, 0x4F, 0x22, 0xFF },
            new byte[] { 0x00, 0x22, 0x50, 0x50, 0x50, 0x50, 0x50, 0x22, 0xFF },
            new byte[] { 0x00, 0x22, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x22, 0xFF },
            new byte[] { 0x00, 0x22, 0x52, 0x52, 0x52, 0x52, 0x52, 0x52, 0x52, 0x22, 0xFF },
            new byte[] { 0x00, 0x22, 0x53, 0x53, 0x53, 0x53, 0x53, 0x53, 0x53, 0x53, 0x22, 0xFF },
        };

        private static byte[][] loop = new byte[][]
        {
            new byte[] { 0x00, 0x22, 0x54, 0x22, 0xFF, 0x00 },
            new byte[] { 0x22, 0x54, 0x22, 0xFF, 0x00, 0x22 },
            new byte[] { 0x54, 0x22, 0xFF, 0x00, 0x22, 0x54 },
            new byte[] { 0x22, 0xFF, 0x00, 0x22, 0x54, 0x22 },
            new byte[] { 0xFF, 0x00, 0x22, 0x54, 0x22, 0xFF },
        };

        private static byte frameBegin = 0x00;
        private static byte frameEnd = 0xFF;
        private static byte[] close = new byte[] { Client.frameEnd, Client.frameBegin };
        private static char[] separators = new char[] { Convert.ToChar(Client.frameBegin), Convert.ToChar(Client.frameEnd), '"' };

        private static string hello = "Hello from client";
        private static string goodbye = "Goodbye from client";

        // Client index counter
        private static int globalID = 0;

        private byte[][] http = new byte[][]
        {
            Encoding.UTF8.GetBytes("G"),
            Encoding.UTF8.GetBytes("ET "),
            Encoding.UTF8.GetBytes(string.Empty),                             // Place for request URI
            Encoding.UTF8.GetBytes(" HTTP/1."),
            Encoding.UTF8.GetBytes("1"),
            Encoding.UTF8.GetBytes("\r"),
            Encoding.UTF8.GetBytes("\n"),
            Encoding.UTF8.GetBytes(string.Empty),                             // Place for host header
            Encoding.UTF8.GetBytes("\r\n"),
            Encoding.UTF8.GetBytes("Upgrade:"),
            Encoding.UTF8.GetBytes("   WebSocket\r\n"),
            Encoding.UTF8.GetBytes("connection: upgrade\r\n"),
        };

        private byte[][] versionSpecific;
        private byte[] inputData = new byte[2048];

        private bool isDisposed;
        private bool done;

        private Uri address;
        private int id;

        private Timer timer;
        private int timeDelay = 50;
        private int timeDeviation = 20;

        private ClientState state;
        private int offset;
        private int iteration;

        private string lastMessageRead;
        private object lockThis = new object();

        private TcpClient client;
        private Stream stream;
        private int steadyCounter;

        /// <summary>
        /// Initializes a new instance of the <see cref="Client"/> class.
        /// </summary>
        /// <param name="address">URI of chat service to talk to.</param>
        public Client(Uri address)
        {
            if (address == null)
            {
                throw new ArgumentNullException("address");
            }

            this.id = Interlocked.Increment(ref Client.globalID);
            this.address = address;

            this.http[2] = Encoding.UTF8.GetBytes(address.PathAndQuery);
            this.http[7] = Encoding.UTF8.GetBytes(string.Format(CultureInfo.InvariantCulture, "Host: {0}:{1}", address.Host, address.Port));

            switch (Program.Config.GetClientVersion())
            {
                case WebSocketVersion.v75:
                    this.versionSpecific = Client.v75;
                    break;

                case WebSocketVersion.v76:
                    this.versionSpecific = Client.v76;
                    break;

                case WebSocketVersion.v03:
                    this.versionSpecific = Client.v03;
                    break;

                default:
                    throw new NotImplementedException();
            }

            Program.LogVerbose("Request URI: {0}", address.AbsolutePath);
        }

        private enum ClientState
        {
            HttpRequest = 0,
            WebSocketHandshake,
            IntroMessages,
            FrameLoop,
            SteadyState,
            Close
        }

        /// <summary>
        /// Starts communicating with the chat service.
        /// </summary>
        public void Communicate()
        {
            var host = this.address.DnsSafeHost;
            var port = this.address.Port;

            try
            {
                Program.LogVerbose("Connecting to {0} on {1}", host, port);

                this.client = new TcpClient(host, port);
                this.stream = this.client.GetStream();

                this.stream.BeginRead(this.inputData, 0, this.inputData.Length, Client.ReadDataCallback, this);
                this.timer = new Timer(new TimerCallback(Client.WriteData), this, SampleValues.GetNextSample(this.timeDelay, this.timeDeviation), Timeout.Infinite);
            }
            catch (Exception e)
            {
                Program.LogError("Connect exception: {0} - restarting", e.Message);
                Program.RemoveClient(this);
            }
        }

        /// <summary>
        /// Dispose this instance.
        /// </summary>
        public void Dispose()
        {
            this.isDisposed = true;
            if (this.stream != null)
            {
                this.stream.Close();
            }

            if (this.client != null)
            {
                this.client.Close();
            }

            if (this.timer != null)
            {
                this.timer.Dispose();
            }
        }

        private static void ReadDataCallback(IAsyncResult result)
        {
            var instance = result.AsyncState as Client;
            if (instance.isDisposed)
            {
                return;
            }

            try
            {
                var bytesRead = instance.stream.EndRead(result);
                if (bytesRead > 0)
                {
                    var content = Encoding.UTF8.GetString(instance.inputData, 0, bytesRead);

                    Program.LogVerbose("Client {0:D3}: Read  {1} bytes: {2}", instance.id, bytesRead, content);

                    if (instance.state == ClientState.SteadyState &&
                        content[0] == Client.frameBegin &&
                        content.IndexOf(Client.hello, StringComparison.Ordinal) < 0 &&
                        content.IndexOf(Client.goodbye, StringComparison.Ordinal) < 0)
                    {
                        var segments = content.Split(Client.separators);
                        if (segments.Length >= 2)
                        {
                            instance.SetLastMessageRead(segments[segments.Length - 2]);
                        }
                    }

                    if (instance.isDisposed)
                    {
                        return;
                    }

                    instance.stream.BeginRead(instance.inputData, 0, instance.inputData.Length, Client.ReadDataCallback, instance);
                }
                else
                {
                    Program.LogVerbose("Reading complete.");
                }
            }
            catch (Exception e)
            {
                Program.LogError("Read exception: {0} - restarting", e.Message);
                Program.RemoveClient(instance);
            }
        }

        private static void WriteData(object state)
        {
            var instance = state as Client;
            if (instance.isDisposed)
            {
                return;
            }

            try
            {
                byte[] outputData = null;

                switch (instance.state)
                {
                    case ClientState.HttpRequest:
                        if (instance.offset < instance.http.Length)
                        {
                            outputData = instance.http[instance.offset++];
                        }

                        if (instance.offset >= instance.http.Length)
                        {
                            instance.offset = 0;
                            instance.state = ClientState.WebSocketHandshake;
                        }

                        break;

                    case ClientState.WebSocketHandshake:
                        if (instance.offset < instance.versionSpecific.Length)
                        {
                            outputData = instance.versionSpecific[instance.offset++];
                        }

                        if (instance.offset >= instance.versionSpecific.Length)
                        {
                            if (Program.Config.SteadystateOnly)
                            {
                                instance.InitializeSteadyState();
                            }
                            else
                            {
                                instance.offset = 0;
                                instance.state = ClientState.IntroMessages;
                            }
                        }

                        break;

                    case ClientState.IntroMessages:
                        if (instance.offset < Client.intro.Length)
                        {
                            outputData = Client.intro[instance.offset++];
                        }

                        if (instance.offset >= Client.intro.Length)
                        {
                            instance.offset = 0;
                            instance.state = ClientState.FrameLoop;
                        }

                        break;

                    case ClientState.FrameLoop:
                        if (instance.iteration < Program.Config.LoopCount)
                        {
                            outputData = Client.loop[instance.offset++];
                            instance.offset %= Client.loop.Length;
                            if (instance.offset == 0)
                            {
                                instance.iteration++;
                            }
                        }
                        else
                        {
                            instance.InitializeSteadyState();
                            goto case ClientState.SteadyState;
                        }

                        break;

                    case ClientState.SteadyState:
                        if (instance.iteration < Program.Config.SteadystateCount - 1)
                        {
                            outputData = Client.CreateSteadyStateMessage(instance, Client.hello);
                            instance.iteration++;
                        }
                        else
                        {
                            outputData = Client.CreateSteadyStateMessage(instance, Client.goodbye);
                            instance.done = true;
                        }

                        break;

                    case ClientState.Close:
                        outputData = Client.close;
                        break;
                }

                Program.LogVerbose("Client {0:D3}: Write {1} bytes: {2}", instance.id, outputData.Length, Encoding.UTF8.GetString(outputData, 0, outputData.Length));
                Debug.Assert(outputData != null, "Output data not set");
                instance.stream.BeginWrite(outputData, 0, outputData.Length, Client.WriteDataCallback, instance);
            }
            catch (Exception e)
            {
                Program.LogError("Write exception: {0} - restarting", e.Message);
                Program.RemoveClient(instance);
            }
        }

        private static void WriteDataCallback(IAsyncResult result)
        {
            var instance = result.AsyncState as Client;
            if (instance.isDisposed)
            {
                return;
            }

            try
            {
                instance.stream.EndWrite(result);
                if (instance.done)
                {
                    Program.RemoveClient(instance);
                }
                else
                {
                    instance.timer.Change(SampleValues.GetNextSample(instance.timeDelay, instance.timeDeviation), Timeout.Infinite);
                }
            }
            catch (Exception e)
            {
                Program.LogError("Write exception: {0} - restarting", e.Message);
                Program.RemoveClient(instance);
            }
        }

        private static byte[] CreateSteadyStateMessage(Client instance, string greeting)
        {
            byte[] message = null;
            if (instance.steadyCounter++ % 2 == 0)
            {
                var msg = Encoding.UTF8.GetBytes(string.Format(CultureInfo.InvariantCulture, "\"{0} {1:D3}{2}\"", greeting, instance.id, instance.GetLastMessageRead()));
                message = new byte[msg.Length + 2];
                message[0] = 0x84;
                message[1] = (byte)(msg.Length & 0x7F);
                Buffer.BlockCopy(msg, 0, message, 2, msg.Length);
            }
            else
            {
                var msg = Encoding.UTF8.GetBytes(string.Format(CultureInfo.InvariantCulture, "\"{0}\"", Client.longGreeting));
                message = new byte[msg.Length + 4];
                message[0] = 0x84;
                message[1] = 126;
                message[2] = (byte)((msg.Length & 0xFF00) >> 8);
                message[3] = (byte)(msg.Length & 0x00FF);
                Buffer.BlockCopy(msg, 0, message, 4, msg.Length);
            }

            return message;
        }

        private string GetLastMessageRead()
        {
            lock (this.lockThis)
            {
                var msg = this.lastMessageRead;
                this.lastMessageRead = string.Empty;
                return msg;
            }
        }

        private void SetLastMessageRead(string value)
        {
            lock (this.lockThis)
            {
                this.lastMessageRead = string.Format(
                    CultureInfo.InvariantCulture,
                    " (received '{0}' at {1})",
                    value,
                    DateTime.Now.ToString("T", CultureInfo.CurrentCulture));
            }
        }

        private void InitializeSteadyState()
        {
            this.offset = 0;
            this.iteration = 0;
            this.timeDelay = Program.Config.SteadystateDelay;
            this.state = ClientState.SteadyState;
        }
    }
}