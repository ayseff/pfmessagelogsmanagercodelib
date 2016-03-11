namespace PFLogManagerObjects
{
#pragma warning disable 1591

    public enum enLogFileStorageType
    {
        Unknown = 0,
        TextFile = 1
    }

    public enum enLogRetryQueueStorageType
    {
        Unknown = 0,
        XmlFile = 1
    }

    public enum enLogMessageType
    {
        Error = 1,
        Warning = 2,
        Information = 4,
        Alert = 8
    }


#pragma warning restore 1591

}