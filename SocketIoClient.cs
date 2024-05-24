using System;
using System.Collections;
using System.Collections.Generic;
using SocketIOClient;
using SocketIOClient.Newtonsoft.Json;
using UnityEngine;

public class SocketIoClient : MonoBehaviour
{
    SocketIOUnity socket;
    // Start is called before the first frame update
    void Start()
    {
        Uri uri = new Uri("http://localhost:3000");
        socket = new SocketIOUnity(uri);
        socket.JsonSerializer = new NewtonsoftJsonSerializer();
        Debug.Log("Started connection");
        socket.Connect();        
        socket.OnConnected += Socket_OnConnected;
        socket.On("server-message", (response) =>
        {
            Debug.Log($"Received message from server {response.GetValue<string>()}");
        });
    }

    private void Socket_OnConnected(object sender, EventArgs e)
    {
        Debug.Log("Connected to socket "+e);
        socket.On("onRoomJoined", (response) =>
        {
            Debug.Log($"Player joined room {response.GetValue<string>()}");
        });
        socket.Emit("joinRoom");
        socket.On("onDraw", (response) =>
        {
            Debug.Log($"Drawing {response}");
        });

    }
    bool isDrawing = true;
    Vector3 mousePosition;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isDrawing = true;
        }else if (Input.GetMouseButton(0))
        {
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            socket.Emit("onDraw", JsonUtility.ToJson(mousePosition));
        }else if (Input.GetMouseButtonUp(0))
        {
            isDrawing = false;
        }
    }

    private void OnDestroy()
    {
        socket.Dispose();
    }
}
