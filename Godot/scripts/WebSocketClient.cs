using Godot;
using System;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;

public partial class WebSocketClient : Node
{
    [Export]
    string url;
    WebSocketPeer Client;
    public string packet { get; set; }

    public bool WebSocketClosed = false;
    Thread myThread;

    public override void _Ready()
    {
        Connect();

    }

    public override void _Process(double delta)
    {
        myThread = new Thread(new ThreadStart(PollWebSocket));
        myThread.Start();
        //PollWebSocket();

        //if (WebSocketClosed)
        //SetProcess(false);

    }

    public void Connect()
    {
        Client = new WebSocketPeer();
        Client.ConnectToUrl(url);

    }

    public async void SendText(string text)
    {
        try
        {
            Client.SendText(text);
        }
        catch (System.Exception)
        {
        }

        await Task.Delay(0);

    }

    public void PollWebSocket()
    {

        WebSocketClosed = false;
        Client.Poll();
        var state = Client.GetReadyState();
        if (WebSocketPeer.State.Open == state)
        {
            SendText("packet request");
            while (Client.GetAvailablePacketCount() >= 1)
            {
                packet = Client.GetPacket().GetStringFromUtf8();
                //GD.Print($"packet: {packet}");

            }
        }
        else if (state == WebSocketPeer.State.Closing) { return; }
        else if (state == WebSocketPeer.State.Closed)
        {
            var code = Client.GetCloseCode();
            var reason = Client.GetCloseReason();

            GD.Print($"Websocket WebSocketClosed with code: {code}, reason: {reason}");
            Connect();
            WebSocketClosed = true;
        }
        Thread.Sleep(10);
    }

}


