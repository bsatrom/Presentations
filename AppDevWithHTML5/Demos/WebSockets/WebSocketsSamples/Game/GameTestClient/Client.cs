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
        /// <summary>
        /// Byte array for websocket 75 version.
        /// </summary>
        private static byte[][] v75 = new byte[][]
        {
            Encoding.UTF8.GetBytes("\r"),
            Encoding.UTF8.GetBytes("\n")
        };

        /// <summary>
        /// Byte array for websocket 76 version.
        /// </summary>
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

        /// <summary>
        /// Two deimensional byte array to be used in handshake protocol.
        /// </summary>
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

        /// <summary>
        /// Two deimensional byte array to be used in handshake protocol.
        /// </summary>
        private static byte[][] loop = new byte[][]
        {
            new byte[] { 0x00, 0x22, 0x54, 0x22, 0xFF, 0x00 },
            new byte[] { 0x22, 0x54, 0x22, 0xFF, 0x00, 0x22 },
            new byte[] { 0x54, 0x22, 0xFF, 0x00, 0x22, 0x54 },
            new byte[] { 0x22, 0xFF, 0x00, 0x22, 0x54, 0x22 },
            new byte[] { 0xFF, 0x00, 0x22, 0x54, 0x22, 0xFF },
        };

        /// <summary>
        /// Byte at the beginning of a frame.
        /// </summary>
        private static byte frameBegin = 0x00;

        /// <summary>
        /// Byte at the end of a frame.
        /// </summary>
        private static byte frameEnd = 0xFF;

        /// <summary>
        /// Byte array consists of frameBegin and frameEnd bytes.
        /// </summary>
        private static byte[] close = new byte[] { Client.frameEnd, Client.frameBegin };

        /// <summary>
        /// Client index counter
        /// </summary>
        private static int globalID = 0;

        /// <summary>
        /// Two dimensional byte array for HTTP header.
        /// </summary>
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
            Encoding.UTF8.GetBytes("origin: http://value:8080/foo/bar\r\n"),
        };

        /// <summary>
        /// Version specific 2D byte array.
        /// </summary>
        private byte[][] versionSpecific;

        /// <summary>
        /// Input buffer.
        /// </summary>
        private byte[] inputData = new byte[2048];

        /// <summary>
        /// Whether disposed.
        /// </summary>
        private bool isDisposed;

        /// <summary>
        /// Boolean value.
        /// </summary>
        private bool done;

        /// <summary>
        /// Uri address.
        /// </summary>
        private Uri address;

        /// <summary>
        /// Identifier of a client.
        /// </summary>
        private int id;

        /// <summary>
        /// Timer object.
        /// </summary>
        private Timer timer;

        /// <summary>
        /// Time delay.
        /// </summary>
        private int timeDelay = 50;

        /// <summary>
        /// Time deviation.
        /// </summary>
        private int timeDeviation = 20;

        /// <summary>
        /// Client state.
        /// </summary>
        private ClientState state;

        /// <summary>
        /// Offset value.
        /// </summary>
        private int offset;

        /// <summary>
        /// Number of iterations.
        /// </summary>
        private int iteration;

        /// <summary>
        /// TCP client obejct.
        /// </summary>
        private TcpClient client;

        /// <summary>
        /// Stream object.
        /// </summary>
        private Stream stream;

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

            this.http[2] = Encoding.UTF8.GetBytes(address.AbsolutePath);
            this.http[7] = Encoding.UTF8.GetBytes(string.Format(CultureInfo.InvariantCulture, "Host: {0}:{1}", address.Host, address.Port));

            var version = Program.Config.GetClientVersion();
            this.versionSpecific = version == WebSocketVersion.v75 ? Client.v75 : Client.v76;
            this.ClientMessageProvider = this.ConsoleCommandProvider;

            Program.LogVerbose("Request URI: {0}", address.AbsolutePath);
        }

        /// <summary>
        /// Client state enumeration.
        /// </summary>
        private enum ClientState
        {
            /// <summary>
            /// Http request state.
            /// </summary>
            HttpRequest = 0,

            /// <summary>
            /// Handshake state.
            /// </summary>
            WebSocketHandshake,

            /// <summary>
            /// Intro Messages state.
            /// </summary>
            IntroMessages,

            /// <summary>
            /// Frameloop state.
            /// </summary>
            FrameLoop,

            /// <summary>
            /// Steady state.
            /// </summary>
            SteadyState,

            /// <summary>
            /// Closed state.
            /// </summary>
            Close
        }

        /// <summary>
        /// Gets or sets the delegate having a string return value.
        /// </summary>
        public Func<string> ClientMessageProvider { get; set; }

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

        /// <summary>
        /// Gets invoked when read operation completes.
        /// </summary>
        /// <param name="result">Async result.</param>
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

        /// <summary>
        /// Writes the output data.
        /// </summary>
        /// <param name="state">Client object.</param>
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
                            outputData = Client.CreateSteadyStateMessage(instance);
                            instance.iteration++;
                        }
                        else
                        {
                            outputData = Client.CreateSteadyStateMessage(instance);
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

        /// <summary>
        /// Gets invoked after write operation completes.
        /// </summary>
        /// <param name="result">Async result.</param>
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

        /// <summary>
        /// Creates the steady state message.
        /// </summary>
        /// <param name="instance">Client object.</param>
        /// <returns>Byte array.</returns>
        private static byte[] CreateSteadyStateMessage(Client instance)
        {
            var command = instance.ClientMessageProvider();
            var msg = Encoding.UTF8.GetBytes(string.Format(CultureInfo.InvariantCulture, "\"{0}\"", command));
            var message = new byte[msg.Length + 2];
            message[0] = 0x00;
            Buffer.BlockCopy(msg, 0, message, 1, msg.Length);
            message[msg.Length + 1] = 0xFF;
            return message;
        }

        /// <summary>
        /// Stops the thread for the user input.
        /// </summary>
        /// <returns>User typed string.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Console.Write(System.String)", Justification = "Sample")]
        private string ConsoleCommandProvider()
        {
            Console.Write("Enter Game Command>");
            var message = Console.ReadLine();
            return message;
        }

        /// <summary>
        /// Initializes the steady state.
        /// </summary>
        private void InitializeSteadyState()
        {
            this.offset = 0;
            this.iteration = 0;
            this.timeDelay = Program.Config.SteadystateDelay;
            this.state = ClientState.SteadyState;
        }
    }
}