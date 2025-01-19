namespace EzMoq.EfCore.Interfaces
{
    internal interface IVerifySave
    {
        void VerifyChangesSaved();

        void VerifyChangesNeverSaved();

        void VerifyChangesSavedAsync();

        void VerifyChangesNeverSavedAsync();
    }
}