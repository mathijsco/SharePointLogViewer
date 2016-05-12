using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Web.Administration;

namespace SharePointLogViewer.Common.Utilities
{
    public static class AppPoolUtility
    {
        public static bool TryRecycle(string appPoolName)
        {
            try
            {
                var appPool = GetAllActive().FirstOrDefault(a => a.Name.Equals(appPoolName, StringComparison.InvariantCultureIgnoreCase));
                if (appPool == null)
                    return false;

                appPool.Recycle();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public static IEnumerable<ApplicationPool> GetAllActive()
        {
            var manager = new ServerManager();
            var appPools = manager.ApplicationPools.Where(
                a => a.State == ObjectState.Started
                     && a.ManagedPipelineMode == ManagedPipelineMode.Integrated
                );

            foreach (var appPool in appPools)
            {
                yield return appPool;
            }
        }
    }
}
