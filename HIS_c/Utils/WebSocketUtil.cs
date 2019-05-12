using HIS_c.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.WebSockets;

namespace HIS_c.Utils
{
    public class WebSocketUtil
    {
        public static Dictionary<string, WebSocket> dicSockets = new Dictionary<string, WebSocket>();



        /// <summary>
        /// 创建socket连接
        /// </summary>
        /// <returns></returns>
        public HttpResponseMessage CreateSocket()
        {
            if (HttpContext.Current.IsWebSocketRequest)
            {
                HttpContext.Current.AcceptWebSocketRequest(ProcessChat);
            }
            return new HttpResponseMessage(HttpStatusCode.SwitchingProtocols);
        }


        /// <summary>
        /// 接收 报警程序推送数据
        /// </summary>
        /// <param name="polices"></param>
        /// <returns></returns>
        public bool PushPoliceInfo(List<UserModel> polices)
        {

            try
            {

                if (dicSockets != null && dicSockets.Count > 0)
                {
                    foreach (var dic in dicSockets)
                    {
                        var socket = dic.Value;
                        if (socket.State == WebSocketState.Open)
                        {
                            var msg = JsonConvert.SerializeObject(polices);
                            ArraySegment<byte> buffer = new ArraySegment<byte>(Encoding.UTF8.GetBytes(msg));
                            socket.SendAsync(buffer, WebSocketMessageType.Text, true, CancellationToken.None);
                        }
                    }
                }


                return true;

            }
            catch (Exception e)
            {
                return false;
            }

        }

        /// <summary>
        /// 接收前端发送的数据
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        private async Task ProcessChat(AspNetWebSocketContext arg)
        {
            WebSocket socket = arg.WebSocket;
            string key = arg.SecWebSocketKey;
            if (!dicSockets.ContainsKey(key))
            {
                dicSockets.Add(key, socket);
            }


            while (true)
            {
                ArraySegment<byte> buffer = new ArraySegment<byte>(new byte[1024 * 10]);
                WebSocketReceiveResult result = await socket.ReceiveAsync(buffer, CancellationToken.None);

                if (socket.State == WebSocketState.Open)
                {
                    //前端发送的命令
                    string message = Encoding.UTF8.GetString(buffer.Array, 0, result.Count);

                }
                else
                {
                    dicSockets.Remove(key);
                    break;
                }
            }
        }

    }
}