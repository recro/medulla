// Licensed to the Medulla Contributors under one or more agreements.
// The Medulla Contributors licenses this file to you under the Apache 2.0 license.
// See the LICENSE file in the project root for more information.

using System;

namespace Medulla.Frontend.Client.Library.Utilities.Unique
{
    public class UniqueId
    {
        private string Id;

        public UniqueId()
        {
            var guid = System.Guid.NewGuid().ToString();
            Id = guid;
            Console.WriteLine(Id);
        }

        public override bool Equals(object? obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            UniqueId _otherUniqueId = (UniqueId)obj;
            return _otherUniqueId.Id == this.Id;
        }

        protected bool Equals(UniqueId other)
        {
            return Id == other.Id;
        }

    }
}
