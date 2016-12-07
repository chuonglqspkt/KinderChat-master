using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using KinderChat.ServerClient;
using KinderChat.ServerClient.Entities;
using KinderChat.ServerClient.Ws.Proxy;
using KinderChat.Services.Messages;

namespace KinderChat
{
	public static class App
	{
		public const string InsightsKey = "27a02f75ddc98a035428dad91c70cf577b067895";

		public static void Init()
		{
			ServiceContainer.Register<IAuthenticationManager>(()=>new AuthenticationManager());
			ServiceContainer.Register<SignUpViewModel> (() => new SignUpViewModel ());
			ServiceContainer.Register<ConversationsViewModel> (() => new ConversationsViewModel ());
			ServiceContainer.Register<FriendsViewModel> (() => new FriendsViewModel ());
			ServiceContainer.Register<ProfileViewModel> (() => new ProfileViewModel ());
			ServiceContainer.Register<PointsViewModel> (() => new PointsViewModel ());
			ServiceContainer.Register<Logger> (() => new Logger ());
			ServiceContainer.Register<ICryptoService> (() => new RsaAndAesHybridCryptoService ());

            
            ServiceContainer.Register<ConnectionManager>(() => new ConnectionManager(ServiceContainer.Resolve<ILiveConnection>(), new SettingsToCredentialsProviderAdapter()));
            ServiceContainer.Register<MessagingService>(() => new MessagingService(ServiceContainer.Resolve<ConnectionManager>()));
            ServiceContainer.Register<GroupChatsService>(() => new GroupChatsService(ServiceContainer.Resolve<ConnectionManager>()));

            ServiceContainer.Register<MessagesManager>(() => new MessagesManager(
                ServiceContainer.Resolve<MessagingService>(),
                ServiceContainer.Resolve<ConnectionManager>(),
                ServiceContainer.Resolve<IMessageRepository>(),
                ServiceContainer.Resolve<ICryptoService>(),
                ServiceContainer.Resolve<FriendsViewModel>()));

            ServiceContainer.Register<TypingService>(() => new TypingService(ServiceContainer.Resolve<MessagingService>()));
			if (Settings.FirstRun)
				InitKinderTasks ().Wait ();

		}

		public static void Logout()
		{
			ServiceContainer.Clear ();
			Init ();
		}

		public static Task InitKinderTasks ()
		{
			return Task.WhenAll ();
		} 


		public static ICryptoService CryptoService
		{
			get { return ServiceContainer.Resolve<ICryptoService> (); }
		}

		public static IAuthenticationManager AuthenticationManager
		{
			get { return ServiceContainer.Resolve<IAuthenticationManager> (); }
		}

		public static IMessageDialog MessageDialog
		{
			get { return ServiceContainer.Resolve<IMessageDialog> (); }
		}

        public static IUIThreadDispacher UIThreadDispacher
		{
            get { return ServiceContainer.Resolve<IUIThreadDispacher>(); }
		}

		public static Logger Logger
		{
			get { return ServiceContainer.Resolve<Logger> (); }
		}

	    public static MessagesManager MessagesManager
	    {
            get { return ServiceContainer.Resolve<MessagesManager>(); }
	    }

        public static ConnectionManager ConnectionManager
        {
            get { return ServiceContainer.Resolve<ConnectionManager>(); }
        }

		public static SignUpViewModel SignUpViewModel
		{
			get { return ServiceContainer.Resolve<SignUpViewModel> (); }
		}

		public static ConversationsViewModel ConversationsViewModel
		{
			get { return ServiceContainer.Resolve<ConversationsViewModel> (); }
		}

		public static FriendsViewModel FriendsViewModel
		{
			get { return ServiceContainer.Resolve<FriendsViewModel> (); }
		}

		public static ProfileViewModel ProfileViewModel
		{
			get { return ServiceContainer.Resolve<ProfileViewModel> (); }
		}

		public static PointsViewModel PointsViewModel
		{
			get { return ServiceContainer.Resolve<PointsViewModel> (); }
		}

		public static bool FakeSignup
		{
			get { return false; }
		}

		public static bool ForceSignup
		{
			get { return false; }
		}

        public static IMessageRepository MessageRepository
        {
            get { return ServiceContainer.Resolve<IMessageRepository>(); }
        }
	}

    public class SettingsToCredentialsProviderAdapter : ICredentialsProvider
    {
        public string DeviceId
        {
            get { return Settings.UserDeviceId; }
        }

        public string AccessToken
        {
            get { return Settings.AccessToken; }
        }

        public long UserId
        {
            get { return Settings.MyId; }
        }

        public byte[] PublicKey
        {
            get { return Settings.PublicKey; }
        }
    }
}

