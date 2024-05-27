namespace Hashbyte.SocketMultiplayer
{
    public class HashbyteMultiplayer
    {
        #region Singelton
        private static HashbyteMultiplayer instance;
        public static HashbyteMultiplayer Instance
        {
            get
            {
                if (instance == null) instance = new HashbyteMultiplayer();
                return instance;
            }
        }

        private HashbyteMultiplayer()
        {
            socketIoClient = new SocketIoClient();
        }
        #endregion

        private SocketIoClient socketIoClient;        

        public void ConnectToServer(string url, ISocketEvents socketEventsListener)
        {            
            socketIoClient.Connect(url, socketEventsListener);
        }
        public void CreateOrJoinRoom()
        {
            socketIoClient.CreateOrJoinRoom();
        }
        public void SendMove(string move)
        {
            socketIoClient.SendMove(move);
        }
        public void Dispose()
        {
            socketIoClient.OnDispose();
            socketIoClient = null;
        }
    }
}
