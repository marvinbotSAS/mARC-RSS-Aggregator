using System;
using System.Runtime.Serialization;

namespace RSSRTReader.Misc
{
    [Serializable]
    class CreateConfigFileException : Exception
    {
        public CreateConfigFileException()
            : base()
        { }

        public CreateConfigFileException(string message)
            : base(message)
        { }

        public CreateConfigFileException(string message, Exception innerException)
            : base(message, innerException)
        { }

        public CreateConfigFileException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }
    }

    [Serializable]
    class CreateConfigDirException : Exception
    {
        public CreateConfigDirException()
            : base()
        { }

        public CreateConfigDirException(string message)
            : base(message)
        { }

        public CreateConfigDirException(string message, Exception innerException)
            : base(message, innerException)
        { }

        public CreateConfigDirException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }
    }

    [Serializable]
    class FileDownloadException : Exception
    {
        public FileDownloadException()
            : base()
        { }

        public FileDownloadException(string message)
            : base(message)
        { }

        public FileDownloadException(string message, Exception innerException)
            : base(message, innerException)
        { }

        public FileDownloadException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }
    }
}
