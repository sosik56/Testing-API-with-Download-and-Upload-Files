namespace examTaskVariant2.UtilityClasses
{
    public static class TestDbApi
    {
        private static string _apiUrl = UtilityClass.ReturnValue("apiUrl", "config");

        public static string GetTokenByNumVariant(string numberOfVariant)
        {
            var result = ApiUtil.RestPostRequest($"{_apiUrl}/token/get?variant={numberOfVariant}");
            return ApiUtil.RestPostRequest($"{_apiUrl}/token/get?variant={numberOfVariant}").Content;
        }

        public static string GetJsonListOfTestByProjectId(string projectId)
        {
            var result = ApiUtil.RestPostRequest($"{_apiUrl}/test/get/json?projectId={projectId}");
            return result.Content;
        }

        public static string CreateTestWithImageAndLog(string sid, string projectName, string testName , string methodName, string hostName,
            string imageContent, string logs)
        {
            var result = ApiUtil.RestPostRequest($"{_apiUrl}/test/put?SID={sid}" +
                                                                   $"&projectName={projectName}" +
                                                                   $"&testName={testName}" +
                                                                   $"&methodName={methodName}" +
                                                                   $"&env={hostName}");

            string id = result.Content;
            AttachScreeen(id, imageContent, "image/png");
            PutLogs(id, logs);
            return result.Content;
        } 

        public static void AttachScreeen(string testId, string content, string contentType)
        {
            var result = ApiUtil.RestPostRequestWithParametrs($"{_apiUrl}/test/put/attachment?testId={testId}", "content", content, contentType);                                                                            
        }        

        public static void PutLogs(string testId, string logs)
        {
            var result = ApiUtil.RestPostRequestWithParametrs($"{_apiUrl}/test/put/log?testId={testId}", "content", logs, "");

        }
    }
}
