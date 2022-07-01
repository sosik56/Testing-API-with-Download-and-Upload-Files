using RestSharp;

namespace examTaskVariant2.UtilityClasses
{
    public static class ApiUtil
    {
        public static RestResponse RestGetRequest(string endpoint)
        {
            using (RestClient client = new RestClient())
            {
                var request = new RestRequest(endpoint, Method.Get);
                var result = client.ExecuteAsync(request);

                return result.Result;
            }
        }

        public static RestResponse RestPostRequest(string endpoint)
        {
            using (RestClient client = new RestClient())
            {                
                var request = new RestRequest(endpoint, Method.Post);
                
                var result = client.ExecuteAsync(request);

                return result.Result;
            }
        }

        public static RestResponse RestPostWithFileRequest(string endpoint, string filePath, string fileName)
        {
            using (RestClient client = new RestClient())
            {
                var request = new RestRequest(endpoint, Method.Post);
                request.AlwaysMultipartFormData = true;                
                request.AddFile(fileName, filePath, "multipart/form-data");
                var result = client.ExecuteAsync(request);    

                return result.Result;
            }
        }

        public static RestResponse RestPostRequestWithParametrs(string endpoint,string parametrName, string base64, string contentType)
        {
            using (RestClient client = new RestClient())
            {  
                var request = new RestRequest(endpoint, Method.Post);
                request.AddParameter(parametrName, base64); 
                request.AddParameter("contentType", contentType);
                var result = client.ExecuteAsync(request);

                return result.Result;
            }
        }

    }
}
