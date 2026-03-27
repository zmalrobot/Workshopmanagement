namespace Workshopmanagement.DatabaseManagment.DataObject
{
    /// <summary>
    /// Implemented by DataObjects that contain user-typed strings.
    /// Called by DataAccessBase before every insert and update
    /// to ensure no leading/trailing whitespace is persisted.
    /// </summary>
    public interface ISanitizable
    {
        void Sanitize();
    }
}
