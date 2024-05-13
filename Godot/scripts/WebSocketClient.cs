using Godot;
using System;
using System.Threading.Tasks;

public partial class WebSocketClient : Node
{
    [Export]
    string url;
    WebSocketPeer Client;
    public string packet { get; set; }

    bool WebSocketClosed = false;

    public override void _Ready()
    {
        Connect();
    }

    public override void _Process(double delta)
    {
        PollWebSocket();
        if (WebSocketClosed)
            SetProcess(false);
    }

    public async void Connect()
    {
        Client = new WebSocketPeer();
        Client.ConnectToUrl(url);

        await Task.Delay(0);
    }

    public async void PollWebSocket()
    {
        await Task.Run(async () =>
        {
            WebSocketClosed = false;
            Client.Poll();
            var state = Client.GetReadyState();
            if (WebSocketPeer.State.Open == state)
            {
                while (Client.GetAvailablePacketCount() == 1)
                {
                    packet = System.Text.Encoding.UTF8.GetString(Client.GetPacket());
                    GD.Print($"packet: {packet}");
                }
            }
            else if (state == WebSocketPeer.State.Closing) { return; }
            else if (state == WebSocketPeer.State.Closed)
            {
                var code = Client.GetCloseCode();
                var reason = Client.GetCloseReason();

                GD.Print($"Websocket WebSocketClosed with code: {code}, reason: {reason}");
                WebSocketClosed = true;
            }
            await Task.Delay(500);
        });

        await Task.Delay(0);
    }

}


