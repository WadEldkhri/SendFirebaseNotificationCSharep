using Microsoft.Extensions.Options;
using System.Net;

namespace SendFirebaseNotificationC_
{

    public class SendNotificationRepo : ISendNotification
    {
        private readonly FirebaseSettings _appSettings;
        public SendNotificationRepo( IOptions<FirebaseSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }
        public bool SendNotification(string Msg, string Title, Types Type, string TypeValue = "")
        {
            var result =false;
            try
            {
                var webAddr = _appSettings.FirebaseURL;// "https://fcm.googleapis.com/fcm/send";
                // string AppSenderID = "";
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(webAddr);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Headers.Add(HttpRequestHeader.Authorization, $"key={_appSettings.ServerKey}");
                httpWebRequest.Method = "POST";
                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    string RequestStr = "";
                    if (Type == Types.Topic)
                    {
                        
                        RequestStr = "{\"to\": \"/topics/" + TypeValue + "\",\"data\": {\"ShortDesc\": \"" + Title + "\",\"IncidentNo\": \"123\",\"Description\":\"detail desc\"},\"notification\":{\"title\": \"" + Title + "\",\"text\": \"" + Msg + "\",\"sound\":\"default\"}}";
                    }
                    else
                    {

                        RequestStr = "{\"to\":\"" + TypeValue + "\",\"data\": {\"ShortDesc\": \"" + Title + "\",\"IncidentNo\": \"123\",\"Description\":\"detail desc\"},\"notification\":{\"title\": \"" + Title + "\",\"text\": \"" + Msg + "\",\"sound\":\"default\"}}";
                    }

                    streamWriter.Write(RequestStr);
                    streamWriter.Flush();
                }

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var re = streamReader.ReadToEnd();
                    result = true;
                }
            }
            catch (Exception)
            {
                result = false;
            }
           
            return result;
        }
    }
}
