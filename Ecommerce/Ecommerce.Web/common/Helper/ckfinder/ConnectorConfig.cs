using CKSource.CKFinder.Connector.Config;
using CKSource.CKFinder.Connector.Core.Acl;
using CKSource.CKFinder.Connector.Core.Builders;
using CKSource.CKFinder.Connector.Host.Owin;
using CKSource.FileSystem.Local;
using Owin;
using System.Collections.Generic;

namespace Ecommerce.Web.common.Helper.ckfinder
{
    public class ConnectorConfig
    {
        public static void RegisterFileSystems()
        {
            FileSystemFactory.RegisterFileSystem<LocalStorage>();
            //FileSystemFactory.RegisterFileSystem<DropboxStorage>();
            //FileSystemFactory.RegisterFileSystem<AmazonStorage>();
            //FileSystemFactory.RegisterFileSystem<AzureStorage>();
        }

        public static void SetupConnector(IAppBuilder builder)
        {
            var allowedRoleMatcherTemplate = "ABC";//ConfigurationManager.AppSettings["ckfinderAllowedRole"];
            var authenticator = new RoleBasedAuthenticator(allowedRoleMatcherTemplate);

            var connectorFactory = new OwinConnectorFactory();
            var connectorBuilder = new ConnectorBuilder();
            var connector = connectorBuilder
                //.LoadConfig()
                .SetAuthenticator(authenticator)
                .SetRequestConfiguration(
                    (request, config) =>
                    {
                       config.LoadConfig();

                        //var defaultBackend = config.GetBackend("default");
                        //var keyValueStoreProvider = new FileSystemKeyValueStoreProvider(defaultBackend);
                        //config.SetKeyValueStoreProvider(keyValueStoreProvider);
                        
                        config.AddProxyBackend("local", new LocalStorage(@"uploads"));
                        config.AddResourceType("Files", resourceBuilder => resourceBuilder.SetBackend("local", "files"));
                        config.AddResourceType("Images", resourceBuilder => resourceBuilder.SetBackend("local", "images"));
                        config.AddResourceType("Products", resourceBuilder => resourceBuilder.SetBackend("local", "products"));
                        config.AddAclRule(new AclRule(
                            new StringMatcher("*"), new StringMatcher("/"), new StringMatcher("*"),
                            new Dictionary<Permission, PermissionType>
                            {
                                 { Permission.FolderView, PermissionType.Allow },
                                 { Permission.FolderCreate, PermissionType.Allow },
                                 { Permission.FolderRename, PermissionType.Allow },
                                 { Permission.FolderDelete, PermissionType.Allow },

                                 { Permission.FileView, PermissionType.Allow },
                                 { Permission.FileCreate, PermissionType.Allow },
                                 { Permission.FileRename, PermissionType.Allow },
                                 { Permission.FileDelete, PermissionType.Allow },

                                 { Permission.ImageResize, PermissionType.Allow },
                                 { Permission.ImageResizeCustom, PermissionType.Allow }
                            }));

                    })
                .Build(connectorFactory);

            builder.UseConnector(connector);
        }
    }
}