using System.Collections.Generic;

namespace Global
{
    using Managers;

    public static class Services
    {
        private static List<BaseManager> managersList = new List<BaseManager>();

        public static void AddManager(BaseManager baseManager)
        {
            if (baseManager != null)
            {
                managersList.Add(baseManager);
            }
        }

        public static T GetManager<T>() where T : BaseManager
        {
            return (T)managersList.Find(x => x.ManagerType == typeof(T));
        }
    }
}