using System;

namespace ServerTurism.Networking
{
    [Serializable]
    public enum MessageType
    {
        Error,
        LogIn,
        LoggedIn,
        LogOut,
        LoggedOut,
        GetExcursii,
        GiveExcursii,
        GetSearchExcursii,
        GiveSearchExcursii,
        MakeRezervare,
        RezervareMade,
        UpdateNotify
    }

    [Serializable]
    public class Message
    {
        public Message(MessageType type, object contents)
        {
            if (contents.GetType().IsSerializable) Contents = contents;
            Type = type;
        }

        public object Contents { get; }
        public MessageType Type { get; }
    }
}