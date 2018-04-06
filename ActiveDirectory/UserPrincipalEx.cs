using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VNC.AD
{
    [DirectoryRdnPrefix("CN")]
    [DirectoryObjectClass("Person")]
    public class UserPrincipalEx : UserPrincipal
    {
        #region Constructors and Load
     
        // Inplement the constructor using the base class constructor. 
        public UserPrincipalEx(PrincipalContext context) : base(context)
        { }

        // Implement the constructor with initialization parameters.    
        public UserPrincipalEx(PrincipalContext context,
                         string samAccountName,
                         string password,
                         bool enabled) : base(context, samAccountName, password, enabled)
        {} 
        #endregion

        #region Properties and Fields
        
        // Create the "Department" property.    
        [DirectoryProperty("department")]
        public string Department
        {
            get
            {
                if (ExtensionGet("department").Length != 1)
                    return string.Empty;

                return (string)ExtensionGet("department")[0];
            }
            set { ExtensionSet("department", value); }
        }

        // Create the "Manager" property.    
        [DirectoryProperty("manager")]
        public string Manager
        {
            get
            {
                if (ExtensionGet("manager").Length != 1)
                    return string.Empty;

                return (string)ExtensionGet("manager")[0];
            }
            set { ExtensionSet("manager", value); }
        }

        #endregion

        #region Delegates and Events
        

        #endregion

        #region Event Handlers
        
 
        #endregion

        #region Main Methods
 
        // Implement the overloaded search method FindByIdentity.
        public static new UserPrincipalEx FindByIdentity(PrincipalContext context, string identityValue)
        {
            return (UserPrincipalEx)FindByIdentityWithType(context, typeof(UserPrincipalEx), identityValue);
        }

        // Implement the overloaded search method FindByIdentity. 
        public static new UserPrincipalEx FindByIdentity(PrincipalContext context, IdentityType identityType, string identityValue)
        {
            return (UserPrincipalEx)FindByIdentityWithType(context, typeof(UserPrincipalEx), identityType, identityValue);
        }

        #endregion

        #region Private Methods
        
   
        #endregion

    }

}