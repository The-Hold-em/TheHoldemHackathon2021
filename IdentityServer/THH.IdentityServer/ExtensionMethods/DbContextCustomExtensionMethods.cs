using Microsoft.EntityFrameworkCore;

using System.Threading.Tasks;

namespace THH.IdentityServer.ExtensionMethods
{
    public static class DbContextCustomExtensionMethods
    {
        public static  Task ClearAsync<T>(this DbSet<T> dbSet) where T : class =>  Task.Run(() => dbSet.RemoveRange(dbSet));
    }
}
