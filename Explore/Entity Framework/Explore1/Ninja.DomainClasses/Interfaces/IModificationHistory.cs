using System;

namespace NinjaDomain.Classes.Interfaces
{
    public interface IModificationHistory
    {
        Nullable<DateTime> DateModified { get; set; }
        Nullable<DateTime> DateCreated { get; set; }
        bool IsDirty { get; set; }
    }
}
