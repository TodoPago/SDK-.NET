﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Xml;
using System.IO;
using Newtonsoft.Json;
using TodoPagoConnector.Model;
using TodoPagoConnector.Exceptions;
using TodoPagoConnector.Utils;
using TodoPagoConnector.Operations;


namespace TodoPagoConnector
{
    public class TodoPago : RestConnector
    {
        private const string REQUESTTYPE = "RequestType";
        private const string AUTHORIZE = "Authorize";
        private const string CREDENTIALS = "Credentials";
        private const string OPERATIONS_GET_BY_RANGE_DATE_TIME = "Operations/GetByRangeDateTime";
        private const string PAYMENT_METHODS_GET = "PaymentMethods/Get";
        private const string OPERATIONS_GET_BY_OPERATION_ID = "Operations/GetByOperationId";

        public TodoPago(string endpoint, Dictionary<string, string> headders)
             : base(endpoint, headders)
        {
        }

        public List<Dictionary<string, object>> getByOperationID(string merchant, string operation)
        {
            string res = client.DownloadString(endpoint + OPERATIONS_GET_BY_OPERATION_ID + "/MERCHANT/" + merchant + "/OPERATIONID/" + operation);

            List<Dictionary<string, object>> returnList = new List<Dictionary<string, object>>();
            XmlDocument xd = new XmlDocument();

            try
            {
                xd.LoadXml(res);
                XmlNodeList nl = xd.GetElementsByTagName("OperationsColections");

                for (int i = 0; i < nl.Count; i++)
                {
                    Dictionary<string, object> masterDic = new Dictionary<string, object>();
                    XmlDocument xd2 = new XmlDocument();
                    xd2.LoadXml(nl[i].InnerXml);
                    XmlNodeList nl2 = xd2.GetElementsByTagName("Operations");
                    XmlNodeList nl3 = xd2.GetElementsByTagName("REFUND");
                    Dictionary<string, object> detailsDic = new Dictionary<string, object>();

                    for (int j = 0; j < nl2.Count; j++)
                    {
                        detailsDic = new Dictionary<string, object>();
                        XmlNode aux = nl2[i].FirstChild;
                        while (aux != null)
                        {
                            if (!"REFUNDS".Equals(aux.Name))
                            {
                                string a = aux.InnerText;
                                string b = aux.Name;
                                //Console.WriteLine("- " + b + " : " + a);
                                detailsDic.Add(b, a);
                            }
                            aux = aux.NextSibling;
                        }
                        masterDic.Add("Operations", detailsDic);
                    }

                    Dictionary<string, object> detailsDic2 = new Dictionary<string, object>();
                    for (int k = 0; k < nl3.Count; k++)
                    {
                        Dictionary<string, object> detailsDic3 = new Dictionary<string, object>();
                        XmlNode aux2 = nl3[i].FirstChild;

                        while (aux2 != null)
                        {
                            string a2 = aux2.InnerText;
                            string b2 = aux2.Name;

                            detailsDic3.Add(b2, a2);
                            aux2 = aux2.NextSibling;
                        }
                        detailsDic2.Add("REFUND" + k, detailsDic3);
                    }
                    detailsDic.Add("REFUNDS", detailsDic2);
                    returnList.Add(masterDic);
                }
            }
            catch (System.Xml.XmlException)
            {
                throw new ResponseException(res);
            }

            return returnList;
        }
        
