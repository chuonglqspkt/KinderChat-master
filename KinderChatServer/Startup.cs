using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Owin;
using Microsoft.Owin.StaticFiles;
using Newtonsoft.Json;
using Owin; 
using KinderChatServer.Core;
using KinderChatServer.DAL;

[assembly: OwinStartup(typeof(KinderChatServer.Startup))]

namespace KinderChatServer
{
    public partial class Startup
    {
        private const string ConnectionString = "Endpoint=sb://kinderchat-ns.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=ggJ8eNLIkipevig9g7J2PzoW0h1iArizyA4XNMJtJJo=";

        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            app.UseFileServer(new FileServerOptions()
            {
                RequestPath = new PathString("/Images")
            });
             
        }
         

        private class PushObject
        {
            public string Message { get; set; }

            public string FromId { get; set; }
        }
    }
}
