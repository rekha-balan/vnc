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


namespace VNC.ActiveDirectory
{
    public class Helper
    {
        public static bool CheckDirectGroupMembership(string userID, string groupName, string Domain)
        {
//#if TRACE
//            long startTicks = VNC.AppLog.Trace5("Start", LOG_APPNAME);
//#endif

            bool isMember = false;

            using (PrincipalContext ADDomain = new PrincipalContext(ContextType.Domain, Domain))
            {
                using (UserPrincipal user = UserPrincipal.FindByIdentity(ADDomain, userID))
                {
                    if (user.IsMemberOf(ADDomain, IdentityType.Name, groupName.Trim()))
                    {
                        isMember = true;
                    }
                }
            }
//#if TRACE
//            VNC.AppLog.Trace5("End", LOG_APPNAME, startTicks);
//#endif

            return isMember;
        }

        public static bool CheckGroupMembership(string userID, string groupName, string Domain)
        {
//#if TRACE
//            long startTicks = VNC.AppLog.Trace5("Start", LOG_APPNAME);
//#endif

            bool isMember = false;

            PrincipalSearchResult<Principal> groups = GetAuthorizationGroupsMembership(userID, Domain);

//#if TRACE
//            VNC.AppLog.Trace5("After GetAuthorizationGroupsMembership", LOG_APPNAME, startTicks);
//#endif


//#if TRACE
//            VNC.AppLog.Trace5(string.Format("After GetAuthorizationGroupsMembership {0}", groups.Count()), LOG_APPNAME, startTicks);
//#endif
            Principal foo = groups.First(g => g.Name == groupName);

//#if TRACE
//            VNC.AppLog.Trace5("After First", LOG_APPNAME, startTicks);
//#endif

            if (foo != null)
            {
                isMember = true;
            }
            int count = groups.Where(g => g.Name == groupName).Count();

//#if TRACE
//            VNC.AppLog.Trace5(string.Format("After Where {0}", count), LOG_APPNAME, startTicks);
//#endif

            using (PrincipalContext ADDomain = new PrincipalContext(ContextType.Domain, Domain))
            {
//#if TRACE
//                VNC.AppLog.Trace5("After new Principal", LOG_APPNAME, startTicks);
//#endif

                using (UserPrincipal user = UserPrincipal.FindByIdentity(ADDomain, userID))
                {
//#if TRACE
//                    VNC.AppLog.Trace5("After FindByIdentity", LOG_APPNAME, startTicks);
//#endif

                    if (count > 0)
                    {
                        isMember = true;
                    }
                }
            }

//#if TRACE
//            VNC.AppLog.Trace5("End", LOG_APPNAME, startTicks);
//#endif

            return isMember;
        }

        public static PrincipalSearchResult<Principal> GetGroupsMembership(string userID, string Domain)
        {
            PrincipalContext ADDomain = new PrincipalContext(ContextType.Domain, Domain);

            return UserPrincipal.FindByIdentity(ADDomain, userID).GetGroups();
        }

        public static PrincipalSearchResult<Principal> GetAuthorizationGroupsMembership(string userID, string Domain)
        {
            PrincipalContext ADDomain = new PrincipalContext(ContextType.Domain, Domain);

            return UserPrincipal.FindByIdentity(ADDomain, userID).GetAuthorizationGroups();
        }

        string ExtractUserName(string path)
        {
            string[] userPath = path.Split(new char[] { '\\' });
            return userPath[userPath.Length - 1];
        }

