﻿using JccApi.Entities.Base;

namespace JccApi.Entities
{
    public class UserType : TypeEntity<int>
    {
        protected UserType() { }

        public UserType(int id, string description)
        {
            Id = id;
            Description = description;
        }
    }
}
