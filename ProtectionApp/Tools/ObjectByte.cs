using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace ProtectionApp.Tools
{
    internal class ObjectByte
    {
        // Convert an object to a byte array
        internal static byte[] ObjectToByteArray(object obj)
        {
            if (obj == null)
                return null;

            var bf = new BinaryFormatter();
            var ms = new MemoryStream();
            bf.Serialize(ms, obj);

            return ms.ToArray();
        }

        // Convert a byte array to an Object
        internal static object ByteArrayToObject(byte[] arrBytes)
        {
            using (var memStream = new MemoryStream())
            {
                var binForm = new BinaryFormatter();
                memStream.Write(arrBytes, 0, arrBytes.Length);
                memStream.Seek(0, SeekOrigin.Begin);
                var obj = (object)binForm.Deserialize(memStream);

                return obj;
            }
        }
    }
}
