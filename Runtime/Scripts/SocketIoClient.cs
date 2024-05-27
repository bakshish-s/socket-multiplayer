using System;
using SocketIOClient.Newtonsoft.Json;
namespace Hashbyte.SocketMultiplayer
{
    internal class SocketIoClient
    {
        private SocketIOUnity socket;
        private ISocketEvents socketListener;
        private bool isConnected;

        // Start is called before the first frame update
        public void Connect(string serverUrl, ISocketEvents socketListener)
        {
            Uri uri = new Uri("http://localhost:3000");
            this.socketListener = socketListener;
            socket = new SocketIOUnity(uri);
            socket.JsonSerializer = new NewtonsoftJsonSerializer();        
            socket.Connect();        
            socket.OnConnected += OnConnected;

            socket.On("server-message", (response) =>
            {
                //Debug.Log($"Received message from server {response.GetValue<string>()}");
            });
        }

        public void CreateOrJoinRoom()
        {
            if (!isConnected)
            {
                throw new System.Exception("Not connected, Call HashbyteMultiplayer.Instance.Connect() first");                
            }            
            socket?.Emit("joinRoom");
        }

        public void SendMove(string move)
        {
            socket?.Emit("onMove", move);
        }

        private void OnConnected(object sender, EventArgs e)
        {
            socketListener.OnConnected(socket.Id);
            isConnected = true;
            //Debug.Log("Connected to socket "+e);
            socket.On("onRoomJoined", (response) => socketListener.OnRoomJoined(response.GetValue<string>()));            
            socket.On("onMove", (response) => socketListener.OnMove(response.GetValue<string>()));            
        }
   
        public void OnDispose()
        {
            socket.Disconnect();
            socket.Dispose();
        }
    }
}
