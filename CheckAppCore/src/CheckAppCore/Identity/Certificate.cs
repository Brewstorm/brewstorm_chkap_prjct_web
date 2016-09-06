using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace CheckAppCore.Identity
{
    static class Certificate
    {
        internal static X509Certificate2 Get(string contentRootPath)
        {
            return new X509Certificate2(Path.Combine(contentRootPath, "Identity\\idsvr3test.pfx"), "idsrv3test");
        }
    }
}