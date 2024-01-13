namespace CommonBasicLibraries.MediaHelpers.Services;
public class MockMediaForce : IMediaForcePlay
{
    Task IMediaForcePlay.ForcePlayAsync()
    {
        return Task.CompletedTask; //so if its running on nonwindows and there is no autoclicker, then just won't do anything there.
    }
}