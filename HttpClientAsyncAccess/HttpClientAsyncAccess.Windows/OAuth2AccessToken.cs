using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace HttpClientAsyncAccess
{
    [DataContract]
    public class OAuth2AccessToken
    {
        // {"token_type":"bearer","access_token":"AAAAAAAAAAAAAAAAAAAAAPPMVwAAAAAA77uFK8FCnOnw5HagSCNxY93HjXo%3D0Ty2nkgdSIE0JSz0gTfaA3P6yaAS7V81fUm9JmdD9LYTzrCgc4"}
        [DataMember]
        public string token_type { get; set; }

        [DataMember]
        public string access_token { get; set; }
    }
}
