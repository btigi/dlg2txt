
namespace DlgToTxt.Model
{
    public enum ExitCode : int
    {
        Success = 0,
        InvalidArguments,
        NoTlkFile,
        NoListingFile,
        GenericError
    }
}