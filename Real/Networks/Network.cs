using System;
using REAL.Tools;
using UnityEngine;
using System.Collections.Generic;

namespace REAL.Networks
{
    public static class RealNetwork
    {
        public const string Domain = "render.real-api.online";
        public static readonly string Api = $"https://{Domain}/rapi/render";

        public static string LoginAPI(LoginCred login)
        {
            var insID = login.insID;
            var appKey = login.appKey;
            var prodKey = login.prodKey;
            return $"https://{Domain}/rapi/login?insID={insID}&appKey={appKey}&prodKey={prodKey}";
        }

        public static SocketResponse ToSocketMessage(string jsonString)
        {
            try
            {
                var jsonData = JsonUtility.FromJson<SocketMsg>(jsonString);
                if (jsonData.type != "job") return JsonUtility.FromJson<SocketResponse>(JsonUtility.ToJson(jsonData));
                var dataArray = new List<string> { jsonData.data };
                jsonData.data = JsonUtility.ToJson(dataArray);
                return JsonUtility.FromJson<SocketResponse>(JsonUtility.ToJson(jsonData));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        
        [Serializable]
        private class SocketMsg
        {
            public string msg;
            public string type;
            public string data;
        }
    }
}
