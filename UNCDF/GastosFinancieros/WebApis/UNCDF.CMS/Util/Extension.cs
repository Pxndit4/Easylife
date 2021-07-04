using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Routing;
using System.Web.Script.Serialization;
using UNCDF.Layers.Model;
using System.Configuration;

namespace UNCDF.CMS
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
    public class OnlyNumericAttribute : RegularExpressionAttribute
    {
        private const string pattern = @"^[0-9\b]+$";

        static OnlyNumericAttribute()
        {
            System.Web.Mvc.DataAnnotationsModelValidatorProvider.RegisterAdapter(typeof(OnlyNumericAttribute), typeof(System.Web.Mvc.RegularExpressionAttributeAdapter));
        }

        public OnlyNumericAttribute()
            : base(pattern)
        {
        }
    }

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
    public class NoSpacessAttribute : RegularExpressionAttribute
    {
        private const string pattern = @"[^\s]+";

        static NoSpacessAttribute()
        {
            System.Web.Mvc.DataAnnotationsModelValidatorProvider.RegisterAdapter(typeof(NoSpacessAttribute), typeof(System.Web.Mvc.RegularExpressionAttributeAdapter));
        }

        public NoSpacessAttribute()
            : base(pattern)
        {
        }
    }

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
    public class EmailMultiAttribute : RegularExpressionAttribute
    {
        private const string pattern = @"^(;?[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)+$";

        static EmailMultiAttribute()
        {
            System.Web.Mvc.DataAnnotationsModelValidatorProvider.RegisterAdapter(typeof(EmailMultiAttribute), typeof(System.Web.Mvc.RegularExpressionAttributeAdapter));
        }


        public EmailMultiAttribute()
            : base(pattern)
        {
        }

    }

    public static class Extension
    {

        public const string S3Server = "https://uncdfbucket.s3.us-east-2.amazonaws.com/";
        public static readonly string pathProject = ConfigurationManager.AppSettings["URLProject"];
        public static readonly string pathBanner = ConfigurationManager.AppSettings["URLBanner"];
        public static readonly string pathCountry = ConfigurationManager.AppSettings["URLCountry"];
        public static readonly string pathIntroduction = ConfigurationManager.AppSettings["URLIntroduction"];
        public static readonly string pathLanguage = ConfigurationManager.AppSettings["URLLanguage"];
        public static readonly string pathOng = ConfigurationManager.AppSettings["URLOng"];
        public static readonly string pathTestimonial = ConfigurationManager.AppSettings["URLTestPath"];
        public static readonly string pathMultimedia = ConfigurationManager.AppSettings["URLMultimedia"];





        //public static string GetOngPath()
        //{
        //    return ConfigurationManager.AppSettings["URLOng"].ToString();
        //}

        public static string SerializeJson(this WebViewPage value, object data)
        {
            JavaScriptSerializer objSer = new JavaScriptSerializer();
            return objSer.Serialize(data);
        }

        public static string GetTargetName(this WebViewPage value)
        {
            return "trg";
        }

        public static int GetIdLanguageENG()
        {
            return 2;
        }

        public static List<MStatic> GetStatus()
        {
            List<MStatic> lisQuery = new List<MStatic>();
            MStatic entRow;
            entRow = new MStatic();
            entRow.Id = "1";
            entRow.Value = "Active";
            lisQuery.Add(entRow);
            entRow = new MStatic();
            entRow.Id = "0";
            entRow.Value = "Inactive";
            lisQuery.Add(entRow);
            return lisQuery;
        }

        public static List<MStatic> GetOrder()
        {
            List<MStatic> lisQuery = new List<MStatic>();
            for (int i = 1; i <= 10; i++)
            {
                MStatic entRow = new MStatic();
                entRow.Id = i.ToString();
                entRow.Value = i.ToString();
                lisQuery.Add(entRow);
            }

            return lisQuery;
        }


        public static List<MStatic> GetTypeDonation()
        {
            List<MStatic> lisQuery = new List<MStatic>();
            MStatic entRow;
            entRow = new MStatic();
            entRow.Id = "-1";
            entRow.Value = "All";
            lisQuery.Add(entRow);
            entRow = new MStatic();
            entRow.Id = "0";
            entRow.Value = "Anonymous";
            lisQuery.Add(entRow);
            return lisQuery;
        }

        public static List<MStatic> GetRegistered()
        {
            List<MStatic> lisQuery = new List<MStatic>();
            MStatic entRow;
            entRow = new MStatic();
            entRow.Id = "1";
            entRow.Value = "Yes";
            lisQuery.Add(entRow);
            entRow = new MStatic();
            entRow.Id = "0";
            entRow.Value = "Not";
            lisQuery.Add(entRow);
            return lisQuery;
        }


        public static List<MStatic> GetTypeInterface()
        {
            List<MStatic> lisQuery = new List<MStatic>();
            MStatic entRow;
            entRow = new MStatic();
            entRow.Id = "2";
            entRow.Value = "Web";
            lisQuery.Add(entRow);
            entRow = new MStatic();
            entRow.Id = "1";
            entRow.Value = "App";
            lisQuery.Add(entRow);
            return lisQuery;
        }

        public static List<MStatic> GetTypeFile()
        {
            List<MStatic> lisQuery = new List<MStatic>();
            MStatic entRow;
            entRow = new MStatic();
            entRow.Id = "1";
            entRow.Value = "Image";
            lisQuery.Add(entRow);
            entRow = new MStatic();
            entRow.Id = "2";
            entRow.Value = "Video";
            lisQuery.Add(entRow);
            return lisQuery;
        }

        public static string RegxCATipoDato(this WebViewPage value, int tipo)
        {
            if (tipo == (int)TipoDatoCampoAdicional.Numerico)
                return @"^[0-9\b]+$";
            else if (tipo == (int)TipoDatoCampoAdicional.Cadena)
                return "";
            else if (tipo == (int)TipoDatoCampoAdicional.Fecha)
                return "^(?:[012]?[0-9]|3[01])[./-](?:0?[1-9]|1[0-2])[./-](?:[0-9]{2}){1,2}$";

            return null;

        }

        public static List<SelectListItem> BooleanDropDownList(this WebViewPage value)
        {
            return new List<SelectListItem>() { new SelectListItem { Value = "false", Text = "No" }, new SelectListItem { Value = "true", Text = "Si" } };
        }

        public static string BooleanText(this bool value)
        {
            if (value)
                return "Si";
            else
                return "No";
        }

        public static MvcHtmlString Link(this UrlHelper urlHelper, string actionName, string controllerName, RouteValueDictionary routeValues)
        {
            var repID = Guid.NewGuid().ToString();
            var lnk = urlHelper.Action(actionName, controllerName, routeValues);
            HttpRequestBase objRequest = urlHelper.RequestContext.HttpContext.Request;
            string objTarget = "";
            if (objRequest != null)
            {
                objTarget = objRequest.Params["trg"] as string;
                if (!string.IsNullOrEmpty(objTarget))
                    objTarget = string.Format("?trg={0}", objTarget);
            }

            MvcHtmlString result = MvcHtmlString.Create(string.Format("{0}{1}", lnk.ToString(), objTarget));
            return result;

        }

        public static MvcHtmlString TargetParam(this UrlHelper urlHelper, bool isConcat = false)
        {
            HttpRequestBase objRequest = urlHelper.RequestContext.HttpContext.Request;
            string objTarget = "";
            if (objRequest != null)
            {
                objTarget = objRequest.Params["trg"] as string;
                if (!string.IsNullOrEmpty(objTarget))
                    objTarget = string.Format("{0}trg={1}", isConcat ? "&" : "?", objTarget);
            }

            MvcHtmlString result = MvcHtmlString.Create(string.Format("{0}{1}", "", objTarget));
            return result;
        }

        public static string ToEmpty(this string value)
        {
            if (value == null) return string.Empty;
            return value;
        }



        public static string ToDate(this string value)
        {
            DateTime dt = new DateTime();
            if (DateTime.TryParse(value, out dt))
                return dt.ToDateString();

            return null;
        }

        public static DateTime GetFirtDatYear(this DateTime value)
        {
            return new DateTime(value.Year, 1, 1);

        }

        public static string ToDateString(this DateTime value)
        {
            return value.ToString("dd/MM/yyyy");
        }

        public static int ToInt32(this string value)
        {
            int result;
            if (int.TryParse(value, out result))
                return result;
            return 0;
        }

        public static decimal ToDecimal(this string value)
        {
            decimal result;
            if (decimal.TryParse(value, out result))
                return result;
            return 0;
        }

        public static string ToFormatDateDDMMYYY(this string value)
        {
            string dateString;
            if (value == null || value == string.Empty || value == "0")
                dateString = string.Empty;
            else
                dateString = value.Substring(6) + "/" + value.Substring(4, 2) + "/" + value.Substring(0, 4);

            return dateString;
        }


        public static string ToFormatDateYYYYMMDD(this string value)
        {
            string result = string.Join("", value.Split('/').Reverse());
            return result;
        }

        public static string HtmlIdNameFor<TModel, TValue>(
            this HtmlHelper<TModel> htmlHelper,
            System.Linq.Expressions.Expression<Func<TModel, TValue>> expression)
        {
            return (GetHtmlIdNameFor(expression));
        }

        private static string GetHtmlIdNameFor<TModel, TValue>(Expression<Func<TModel, TValue>> expression)
        {
            if (expression.Body.NodeType == ExpressionType.Call)
            {
                var methodCallExpression = (MethodCallExpression)expression.Body;
                string name = GetHtmlIdNameFor(methodCallExpression);
                return name.Substring(expression.Parameters[0].Name.Length + 1).Replace('.', '_');
            }
            string field = expression.Body.ToString();
            if (field.LastIndexOf('.') >= 0)
            {
                return field.Substring(field.LastIndexOf('.') + 1);
            }

            return expression.Body.ToString().Substring(expression.Parameters[0].Name.Length + 1).Replace('.', '_');
        }

        private static string GetHtmlIdNameFor(MethodCallExpression expression)
        {
            var methodCallExpression = expression.Object as MethodCallExpression;
            if (methodCallExpression != null)
            {
                return GetHtmlIdNameFor(methodCallExpression);
            }
            return expression.Object.ToString();
        }

        public static MvcHtmlString AjaxActionLink(this AjaxHelper ajaxHelper, string linkText, string actionName, string controllerName, object routeValues, AjaxOptions ajaxOptions, object htmlAttributes, string iconCss = "")
        {
            var repID = Guid.NewGuid().ToString();
            var lnk = ajaxHelper.ActionLink(repID, actionName, controllerName, routeValues, ajaxOptions, htmlAttributes);

            var builder = new TagBuilder("span");
            builder.MergeAttribute("class", iconCss);

            MvcHtmlString result = MvcHtmlString.Create(lnk.ToString().Replace(repID, builder.ToString() + linkText));
            return result;
        }

        public static string GetSubstring(this string value, int from, int to = 0)
        {
            if (string.IsNullOrEmpty(value))
                return value;
            if (to == 0)
                return value.Substring(from);
            return value.Substring(from, to);
        }

        public static byte[] FileToByteArray(HttpPostedFileBase File)
        {
            using (var reader = new BinaryReader(File.InputStream))
            {
                return reader.ReadBytes(File.ContentLength);
            }

        }


    }


    public class ViewUtil
    {
        public static string Target(string value)
        {
            string result = string.Empty;
            byte[] encryted = System.Text.Encoding.Unicode.GetBytes(value);
            result = Convert.ToBase64String(encryted);
            result = result.Replace("=", "eqxe");
            result = result.Replace("/", "slcex");
            return result;
        }

        public static string GetPerfilTarget(string value)
        {
            string result = string.Empty;
            if (string.IsNullOrEmpty(value))
                return string.Empty;
            try
            {
                value = value.Replace("eqxe", "=");
                value = value.Replace("slcex", "/");

                byte[] encryted = Convert.FromBase64String(value);
                result = System.Text.Encoding.Unicode.GetString(encryted);
                if (result.IndexOf('_') != -1)
                {
                    result = result.Substring(result.IndexOf('_') + 1);
                }
            }
            catch (Exception ex)
            {
                result = "";
            }
            return result;
        }
    }

    public enum TipoDatoCampoAdicional
    {
        Numerico = 1,
        Cadena = 2,
        Fecha = 3
    }
}