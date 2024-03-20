using RDotNet;

namespace Graph.Auxiliary
{
    public class REngineInstance
    {
        static REngineInstance()
        {
            // Set the path to your R installation
            //REngine.SetEnvironmentVariables( "c:/program files/r/r-4.2.1/bin/x64", "c:/program files/r/r-4.2.1" );
            REngine.SetEnvironmentVariables();
            // Initialize the R engine
            REngine = REngine.GetInstance();
            // Workaround - explicitly include R libs in PATH so R environment can find them.  Not sure why R can't find them when
            // we set this via Environment.SetEnvironmentVariable
            REngine.Evaluate( "Sys.setenv(PATH = paste(\"C:/Program Files/R/R-4.2.1/bin/x64\", Sys.getenv(\"PATH\"), sep=\";\"))" );

            REngine.Initialize();
        }

        public static REngine REngine { get; set; }
    }
}