using System.Collections;
using Hashbyte.SocketMultiplayer;
using UnityEngine;

public class DrawSomething : MonoBehaviour, ISocketEvents
{
    private bool isDrawing = true;
    private Vector3 mousePosition;
    const string serverUrl = "http://localhost:3000";
    // Start is called before the first frame update
    void Start()
    {
        HashbyteMultiplayer.Instance.ConnectToServer(serverUrl, this);
    }

    public void GUI_StartGame()
    {
        HashbyteMultiplayer.Instance.CreateOrJoinRoom();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isDrawing = true;
        }
        else if (Input.GetMouseButton(0) && isDrawing)
        {
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);            
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDrawing = false;
        }
    }


    private void OnDestroy()
    {
        HashbyteMultiplayer.Instance.Dispose();
    }

    public void OnConnected(string socketId)
    {
        Debug.Log($"Connected to server {socketId}");
    }

    public void OnRoomJoined(string roomId)
    {
        Debug.Log($"Room joined {roomId}");
    }

    public void OnMove(string moveJson)
    {
        Debug.Log($"Move received {moveJson}");
    }
}
