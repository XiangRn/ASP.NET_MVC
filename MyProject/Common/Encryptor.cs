﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;

namespace MyProject.Common
{
    public class Encryptor
    {
        public static string EncMD5(string password)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            UTF8Encoding encoder = new UTF8Encoding();
            Byte[] originalBytes = encoder.GetBytes(password);
            Byte[] encodedBytes = md5.ComputeHash(originalBytes);
            password = BitConverter.ToString(encodedBytes).Replace("-", "");
            var result = password.ToLower();
            return result;
        }
    }
}