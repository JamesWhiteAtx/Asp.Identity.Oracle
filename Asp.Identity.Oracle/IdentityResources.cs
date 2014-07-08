using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace Asp.Identity.Oracle
{
    [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0"), DebuggerNonUserCode, CompilerGenerated]
    internal class IdentityResources
    {
        private static ResourceManager resourceMan;
        private static CultureInfo resourceCulture;
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static ResourceManager ResourceManager
        {
            get
            {
                if (object.ReferenceEquals(IdentityResources.resourceMan, null))
                {
                    ResourceManager resourceManager = new ResourceManager("My.AspNet.Identity.EntityFramework.IdentityResources", typeof(IdentityResources).Assembly);
                    IdentityResources.resourceMan = resourceManager;
                }
                return IdentityResources.resourceMan;
            }
        }
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static CultureInfo Culture
        {
            get
            {
                return IdentityResources.resourceCulture;
            }
            set
            {
                IdentityResources.resourceCulture = value;
            }
        }
        internal static string DbValidationFailed
        {
            get
            {
                return IdentityResources.ResourceManager.GetString("DbValidationFailed", IdentityResources.resourceCulture);
            }
        }
        internal static string DuplicateEmail
        {
            get
            {
                return IdentityResources.ResourceManager.GetString("DuplicateEmail", IdentityResources.resourceCulture);
            }
        }
        internal static string DuplicateUserName
        {
            get
            {
                return IdentityResources.ResourceManager.GetString("DuplicateUserName", IdentityResources.resourceCulture);
            }
        }
        internal static string EntityFailedValidation
        {
            get
            {
                return IdentityResources.ResourceManager.GetString("EntityFailedValidation", IdentityResources.resourceCulture);
            }
        }
        internal static string ExternalLoginExists
        {
            get
            {
                return IdentityResources.ResourceManager.GetString("ExternalLoginExists", IdentityResources.resourceCulture);
            }
        }
        internal static string IdentityV1SchemaError
        {
            get
            {
                return IdentityResources.ResourceManager.GetString("IdentityV1SchemaError", IdentityResources.resourceCulture);
            }
        }
        internal static string IncorrectType
        {
            get
            {
                return IdentityResources.ResourceManager.GetString("IncorrectType", IdentityResources.resourceCulture);
            }
        }
        internal static string PropertyCannotBeEmpty
        {
            get
            {
                return IdentityResources.ResourceManager.GetString("PropertyCannotBeEmpty", IdentityResources.resourceCulture);
            }
        }
        internal static string RoleAlreadyExists
        {
            get
            {
                return IdentityResources.ResourceManager.GetString("RoleAlreadyExists", IdentityResources.resourceCulture);
            }
        }
        internal static string RoleIsNotEmpty
        {
            get
            {
                return IdentityResources.ResourceManager.GetString("RoleIsNotEmpty", IdentityResources.resourceCulture);
            }
        }
        internal static string RoleNotFound
        {
            get
            {
                return IdentityResources.ResourceManager.GetString("RoleNotFound", IdentityResources.resourceCulture);
            }
        }
        internal static string UserAlreadyInRole
        {
            get
            {
                return IdentityResources.ResourceManager.GetString("UserAlreadyInRole", IdentityResources.resourceCulture);
            }
        }
        internal static string UserIdNotFound
        {
            get
            {
                return IdentityResources.ResourceManager.GetString("UserIdNotFound", IdentityResources.resourceCulture);
            }
        }
        internal static string UserLoginAlreadyExists
        {
            get
            {
                return IdentityResources.ResourceManager.GetString("UserLoginAlreadyExists", IdentityResources.resourceCulture);
            }
        }
        internal static string UserNameNotFound
        {
            get
            {
                return IdentityResources.ResourceManager.GetString("UserNameNotFound", IdentityResources.resourceCulture);
            }
        }
        internal static string UserNotInRole
        {
            get
            {
                return IdentityResources.ResourceManager.GetString("UserNotInRole", IdentityResources.resourceCulture);
            }
        }
        internal static string ValueCannotBeNullOrEmpty
        {
            get
            {
                return IdentityResources.ResourceManager.GetString("ValueCannotBeNullOrEmpty", IdentityResources.resourceCulture);
            }
        }
        internal IdentityResources()
        {
        }
    }
}