        public Dictionary<string, object> GetAllPaymentMethods(string merchant)
        {
            string res = client.DownloadString(endpoint + PAYMENT_METHODS_GET + "/MERCHANT/" + merchant);
            //System.Console.WriteLine(res);
            XmlDocument xd = new XmlDocument();
            try
            {
                xd.LoadXml(res);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return toDictionary(xd);
        }

        public Dictionary<string, object> GetByRangeDateTime(Dictionary<string, string> param)
        {
            string url = endpoint + OPERATIONS_GET_BY_RANGE_DATE_TIME;

            if (param.ContainsKey(ElementNames.MERCHANT))
            {
                string merchant = param[ElementNames.MERCHANT];
                url = url + "/MERCHANT/" + merchant;
            }
            if (param.ContainsKey(ElementNames.STARTDATE))
            {
                string startDate = param[ElementNames.STARTDATE];
                url = url + "/STARTDATE/" + startDate;
            }
            if (param.ContainsKey(ElementNames.ENDDATE))
            {
                string endDate = param[ElementNames.ENDDATE];
                url = url + "/ENDDATE/" + endDate;
            }
            if (param.ContainsKey(ElementNames.PAGENUMBER))
            {
                string pageNumber = param[ElementNames.PAGENUMBER];
                url = url + "/PAGENUMBER/" + pageNumber;
            }

            XmlDocument xd = new XmlDocument();

            try
            {
                string res = client.DownloadString(url);
                //System.Console.WriteLine(res);
                xd.LoadXml(res);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return toDictionaryGetByRange(xd);
        }
        
        public Dictionary<string, object> VoidRequest(Dictionary<string, string> param)
        {
            string URL = endpoint + AUTHORIZE;
            string result;
            param.Add(REQUESTTYPE, "VoidRequest");

            var httpWebRequest = generateHttpWebRequest(URL, CONTENT_TYPE_APP_JSON, METHOD_POST, true);

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = JsonConvert.SerializeObject(param, Newtonsoft.Json.Formatting.Indented);
                streamWriter.Write(json);
                //System.Console.WriteLine(json);
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                result = streamReader.ReadToEnd();
                //System.Console.WriteLine(result);
            }
            return OperationsParser.parseJsonToVoidRequest(result);
        }
        
        public Dictionary<string, object> ReturnRequest(Dictionary<string, string> param)
        {
            string URL = endpoint + AUTHORIZE;
            string result;
            param.Add(REQUESTTYPE, "ReturnRequest");

            var httpWebRequest = generateHttpWebRequest(URL, CONTENT_TYPE_APP_JSON, METHOD_POST, true);

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = JsonConvert.SerializeObject(param, Newtonsoft.Json.Formatting.Indented);
                streamWriter.Write(json);
                //System.Console.WriteLine(json);
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                result = streamReader.ReadToEnd();
                //System.Console.WriteLine(result);
            }
            return OperationsParser.parseJsonToReturnRequest(result);
        }
        
        public User getCredentials(User user)
        {
            string URL = endpoint + CREDENTIALS;
            URL = URL.Replace("t/1.1/", "");
            User userResponse = new User();
            string result;

            var httpWebRequest = generateHttpWebRequest(URL, CONTENT_TYPE_APP_JSON, METHOD_POST, false);

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = JsonConvert.SerializeObject(user.toDictionary(), Newtonsoft.Json.Formatting.Indented);
                streamWriter.Write(json);
                //System.Console.WriteLine(json);
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                result = streamReader.ReadToEnd();
                //System.Console.WriteLine(result);
            }
            userResponse = OperationsParser.parseJsonToUser(result);
            return userResponse;
        }
        
        protected Dictionary<string, object> toDictionaryGetByRange(XmlDocument xd)
        {
            Dictionary<string, object> rv = new Dictionary<string, object>();
            Dictionary<string, object> ret = new Dictionary<string, object>();

            foreach (XmlNode child in xd.ChildNodes)
            {
                rv = toDictionaryGetByRange(child);

                if (!rv.ContainsKey(" "))
                {
                    foreach (string k in rv.Keys)
                    {
                        //Failsafe, exception thrown on adding a key that already exists
                        if (!ret.ContainsKey(k))
                        {
                            ret.Add(k, rv[k]);
                        }
                    }
                }
                else
                {
                    ret.Add(child.Name, rv[" "]);
                }
            }

            return ret;
        }

        protected Dictionary<string, object> toDictionaryGetByRange(XmlNode xd)
        {
            Dictionary<string, object> ret = new Dictionary<string, object>();
            Dictionary<string, object> aux = new Dictionary<string, object>();
            string keyVal = " ";
            int index = 0;

            foreach (XmlNode child in xd.ChildNodes)
            {
                if (child.HasChildNodes && child.FirstChild.HasChildNodes)
                {
                    Dictionary<string, object> rv = toDictionaryGetByRange(child);
                    if (!rv.ContainsKey(" "))
                    {
                        foreach (string k in rv.Keys)
                        {
                            //Failsafe, exception thrown on adding a key that already exists
                            if (!ret.ContainsKey(k))
                            {
                                ret.Add(k, rv[k]);
                            }
                        }
                    }
                    else
                    {
                        index++;
                        ret.Add(child.Name + " - " + index.ToString(), rv[" "]);
                    }
                }
                else
                {
                    if (child.Name.Contains("Id"))
                    {
                        keyVal = child.InnerText;
                    }
                    ret.Add(child.Name, child.InnerText);
                }
            }

            aux.Add(keyVal, ret);

            return aux;
        }
    }
}
