using RDotNet;

namespace StepsCounting.Auxiliary
{
    public class REngineWrapper : IDisposable
    {
        public REngineWrapper()
        {
            // Set the path to your R installation
            REngine.SetEnvironmentVariables();

            // Initialize the R engine
            REngine = REngine.GetInstance();
            REngine.Initialize();
        }

        public REngine REngine { get; set; }

        public void Dispose()
        {
            REngine?.Dispose();
        }
    }
}