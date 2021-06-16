namespace CSharpWebServer.Server.Results
{
    using System;
    using System.Collections;
    using System.IO;
    using System.Linq;
    using System.Text;
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
            var viewPath = Path.GetFullPath(Path.Combine(directory, @"..\..\..\","Views", controllerName, viewName.TrimStart(PathSeparator) + ".cshtml"));
            if (!File.Exists(viewPath))
            {
                PrepareMisingViewError(controllerName, viewName);
                return;
            }

            var viewContent = File.ReadAllText(viewPath);


            var layoutPath = Path.GetFullPath(Path.Combine(directory, @"..\..\..\", "Views", "Layout.cshtml"));

            if(File.Exists(layoutPath))
            {
                var layoutContent = File.ReadAllText(layoutPath);

                viewContent = layoutContent.Replace("@RenderBody()", viewContent);
            }
            if(model != null)
            {
                viewContent = PopulateModel(viewContent, model);
            }

            this.SetContent(viewContent, HttpContentType.Html);
        }


        private void PrepareMisingViewError(string controllerName, string viewName )
        {
            this.StatusCode = HttpStatusCode.NotFound;
            var errorMessage = $"View {controllerName}/{viewName} was not found!";
            this.SetContent(errorMessage, HttpContentType.Html);
        }

        private static string PopulateModel(string viewContent, object model)
        {
            if (model is not IEnumerable)
            {
                viewContent = PopulateModelProperties(viewContent, "Model", model);
            }

            var result = new StringBuilder();

            var lines = viewContent
                .Split(Environment.NewLine)
                .Select(line => line.Trim());

            var inLoop = false;
            string loopModelName = null;
            StringBuilder loopContent = null;

            foreach (var line in lines)
            {
                if (line.StartsWith("@foreach"))
                {
                    if (model is not IEnumerable)
                    {
                        throw new InvalidOperationException("Using a foreach in the loop requires the view model to be a collection.");
                    }

                    inLoop = true;

                    loopModelName = line
                        .Split()
                        .SkipWhile(l => l.Contains("var"))
                        .FirstOrDefault();

                    if (loopModelName == null)
                    {
                        throw new InvalidOperationException("The foreach statement in the view is not valid.");
                    }

                    continue;
                }

                if (inLoop)
                {
                    if (line.StartsWith("{"))
                    {
                        loopContent = new StringBuilder();
                    }
                    else if (line.StartsWith("}"))
                    {
                        var loopTemplate = loopContent.ToString();

                        foreach (var item in (IEnumerable)model)
                        {
                            var loopResult = PopulateModelProperties(loopTemplate, loopModelName, item);

                            result.AppendLine(loopResult);
                        }

                        inLoop = false;
                    }
                    else
                    {
                        loopContent.AppendLine(line);
                    }

                    continue;
                }

                result.AppendLine(line);
            }

            return result.ToString();
        }

        private static string PopulateModelProperties(string content, string modelName, object model)
        {
            var data = model
                .GetType()
                .GetProperties()
                .Select(pr => new
                {
                    pr.Name,
                    Value = pr.GetValue(model)
                });

            foreach (var entry in data)
            {
                content = content.Replace($"@{modelName}.{entry.Name}", entry.Value.ToString());
            }

            return content;
        }
    }
}