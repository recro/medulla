// Licensed to the Medulla Contributors under one or more agreements.
// The Medulla Contributors licenses this file to you under the Apache 2.0 license.
// See the LICENSE file in the project root for more information.

namespace Medulla.Frontend.Client.Library.Utilities.Unique
{
    public class UniqueId
    {
        public readonly string Id;

        public UniqueId()
        {
            var guid = System.Guid.NewGuid().ToString();
            Id = guid;
        }

        public override bool Equals(object? obj)
        {
            if (obj == null || !(this.GetType() == obj.GetType()))
            {
                return false;
            }
            UniqueId otherUniqueId = (UniqueId)obj;
            return otherUniqueId.Id == this.Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        protected bool Equals(UniqueId other)
        {
            return Id == other.Id;
        }

    }
}
