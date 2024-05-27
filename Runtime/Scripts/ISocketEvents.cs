public interface ISocketEvents
{
    void OnConnected(string socketId);
    void OnRoomJoined(string roomId);
    void OnMove(string moveJson);
}
