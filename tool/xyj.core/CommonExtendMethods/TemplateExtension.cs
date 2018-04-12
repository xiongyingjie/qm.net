namespace xyj.core.CommonExtendMethods
{
    public static class TemplateExtension
    {
        public static string Render<T>(this T model, string path)
        {
           return TemplateUtility.Render(model, path);
        }
    }
}