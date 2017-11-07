using Microsoft.Owin;
using Owin;
using CKSource.CKFinder.Connector.Config;
using CKSource.CKFinder.Connector.Core.Builders;
using CKSource.CKFinder.Connector.Host.Owin;
using CKSource.CKFinder.Connector.KeyValue.FileSystem;
using CKSource.FileSystem.Amazon;
using CKSource.FileSystem.Azure;
using CKSource.FileSystem.Dropbox;
using CKSource.FileSystem.Ftp;
using CKSource.FileSystem.Local;
using System.Configuration;
using CKSource.CKFinder.Connector.Core.Authentication;
using CKSource.CKFinder.Connector.Core.Acl;
using CKSource.CKFinder.Connector.Core;
using System.Threading;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Linq;

[assembly: OwinStartupAttribute(typeof(Ecommerce.Web.Startup))]
namespace Ecommerce.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            RegisterFileSystems();
        }
        public  void RegisterFileSystems()
        {
            FileSystemFactory.RegisterFileSystem<LocalStorage>();
            FileSystemFactory.RegisterFileSystem<DropboxStorage>();
            FileSystemFactory.RegisterFileSystem<AmazonStorage>();
            FileSystemFactory.RegisterFileSystem<AzureStorage>();
            FileSystemFactory.RegisterFileSystem<FtpStorage>();
        }

        public void SetupConnector(IAppBuilder builder)
        {
            var allowedRoleMatcherTemplate = ConfigurationManager.AppSettings["ckfinderAllowedRole"];
            var authenticator = new RoleBasedAuthenticator(allowedRoleMatcherTemplate);

            var connectorFactory = new OwinConnectorFactory();
            var connectorBuilder = new ConnectorBuilder();
            var connector = connectorBuilder
                .LoadConfig()
                .SetAuthenticator(authenticator)
                .SetRequestConfiguration(
                    (request, config) =>
                    {
                        config.LoadConfig();

                        var defaultBackend = config.GetBackend("default");
                        var keyValueStoreProvider = new FileSystemKeyValueStoreProvider(defaultBackend);
                        config.SetKeyValueStoreProvider(keyValueStoreProvider);
                    })
                .Build(connectorFactory);

            builder.UseConnector(connector);
        }

        public class RoleBasedAuthenticator : IAuthenticator
        {
            private readonly string _allowedRoleMatcherTemplate;

            private readonly StringMatcher _allowedRoleMatcher;

            public RoleBasedAuthenticator(string allowedRoleMatcherTemplate)
            {
                _allowedRoleMatcherTemplate = allowedRoleMatcherTemplate;
                _allowedRoleMatcher = new StringMatcher(allowedRoleMatcherTemplate);
            }

            public Task<IUser> AuthenticateAsync(ICommandRequest commandRequest, CancellationToken cancellationToken)
            {
                var claimsPrincipal = commandRequest.Principal as ClaimsPrincipal;

                var roles = new string[] { };
                if (claimsPrincipal != null && claimsPrincipal.Claims != null)
                {
                    roles = claimsPrincipal.Claims.Where(x => x.Type == ClaimTypes.Role).Select(x => x.Value).ToArray();
                }

                var user = new User(IsAuthenticated(roles), roles);
                return Task.FromResult((IUser)user);
            }

            private bool IsAuthenticated(string[] roles)
            {
                // Should always fail if matcher is empty.
                if (_allowedRoleMatcherTemplate == string.Empty)
                {
                    return false;
                }

                // Use empty string when there are no roles, so asterisk pattern will match users without any role.
                var safeRoles = roles.Any() ? roles : new[] { string.Empty };

                return safeRoles.Any(role => _allowedRoleMatcher.IsMatch(role));
            }
        }

    }

    
}
