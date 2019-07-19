using System;
using System.Security.Cryptography;
using ProtectionApp.Tools;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.DependencyInjection;
using static System.Console;

namespace ProtectionApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var helloWorld = "Hello World!";
            
            var salt = Salt.Create(128);

            var militaryMovement = ObjectByte.ObjectToByteArray(new DateTime(2020, 11, 2));

            WriteLine(Convert.ToBase64String(militaryMovement));
            WriteLine(ObjectByte.ByteArrayToObject(Convert.FromBase64String(Convert.ToBase64String(militaryMovement))));

            var serviceCollection = new ServiceCollection();
            //serviceCollection.AddDataProtection(); //Original Method
            serviceCollection.AddDataProtection();
            var services = serviceCollection.BuildServiceProvider();

            var instance = ActivatorUtilities.CreateInstance<MyClass>(services);
            instance.RunSample();
        }
    }

    public class MyClass
    {
        private readonly IDataProtector _protector;

        public MyClass(IDataProtectionProvider provider)
        {
            _protector = provider.CreateProtector("Contoso.MyClass.V1");
        }

        public void RunSample()
        {
            Write("Enter input: ");
            var input = Console.ReadLine();

            var protectedPayload = _protector.Protect(input);
            WriteLine($"Protect returned: {protectedPayload}");

            var unprotectedPayload = _protector.Unprotect(protectedPayload);
            WriteLine($"Unprotect returned: {unprotectedPayload}");
        }
    }
}
