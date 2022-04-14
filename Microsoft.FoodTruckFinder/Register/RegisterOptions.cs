using CommandLine;

namespace Microsoft.FoodTruckFinder.Register
{
    [Verb("register")]
    internal class RegisterOptions
    {
        [Option("key-id", Required = true, HelpText = "Enter your SODA API key")]
        public string KeyId { get; set; } = "";

        [Option("key-secret", Required = true, HelpText = "Enter your SODA API secret")]
        public string KeySecret { get; set; } = "";
    }
}
