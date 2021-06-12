namespace CSharpWebServer.Server.Results
{
    using System.IO;
    using System.Linq;
    using CSharpWebServer.Server.Http;

    public class ViewResults : ActionResult
    {
        private const char PathSeparator = '/';

        public ViewResults(HttpResponse response,string viewPath, string controllerName,object model) : base(response)
        => this.GetHtml(viewPath, controllerName, model);


        private void GetHtml(string viewName,string controllerName,object model)
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

            if(model != null)
            {
                viewContent = PopulateModel(viewContent, model);
            }

            this.PrepareContent(viewContent, HttpContentType.Html);
        }


        private void PrepareMisingViewError(string controllerName, string viewName )
        {
            this.StatusCode = HttpStatusCode.NotFound;
            var errorMessage = $"View {controllerName}/{viewName} was not found!";
            this.PrepareContent(errorMessage, HttpContentType.Html);
        }

        private string PopulateModel(string viewContent,object model)
        {
            var data = model.GetType().GetProperties().Select(pr => new
            {
                Name = pr.Name,
                Value = pr.GetValue(model)
            });
            var openingBrackets = "{{";
            var closingBrackets = "}}";
            foreach (var entry in data)
            {
                
                viewContent = viewContent.Replace($"{openingBrackets}{entry.Name}{closingBrackets}",entry.Value.ToString());
            }
            return viewContent;
        }
    }
}
