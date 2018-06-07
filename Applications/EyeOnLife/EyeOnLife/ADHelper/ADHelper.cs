using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using VNC;

namespace ADHelper
{
    public class ADHelper
    {
        //private static int CLASS_BASE_ERRORNUMBER = SQLInformation.ErrorNumbers.EYEONLIFE;
        private const string LOG_APPNAME = "EyeOnLife";

        public static bool CheckDirectGroupMembership(string userID, string groupName, string Domain)
        {
#if TRACE
            long startTicks = VNC.AppLog.Trace5("Start", LOG_APPNAME);
#endif

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
#if TRACE
            VNC.AppLog.Trace5("End", LOG_APPNAME, startTicks);
#endif

            return isMember;
        }

        public static bool CheckGroupMembership(string userID, string groupName, string Domain)
        {
#if TRACE
            long startTicks = VNC.AppLog.Trace5("Start", LOG_APPNAME);
#endif

            bool isMember = false;

            PrincipalSearchResult<Principal> groups = GetAuthorizationGroupsMembership(userID, Domain);

#if TRACE
            VNC.AppLog.Trace5("After GetAuthorizationGroupsMembership", LOG_APPNAME, startTicks);
#endif


#if TRACE
            VNC.AppLog.Trace5(string.Format("After GetAuthorizationGroupsMembership {0}", groups.Count()), LOG_APPNAME, startTicks);
#endif
            Principal foo = groups.First(g => g.Name == groupName);

#if TRACE
            VNC.AppLog.Trace5("After First", LOG_APPNAME, startTicks);
#endif

            if (foo != null)
            {
            	isMember = true;
            }
            int count = groups.Where(g => g.Name == groupName).Count();

#if TRACE
            VNC.AppLog.Trace5(string.Format("After Where {0}", count), LOG_APPNAME, startTicks);
#endif

            using (PrincipalContext ADDomain = new PrincipalContext(ContextType.Domain, Domain))
            {
#if TRACE
                VNC.AppLog.Trace5("After new Principal", LOG_APPNAME, startTicks);
#endif

                using (UserPrincipal user = UserPrincipal.FindByIdentity(ADDomain, userID))
                {
#if TRACE
                    VNC.AppLog.Trace5("After FindByIdentity", LOG_APPNAME, startTicks);
#endif

                    if (count > 0)
                    {
                        isMember = true;
                    }
                }
            }

#if TRACE
            VNC.AppLog.Trace5("End", LOG_APPNAME, startTicks);
#endif

            return isMember;
        }

        public static PrincipalSearchResult<Principal> GetGroupsMembership(string userID, string Domain)
        {
#if TRACE
            long startTicks = VNC.AppLog.Trace5("Start", LOG_APPNAME);
#endif
            PrincipalContext ADDomain = new PrincipalContext(ContextType.Domain, Domain);
#if TRACE
            VNC.AppLog.Trace5("End", LOG_APPNAME, startTicks);
#endif
            return UserPrincipal.FindByIdentity(ADDomain, userID).GetGroups();
        }

        public static PrincipalSearchResult<Principal> GetAuthorizationGroupsMembership(string userID, string Domain)
        {
#if TRACE
            long startTicks = VNC.AppLog.Trace5("Start", LOG_APPNAME);
#endif
            PrincipalContext ADDomain = new PrincipalContext(ContextType.Domain, Domain);
#if TRACE
            VNC.AppLog.Trace5("End", LOG_APPNAME, startTicks);
#endif
            return UserPrincipal.FindByIdentity(ADDomain, userID).GetAuthorizationGroups();
        }

    }
}
