using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using Newtonsoft.Json;

namespace CRUDAjax.UI.Models.Api
{
    public class Translate
    {
        public string code { get; set; }

        public string lang { get; set; }

        public string[] text { get; set; }


        public string Traduzir(string texto)
        {
            try
            {
                Translate trl = new Translate();

                string url = "https://translate.yandex.net/api/v1.5/tr.json/translate";
                url += "?key=trnsl.1.1.20181030T035109Z.896423cf7613c2b0.76e0bdc8f9c7288640723e40bb963d0821beb52d";
                url += "&text=" + texto;
                url += "&lang=pt-en";

                WebClient wc = new WebClient();
                wc.Encoding = Encoding.UTF8;
                string strJson = wc.DownloadString(url);

                trl = descerializarResultado(strJson);

                return trl.text[0];
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                throw;
            }
        }


        public Translate descerializarResultado(string resultado)
        {
            Translate trl = new Translate();
            trl = JsonConvert.DeserializeObject<Translate>(resultado);

            return trl;
        }
    }
}