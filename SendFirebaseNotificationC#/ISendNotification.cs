namespace SendFirebaseNotificationC_
{
    public interface ISendNotification
    {
        bool SendNotification(string Msg, string Title, Types Type, string TypeValue = "");
    }
}
