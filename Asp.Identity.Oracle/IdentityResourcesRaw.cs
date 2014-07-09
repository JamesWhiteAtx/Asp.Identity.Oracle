using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace Big.Fucking.Mess
{
    [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0"), DebuggerNonUserCode, CompilerGenerated]
    internal class IdentityResourcesRaw
    {
        private static ResourceManager resourceMan;
        private static CultureInfo resourceCulture;
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static ResourceManager ResourceManager
        {
            get
            {
                if (object.ReferenceEquals(IdentityResourcesRaw.resourceMan, null))
                {
                    ResourceManager resourceManager = new ResourceManager("My.AspNet.Identity.EntityFramework.IdentityResources", typeof(IdentityResourcesRaw).Assembly);
                    IdentityResourcesRaw.resourceMan = resourceManager;
                }
                return IdentityResourcesRaw.resourceMan;
            }
        }
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static CultureInfo Culture
        {
            get
            {
                return IdentityResourcesRaw.resourceCulture;
            }
            set
            {
                IdentityResourcesRaw.resourceCulture = value;
            }
        }
        internal static string DbValidationFailed
        {
            get
            {
                return IdentityResourcesRaw.ResourceManager.GetString("DbValidationFailed", IdentityResourcesRaw.resourceCulture);
            }
        }
        internal static string DuplicateEmail
        {
            get
            {
                return IdentityResourcesRaw.ResourceManager.GetString("DuplicateEmail", IdentityResourcesRaw.resourceCulture);
            }
        }
        internal static string DuplicateUserName
        {
            get
            {
                return IdentityResourcesRaw.ResourceManager.GetString("DuplicateUserName", IdentityResourcesRaw.resourceCulture);
            }
        }
        internal static string EntityFailedValidation
        {
            get
            {
                return IdentityResourcesRaw.ResourceManager.GetString("EntityFailedValidation", IdentityResourcesRaw.resourceCulture);
            }
        }
        internal static string ExternalLoginExists
        {
            get
            {
                return IdentityResourcesRaw.ResourceManager.GetString("ExternalLoginExists", IdentityResourcesRaw.resourceCulture);
            }
        }
        internal static string IdentityV1SchemaError
        {
            get
            {
                return IdentityResourcesRaw.ResourceManager.GetString("IdentityV1SchemaError", IdentityResourcesRaw.resourceCulture);
            }
        }
        internal static string IncorrectType
        {
            get
            {
                return IdentityResourcesRaw.ResourceManager.GetString("IncorrectType", IdentityResourcesRaw.resourceCulture);
            }
        }
        internal static string PropertyCannotBeEmpty
        {
            get
            {
                return IdentityResourcesRaw.ResourceManager.GetString("PropertyCannotBeEmpty", IdentityResourcesRaw.resourceCulture);
            }
        }
        internal static string RoleAlreadyExists
        {
            get
            {
                return IdentityResourcesRaw.ResourceManager.GetString("RoleAlreadyExists", IdentityResourcesRaw.resourceCulture);
            }
        }
        internal static string RoleIsNotEmpty
        {
            get
            {
                return IdentityResourcesRaw.ResourceManager.GetString("RoleIsNotEmpty", IdentityResourcesRaw.resourceCulture);
            }
        }
        internal static string RoleNotFound
        {
            get
            {
                return IdentityResourcesRaw.ResourceManager.GetString("RoleNotFound", IdentityResourcesRaw.resourceCulture);
            }
        }
        internal static string UserAlreadyInRole
        {
            get
            {
                return IdentityResourcesRaw.ResourceManager.GetString("UserAlreadyInRole", IdentityResourcesRaw.resourceCulture);
            }
        }
        internal static string UserIdNotFound
        {
            get
            {
                return IdentityResourcesRaw.ResourceManager.GetString("UserIdNotFound", IdentityResourcesRaw.resourceCulture);
            }
        }
        internal static string UserLoginAlreadyExists
        {
            get
            {
                return IdentityResourcesRaw.ResourceManager.GetString("UserLoginAlreadyExists", IdentityResourcesRaw.resourceCulture);
            }
        }
        internal static string UserNameNotFound
        {
            get
            {
                return IdentityResourcesRaw.ResourceManager.GetString("UserNameNotFound", IdentityResourcesRaw.resourceCulture);
            }
        }
        internal static string UserNotInRole
        {
            get
            {
                return IdentityResourcesRaw.ResourceManager.GetString("UserNotInRole", IdentityResourcesRaw.resourceCulture);
            }
        }
        internal static string ValueCannotBeNullOrEmpty
        {
            get
            {
                return IdentityResourcesRaw.ResourceManager.GetString("ValueCannotBeNullOrEmpty", IdentityResourcesRaw.resourceCulture);
            }
        }
        internal IdentityResourcesRaw()
        {
        }
    }
}
