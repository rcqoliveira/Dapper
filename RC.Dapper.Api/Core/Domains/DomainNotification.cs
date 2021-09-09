﻿namespace RC.Dapper.Api.Core.Domains
{
    public class DomainNotification
    {
        public string Key { get; private set; }
        public string Value { get; private set; }

        public DomainNotification(string key, string value)
        {
            Key = key;
            Value = value;
        }

    }
}