        bool IsExistInAD(string loginName)
        {
            string userName = ExtractUserName(loginName);
            DirectorySearcher search = new DirectorySearcher();
            search.Filter = String.Format("(SAMAccountName={0})", userName);
            search.PropertiesToLoad.Add("cn");
            SearchResult result = search.FindOne();

            if (result == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static string GetADUserGroups(string userName)
        {
            DirectorySearcher search = new DirectorySearcher();
            search.Filter = String.Format("(cn={0})", userName);
            search.PropertiesToLoad.Add("memberOf");
            StringBuilder groupsList = new StringBuilder();

            SearchResult result = search.FindOne();
            if (result != null)
            {
                int groupCount = result.Properties["memberOf"].Count;

                for (int counter = 0; counter < groupCount; counter++)
                {
                    groupsList.Append((string)result.Properties["memberOf"][counter]);
                    groupsList.Append("|");
                }
            }
            groupsList.Length -= 1; //remove the last '|' symbol

            return groupsList.ToString();
        }

        public static ArrayList GetADGroupUsers(string groupName)
        {
            SearchResult result;
            DirectorySearcher search = new DirectorySearcher();
            search.Filter = String.Format("(cn={0})", groupName);
            search.PropertiesToLoad.Add("member");
            result = search.FindOne();

            ArrayList userNames = new ArrayList();
            if (result != null)
            {
                for (int counter = 0; counter <
                         result.Properties["member"].Count; counter++)
                {
                    string user = (string)result.Properties["member"][counter];
                    userNames.Add(user);
                }
            }

            return userNames;
        }

        public static ArrayList GetAllADDomainGroups()
        {
            ArrayList allGroups = new ArrayList();

            DirectoryEntry searchRoot = new DirectoryEntry();
            DirectorySearcher search = new DirectorySearcher(searchRoot);
            search.Filter = "(objectCategory=group)";
            //search.PropertiesToLoad.Add("samaccountname");

            SearchResult result;
            SearchResultCollection resultCol = search.FindAll();
            if (resultCol != null)
            {
                for (int counter = 0; counter < resultCol.Count; counter++)
                {
                    result = resultCol[counter];

                    allGroups.Add((string)result.Path);
                }
            }

            return allGroups;
        }

        public static ArrayList GetAllADDomainUsers()
        {
            ArrayList allUsers = new ArrayList();

            DirectoryEntry searchRoot = new DirectoryEntry();
            DirectorySearcher search = new DirectorySearcher(searchRoot);
            search.Filter = "(&(objectClass=user)(objectCategory=person))";
            search.PropertiesToLoad.Add("samaccountname");

            SearchResult result;
            SearchResultCollection resultCol = search.FindAll();
            if (resultCol != null)
            {
                for (int counter = 0; counter < resultCol.Count; counter++)
                {
                    result = resultCol[counter];
                    if (result.Properties.Contains("samaccountname"))
                    {
                        allUsers.Add((String)result.Properties["samaccountname"][0]);
                    }
                }
            }

            return allUsers;
        }

        public static ArrayList GetAllADDomainUsers(string domainpath)
        {
            ArrayList allUsers = new ArrayList();

            DirectoryEntry searchRoot = new DirectoryEntry(domainpath);
            DirectorySearcher search = new DirectorySearcher(searchRoot);
            search.Filter = "(&(objectClass=user)(objectCategory=person))";
            search.PropertiesToLoad.Add("samaccountname");

            SearchResult result;
            SearchResultCollection resultCol = search.FindAll();
            if (resultCol != null)
            {
                for (int counter = 0; counter < resultCol.Count; counter++)
                {
                    result = resultCol[counter];
                    if (result.Properties.Contains("samaccountname"))
                    {
                        allUsers.Add((String)result.Properties["samaccountname"][0]);
                    }
                }
            }

            return allUsers;
        }

        public static ArrayList EnumerateDomainControllers()
        {
            ArrayList domainControllers = new ArrayList();
            Domain domain = Domain.GetCurrentDomain();

            foreach (DomainController dc in domain.DomainControllers)
            {
                domainControllers.Add(dc.Name);
            }

            return domainControllers;
        }

        public static ArrayList Foo()
        {
            ArrayList domainControllers = new ArrayList();
            Domain domain = Domain.GetCurrentDomain();

            foreach (DomainController dc in domain.DomainControllers)
            {
                domainControllers.Add(dc.Name);
            }

            return domainControllers;
        }

        public static ArrayList EnumerateDomains()
        {
            ArrayList domains = new ArrayList();
            Forest currentForest = Forest.GetCurrentForest();
            DomainCollection myDomains = currentForest.Domains;

            foreach (Domain objDomain in myDomains)
            {
                domains.Add(objDomain.Name);
            }

            return domains;
        }

        public static ArrayList EnumerateGlobalCatalogs()
        {
            ArrayList globalCatalogs = new ArrayList();
            Forest currentForest = Forest.GetCurrentForest();

            DomainCollection myDomains = currentForest.Domains;

            foreach (GlobalCatalog gc in currentForest.GlobalCatalogs)
            {
                globalCatalogs.Add(gc.Name);
            }

            return globalCatalogs;
        }

        private void foo()
        {
            // set up domain context
            PrincipalContext ctx = new PrincipalContext(ContextType.Domain);

            // find a user
            UserPrincipal user = UserPrincipal.FindByIdentity(ctx, "SomeUserName");

            if (user != null)
            {
                // find the roles....
                var roles = user.GetAuthorizationGroups();

                // enumerate over them
                foreach (Principal p in roles)
                {
                    // do something
                }
            }
        }

        
        public bool UserExists(string username)
        {
           DirectoryEntry de = GetDirectoryEntry();
           DirectorySearcher deSearch = new DirectorySearcher();

           deSearch.SearchRoot = de;
           deSearch.Filter = "(&(objectClass=user) (cn=" + username + "))";

           SearchResultCollection results = deSearch.FindAll();

           return results.Count > 0;
        } 
 
        public static DirectoryEntry GetDirectoryEntry()
        {
           DirectoryEntry de = new DirectoryEntry();
           de.Path = "LDAP://OU=Domain,DC=YourDomain,DC=com";
           de.AuthenticationType = AuthenticationTypes.Secure;

           return de;
        }

        public static String FindName(String userAccount)
        {
           DirectoryEntry entry = GetDirectoryEntry();
           String account = userAccount.Replace(@"CORP\", "");

           try
           {
              DirectorySearcher search = new DirectorySearcher(entry);
              search.Filter = String.Format("(SAMAccountName={0})", account);
              search.PropertiesToLoad.Add("displayName");

              SearchResult result = search.FindOne();

              if (result != null)
              {
                 return result.Properties["displayname"][0].ToString();
              }
              else
              {
                 return "Unknown User";
              }
           }
           catch (Exception ex)
           {
              string debug = ex.Message;

              return debug;
              //return "";
           }
        }

        public void ModifyUser(string userDisplayName, string username, string password)
        {
           DirectoryEntry de = GetDirectoryEntry();
           de.Username = username;
           de.Password = password; 

           DirectorySearcher ds = new DirectorySearcher(de);
           ds.Filter = (String.Format("(&(objectclass=user)(objectcategory=person)(displayname={0}))", userDisplayName));

           ds.SearchScope = SearchScope.Subtree;

           SearchResult results = ds.FindOne();

           if (results != null)
           {
              try
              {
                 DirectoryEntry updateEntry = results.GetDirectoryEntry();
                 updateEntry.Properties["department"].Value = "555";
                 updateEntry.CommitChanges();
                 updateEntry.Close();
              }
              catch (Exception ex)
              {
                 //tbError.Text = ex.ToString();
              }
           }

           de.Close();
        }


        public class ADMethodsAccountManagement
        {

        #region Variables

        private string sDomain = "test.com";
        private string sDefaultOU = "OU=Test Users,OU=Test,DC=test,DC=com";
        private string sDefaultRootOU = "DC=test,DC=com";
        private string sServiceUser = @"ServiceUser";
        private string sServicePassword = "ServicePassword";

        #endregion

        #region Validate Methods

        /// <summary>
        /// Validates the username and password of a given user
        /// </summary>
        /// <param name="sUserName">The username to validate</param>
        /// <param name="sPassword">The password of the username to validate</param>
        /// <returns>Returns True of user is valid</returns>
        public bool ValidateCredentials(string sUserName, string sPassword)
        {
            PrincipalContext oPrincipalContext = GetPrincipalContext();
            return oPrincipalContext.ValidateCredentials(sUserName, sPassword);

        }

        /// <summary>
        /// Checks if the User Account is Expired
        /// </summary>
        /// <param name="sUserName">The username to check</param>
        /// <returns>Returns true if Expired</returns>
        public bool IsUserExpired(string sUserName)
        {
            UserPrincipal oUserPrincipal = GetUser(sUserName);
            if (oUserPrincipal.AccountExpirationDate != null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Checks if user exsists on AD
        /// </summary>
        /// <param name="sUserName">The username to check</param>
        /// <returns>Returns true if username Exists</returns>
        public bool IsUserExisiting(string sUserName)
        {
            if (GetUser(sUserName) == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Checks if user accoung is locked
        /// </summary>
        /// <param name="sUserName">The username to check</param>
        /// <returns>Retruns true of Account is locked</returns>
        public bool IsAccountLocked(string sUserName)
        {
            UserPrincipal oUserPrincipal = GetUser(sUserName);
            return oUserPrincipal.IsAccountLockedOut();
        }
        #endregion

        #region Search Methods

        /// <summary>
        /// Gets a certain user on Active Directory
        /// </summary>
        /// <param name="sUserName">The username to get</param>
        /// <returns>Returns the UserPrincipal Object</returns>
        public UserPrincipal GetUser(string sUserName)
        {
            PrincipalContext oPrincipalContext = GetPrincipalContext();

            UserPrincipal oUserPrincipal = UserPrincipal.FindByIdentity(oPrincipalContext, sUserName);
            return oUserPrincipal;
        }

        /// <summary>
        /// Gets a certain group on Active Directory
        /// </summary>
        /// <param name="sGroupName">The group to get</param>
        /// <returns>Returns the GroupPrincipal Object</returns>
        public GroupPrincipal GetGroup(string sGroupName)
        {
            PrincipalContext oPrincipalContext = GetPrincipalContext();

            GroupPrincipal oGroupPrincipal = GroupPrincipal.FindByIdentity(oPrincipalContext, sGroupName);
            return oGroupPrincipal;
        }

        #endregion

        #region User Account Methods

        /// <summary>
        /// Sets the user password
        /// </summary>
        /// <param name="sUserName">The username to set</param>
        /// <param name="sNewPassword">The new password to use</param>
        /// <param name="sMessage">Any output messages</param>
        public void SetUserPassword(string sUserName, string sNewPassword, out string sMessage)
        {
            try
            {
                UserPrincipal oUserPrincipal = GetUser(sUserName);
                oUserPrincipal.SetPassword(sNewPassword);
                sMessage = "";
            }
            catch (Exception ex)
            {
                sMessage = ex.Message;
            }

        }

        /// <summary>
        /// Enables a disabled user account
        /// </summary>
        /// <param name="sUserName">The username to enable</param>
        public void EnableUserAccount(string sUserName)
        {
            UserPrincipal oUserPrincipal = GetUser(sUserName);
            oUserPrincipal.Enabled = true;
            oUserPrincipal.Save();
        }

        /// <summary>
        /// Force disbaling of a user account
        /// </summary>
        /// <param name="sUserName">The username to disable</param>
        public void DisableUserAccount(string sUserName)
        {
            UserPrincipal oUserPrincipal = GetUser(sUserName);
            oUserPrincipal.Enabled = false;
            oUserPrincipal.Save();
        }

        /// <summary>
        /// Force expire password of a user
        /// </summary>
        /// <param name="sUserName">The username to expire the password</param>
        public void ExpireUserPassword(string sUserName)
        {
            UserPrincipal oUserPrincipal = GetUser(sUserName);
            oUserPrincipal.ExpirePasswordNow();
            oUserPrincipal.Save();

        }

        /// <summary>
        /// Unlocks a locked user account
        /// </summary>
        /// <param name="sUserName">The username to unlock</param>
        public void UnlockUserAccount(string sUserName)
        {
            UserPrincipal oUserPrincipal = GetUser(sUserName);
            oUserPrincipal.UnlockAccount();
            oUserPrincipal.Save();
        }

        /// <summary>
        /// Creates a new user on Active Directory
        /// </summary>
        /// <param name="sOU">The OU location you want to save your user</param>
        /// <param name="sUserName">The username of the new user</param>
        /// <param name="sPassword">The password of the new user</param>
        /// <param name="sGivenName">The given name of the new user</param>
        /// <param name="sSurname">The surname of the new user</param>
        /// <returns>returns the UserPrincipal object</returns>
        public UserPrincipal CreateNewUser(string sOU, string sUserName, string sPassword, string sGivenName, string sSurname)
        {
            if (!IsUserExisiting(sUserName))
            {
                PrincipalContext oPrincipalContext = GetPrincipalContext(sOU);

                UserPrincipal oUserPrincipal = new UserPrincipal(oPrincipalContext, sUserName, sPassword, true /*Enabled or not*/);

                //User Log on Name
                oUserPrincipal.UserPrincipalName = sUserName;
                oUserPrincipal.GivenName = sGivenName;
                oUserPrincipal.Surname = sSurname;
                oUserPrincipal.Save();

                return oUserPrincipal;
            }
            else
            {
                return GetUser(sUserName);
            }
        }

        /// <summary>
        /// Deletes a user in Active Directory
        /// </summary>
        /// <param name="sUserName">The username you want to delete</param>
        /// <returns>Returns true if successfully deleted</returns>
        public bool DeleteUser(string sUserName)
        {
            try
            {
                UserPrincipal oUserPrincipal = GetUser(sUserName);

                oUserPrincipal.Delete();
                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region Group Methods

        /// <summary>
        /// Creates a new group in Active Directory
        /// </summary>
        /// <param name="sOU">The OU location you want to save your new Group</param>
        /// <param name="sGroupName">The name of the new group</param>
        /// <param name="sDescription">The description of the new group</param>
        /// <param name="oGroupScope">The scope of the new group</param>
        /// <param name="bSecurityGroup">True is you want this group to be a security group, false if you want this as a distribution group</param>
        /// <returns>Retruns the GroupPrincipal object</returns>
        public GroupPrincipal CreateNewGroup(string sOU, string sGroupName, string sDescription, GroupScope oGroupScope, bool bSecurityGroup)
        {
            PrincipalContext oPrincipalContext = GetPrincipalContext(sOU);

            GroupPrincipal oGroupPrincipal = new GroupPrincipal(oPrincipalContext, sGroupName);
            oGroupPrincipal.Description = sDescription;
            oGroupPrincipal.GroupScope = oGroupScope;
            oGroupPrincipal.IsSecurityGroup = bSecurityGroup;
            oGroupPrincipal.Save();

            return oGroupPrincipal;
        }

        /// <summary>
        /// Adds the user for a given group
        /// </summary>
        /// <param name="sUserName">The user you want to add to a group</param>
        /// <param name="sGroupName">The group you want the user to be added in</param>
        /// <returns>Returns true if successful</returns>
        public bool AddUserToGroup(string sUserName, string sGroupName)
        {
            try
            {
                UserPrincipal oUserPrincipal = GetUser(sUserName);
                GroupPrincipal oGroupPrincipal = GetGroup(sGroupName);
                if (oUserPrincipal != null && oGroupPrincipal != null)
                {
                    if (!IsUserGroupMember(sUserName, sGroupName))
                    {
                        oGroupPrincipal.Members.Add(oUserPrincipal);
                        oGroupPrincipal.Save();
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Removes user from a given group
        /// </summary>
        /// <param name="sUserName">The user you want to remove from a group</param>
        /// <param name="sGroupName">The group you want the user to be removed from</param>
        /// <returns>Returns true if successful</returns>
        public bool RemoveUserFromGroup(string sUserName, string sGroupName)
        {
            try
            {
                UserPrincipal oUserPrincipal = GetUser(sUserName);
                GroupPrincipal oGroupPrincipal = GetGroup(sGroupName);
                if (oUserPrincipal != null && oGroupPrincipal != null)
                {
                    if (IsUserGroupMember(sUserName, sGroupName))
                    {
                        oGroupPrincipal.Members.Remove(oUserPrincipal);
                        oGroupPrincipal.Save();
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Checks if user is a member of a given group
        /// </summary>
        /// <param name="sUserName">The user you want to validate</param>
        /// <param name="sGroupName">The group you want to check the membership of the user</param>
        /// <returns>Returns true if user is a group member</returns>
        public bool IsUserGroupMember(string sUserName, string sGroupName)
        {
            UserPrincipal oUserPrincipal = GetUser(sUserName);
            GroupPrincipal oGroupPrincipal = GetGroup(sGroupName);

            if (oUserPrincipal != null && oGroupPrincipal != null)
            {
                return oGroupPrincipal.Members.Contains(oUserPrincipal);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Gets a list of the users group memberships
        /// </summary>
        /// <param name="sUserName">The user you want to get the group memberships</param>
        /// <returns>Returns an arraylist of group memberships</returns>
        public ArrayList GetUserGroups(string sUserName)
        {
            ArrayList myItems = new ArrayList();
            UserPrincipal oUserPrincipal = GetUser(sUserName);

            PrincipalSearchResult<Principal> oPrincipalSearchResult = oUserPrincipal.GetGroups();

            foreach (Principal oResult in oPrincipalSearchResult)
            {
                myItems.Add(oResult.Name);
            }
            return myItems;
        }

        /// <summary>
        /// Gets a list of the users authorization groups
        /// </summary>
        /// <param name="sUserName">The user you want to get authorization groups</param>
        /// <returns>Returns an arraylist of group authorization memberships</returns>
        public ArrayList GetUserAuthorizationGroups(string sUserName)
        {
            ArrayList myItems = new ArrayList();
            UserPrincipal oUserPrincipal = GetUser(sUserName);

            PrincipalSearchResult<Principal> oPrincipalSearchResult = oUserPrincipal.GetAuthorizationGroups();

            foreach (Principal oResult in oPrincipalSearchResult)
            {
                myItems.Add(oResult.Name);
            }
            return myItems;
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Gets the base principal context
        /// </summary>
        /// <returns>Retruns the PrincipalContext object</returns>
        public PrincipalContext GetPrincipalContext()
        {
            PrincipalContext oPrincipalContext = new PrincipalContext(ContextType.Domain, sDomain, sDefaultOU, ContextOptions.SimpleBind, sServiceUser, sServicePassword);
            return oPrincipalContext;
        }

        /// <summary>
        /// Gets the principal context on specified OU
        /// </summary>
        /// <param name="sOU">The OU you want your Principal Context to run on</param>
        /// <returns>Retruns the PrincipalContext object</returns>
        public PrincipalContext GetPrincipalContext(string sOU)
        {
            PrincipalContext oPrincipalContext = new PrincipalContext(ContextType.Domain, sDomain, sOU, ContextOptions.SimpleBind, sServiceUser, sServicePassword);
            return oPrincipalContext;
        }

        #endregion

        }

        //ADMethodsAccountManagement ADMethods = new ADMethodsAccountManagement();

        //UserPrincipal myUser = ADMethods.GetUser("Test");
        //myUser.GivenName = "Given Name";
        //myUser.Surname = "Surname";
        //myUser.MiddleName = "Middle Name";
        //myUser.EmailAddress = "Email Address";
        //myUser.EmployeeId = "Employee ID";
        //myUser.Save();
    }
}
