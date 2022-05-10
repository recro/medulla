// Licensed to the Medulla Contributors under one or more agreements.
// The Medulla Contributors licenses this file to you under the Apache 2.0 license.
// See the LICENSE file in the project root for more information.

namespace Medulla.Editor.Client.Library.Utilities.Unique
{
    /// <summary>
    /// Unique id
    /// </summary>
    public class UniqueId
    {
        /// <summary>
        /// Id
        /// </summary>
        public readonly string Id;

        /// <summary>
        /// Unique Id
        /// </summary>
        public UniqueId()
        {
            var guid = Guid.NewGuid().ToString();
            Id = guid;
        }

        /// <summary>
        /// Equals
        /// </summary>
        public override bool Equals(object? obj)
        {
            if (obj == null || !(GetType() == obj.GetType()))
            {
                return false;
            }
            var otherUniqueId = (UniqueId)obj;
            return otherUniqueId.Id == Id;
        }

        /// <summary>
        /// Get Hash Code
        /// </summary>
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        /// <summary>
        /// Equals
        /// </summary>
        protected bool Equals(UniqueId other)
        {
            return Id == other.Id;
        }

    }
}
