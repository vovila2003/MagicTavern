using System;
using System.IO;

namespace Modules.SaveLoad
{
    public interface IEncryptor : IDisposable 
    {
        Stream Encrypt(Stream openStream);
        Stream Decrypt(Stream closedStream);
    }
}