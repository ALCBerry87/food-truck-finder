using Microsoft.FoodTruckFinder.Common;
using Newtonsoft.Json;

namespace Microsoft.FoodTruckFinder.Register
{
    internal class RegisterService
    {
        public async Task Register(RegisterOptions options)
        {
            var json = JsonConvert.SerializeObject(new ApiCredentials()
            {
                Id = options.KeyId,
                Secret = options.KeySecret
            });

            await File.WriteAllTextAsync(Constants.DEFAULT_CREDENTIALS_PATH, json); //TODO: handle errors
        }
    }
}