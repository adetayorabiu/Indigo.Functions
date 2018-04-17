﻿namespace DependencyInjectionSample
{
    public interface IStorageAccess
    {
    }

    public class StorageAccess : IStorageAccess
    {
        private readonly ITableAccess _tableAccess;

        public StorageAccess(ITableAccess tableAccess)
        {
            _tableAccess = tableAccess;
        }
    }
}