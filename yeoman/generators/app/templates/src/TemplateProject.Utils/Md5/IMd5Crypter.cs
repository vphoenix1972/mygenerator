﻿namespace <%= projectNamespace %>.Utils.Md5
{
    public interface IMd5Crypter
    {
        string Encrypt(string data);
    }
}