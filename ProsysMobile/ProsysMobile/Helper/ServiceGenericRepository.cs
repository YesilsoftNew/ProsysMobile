using System;
using System.Collections.Generic;
using System.Text;

namespace WiseMobile.Helper
{
    public interface IServiceGenericRepository<TEntity> where TEntity : class
    {
        public static StringBuilder sSQL = new StringBuilder();
    }
    public class ServiceGenericRepository<TEntity> : IServiceGenericRepository<TEntity> where TEntity : class, new()
    {
        //public StringBuilder sSQL { get; set; } = new StringBuilder();
        public static StringBuilder sSQL = new StringBuilder();

        public ServiceGenericRepository()
        {

        }
    }
}
