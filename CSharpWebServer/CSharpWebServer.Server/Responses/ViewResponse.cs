namespace CSharpWebServer.Server.Responses
{
    using System.IO;
    using System.Linq;
    using CSharpWebServer.Server.Http;

    public class ViewResponse : HttpResponse
    {
        private const char PathSeparator = '/';

        public ViewResponse(string viewPath, string controllerName) : base(HttpStatusCode.OK)
        => this.GetHtml(viewPath, controllerName);


        private void GetHtml(string viewName,string controllerName)
        {
            if(viewName.Contains(PathSeparator))
            {
                viewName = viewName.Split(PathSeparator).Last().ToString();
            }

            var directory = Directory.GetCurrentDirectory();
            //var (controllerName,actionName) = SplitAbsolutePath(filePath);
            var viewPath = Path.GetFullPath(Path.Combine(directory, @"..\..\..\","Views", controllerName, viewName.TrimStart(PathSeparator) + ".cshtml"));
            if (!File.Exists(viewPath))
            {
                PrepareMisingViewError(controllerName, viewName);
                return;
            }

            var viewContent = File.ReadAllText(viewPath);

            this.PrepareContent(viewContent, HttpContentType.Html);
        }


        private void PrepareMisingViewError(string controllerName, string viewName )
        {
            this.StatusCode = HttpStatusCode.NotFound;
            var errorMessage = $"View {controllerName}/{viewName} was not found!";
            this.PrepareContent(errorMessage, HttpContentType.Html);
        }
    }
}
