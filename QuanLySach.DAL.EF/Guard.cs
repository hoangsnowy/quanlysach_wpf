using QuanLySach.Common.Exceptions;

namespace QuanLySach.DAL.EF
{
    public static class Guard
    {
        public static void ThrowIfObjectNotFound(object obj, int id)
        {
            if (obj == null)
            {
                throw new ObjectNotFoundException($"Can't not find the {nameof(obj)} : {id}");
            }
        }
    }
}
