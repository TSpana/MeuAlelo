using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MeuAlelo.Source.Alelo
{
    public class Alelo
    {
        const string urlhome = "http://www.meualelo.com.br/";
        const string urlSaldo = "https://www.meualelo.com.br/SaldoExtratoValidacaoServlet";
        const string urlCaptcha = "https://www.meualelo.com.br/inst/images/captcha.jpg";
        IEnumerable<string> cookies;
        public Alelo()
        {
            SetDefault();
            Current = this;
        }

        private void SetDefault()
        {
            HttpClientHandler handler = new HttpClientHandler()
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            };

            HttpClient = new System.Net.Http.HttpClient(handler);

            HttpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("text/html"));
            HttpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/xhtml+xml"));
            HttpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/xml"));
            HttpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("image/webp"));
            HttpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("*/*"));

            HttpClient.DefaultRequestHeaders.AcceptEncoding.Add(new System.Net.Http.Headers.StringWithQualityHeaderValue("gzip"));
            HttpClient.DefaultRequestHeaders.AcceptEncoding.Add(new System.Net.Http.Headers.StringWithQualityHeaderValue("deflate"));
            HttpClient.DefaultRequestHeaders.AcceptEncoding.Add(new System.Net.Http.Headers.StringWithQualityHeaderValue("sdch"));
            HttpClient.DefaultRequestHeaders.AcceptEncoding.Add(new System.Net.Http.Headers.StringWithQualityHeaderValue("br"));

            HttpClient.DefaultRequestHeaders.Connection.Add("keep-alive");
            HttpClient.DefaultRequestHeaders.Add("DNT", "1");

            HttpClient.DefaultRequestHeaders.Host = "www.meualelo.com.br";

            HttpClient.DefaultRequestHeaders.UserAgent.Add(new System.Net.Http.Headers.ProductInfoHeaderValue("Mozilla", "5.0"));
            HttpClient.DefaultRequestHeaders.UserAgent.Add(new System.Net.Http.Headers.ProductInfoHeaderValue("AppleWebKit", "537.36"));
            HttpClient.DefaultRequestHeaders.UserAgent.Add(new System.Net.Http.Headers.ProductInfoHeaderValue("Chrome", "55.0.2883.87"));
            HttpClient.DefaultRequestHeaders.UserAgent.Add(new System.Net.Http.Headers.ProductInfoHeaderValue("Safari", "537.36"));// /537.36 (KHTML, like Gecko) / /537.36"));

        }

        private static Alelo current;
        public static Alelo Current
        {
            get
            {
                if (current == null)
                    new Alelo();
                return current;
            }
            private set { current = value; }
        }
        public async Task<Image> NewCaptcha()
        {
            return await GetCaptcha();
        }
        public System.Net.Http.HttpClient HttpClient { get; set; }
        public async Task<AleloDados> LoadCartao(string cartao, string capta)
        {
            try
            {
                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic["txtCartao1"] = cartao;
                dic["captcha"] = capta;
                System.Net.Http.FormUrlEncodedContent form = new System.Net.Http.FormUrlEncodedContent(dic);

                HttpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("text/html"));
                HttpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/xhtml+xml"));
                HttpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/xml"));
                HttpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("image/webp"));
                HttpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("*/*"));

                HttpClient.DefaultRequestHeaders.AcceptEncoding.Add(new System.Net.Http.Headers.StringWithQualityHeaderValue("gzip"));
                HttpClient.DefaultRequestHeaders.AcceptEncoding.Add(new System.Net.Http.Headers.StringWithQualityHeaderValue("deflate"));
                HttpClient.DefaultRequestHeaders.AcceptEncoding.Add(new System.Net.Http.Headers.StringWithQualityHeaderValue("br"));

                HttpClient.DefaultRequestHeaders.Connection.Add("keep-alive");
                HttpClient.DefaultRequestHeaders.Add("DNT", "1");


                HttpClient.DefaultRequestHeaders.Referrer = new Uri("https://www.meualelo.com.br/");
                HttpClient.DefaultRequestHeaders.Add("Origin", "https://www.meualelo.com.br");
                HttpClient.DefaultRequestHeaders.Add("Cookie", string.Join(";", cookies));
                HttpClient.DefaultRequestHeaders.Host = "www.meualelo.com.br";
                HttpClient.DefaultRequestHeaders.Add("Upgrade-Insecure-Requests", "1");
                var content = await HttpClient.PostAsync(urlSaldo, form);

                content.EnsureSuccessStatusCode();

                HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                var teste = await content.Content.ReadAsStreamAsync();

                var str = "";

                using (System.IO.StreamReader sr = new System.IO.StreamReader(teste))
                {
                    while (!sr.EndOfStream)
                    {
                        str += sr.ReadLine();
                    }
                }
                doc.LoadHtml(str);
                var node = doc.DocumentNode;


                var iframe = node.Descendants().Where(x => x.Id == "conteudo").ToArray();
                AleloDados alelo = new AleloDados();
                if (iframe.Count() == 0)
                    throw new Exception();

                alelo.CartaoNumero = cartao;
                var m = System.Text.RegularExpressions.Regex.Matches(iframe.First().Attributes["src"].Value, "(ticket)=([^&]+)");

                alelo.Ticket = m[0].Groups[2].Value;

                return alelo;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<AleloExtrato> LoadSaldo(AleloDados dados)
        {
            try
            {
                HttpClient.DefaultRequestHeaders.Host = "www.cartoesbeneficio.com.br";


                var content = await HttpClient.PostAsync($"http://www.cartoesbeneficio.com.br/inst/convivencia/SaldoExtratoAlelo.jsp?ticket={dados.Ticket}&primeiroAcesso=S&origem=Alelo", null);

                HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                doc.LoadHtml(await content.Content.ReadAsStringAsync());
                var nodes = doc.DocumentNode;

                if (nodes.InnerHtml.Contains("O serviço para consulta de saldo e extrato dos cartões Alelo"))
                {
                    var tables = nodes.Descendants().Where(x => x.Name == "table" && x.Attributes["class"] != null && x.Attributes["class"].Value == "alelo").ToArray();

                    var message = tables[0].InnerText.Replace("\t", "");
                    throw new AleloExpiratedException(message);
                }

                AleloExtrato extrato = new AleloExtrato();



                var divsLightGrey = nodes.Descendants().Where(x => x.Name == "div" && x.Attributes["class"] != null && x.Attributes["class"].Value == "button light-grey").ToArray();

                extrato.GastoMedio = divsLightGrey[0].Descendants().Where(x => x.Name == "strong").ToArray().First().InnerText;
                var recarga = divsLightGrey[1].Descendants().Where(x => x.Name == "strong").ToArray();
                extrato.DataProximaRecarga = recarga[0].InnerText;
                extrato.ValorProximaRecarga = recarga[1].InnerText;

                var divSaldo = nodes.Descendants().Where(x => x.Name == "div" && x.Attributes["class"] != null && x.Attributes["class"].Value == "button bt-saldo bpa").ToArray();
                extrato.Saldo = divSaldo[0].Descendants().Where(x => x.Name == "strong").ToArray().First().InnerText;

                var table = nodes.Descendants().Where(x => x.Name == "table" && x.Descendants().ToList().Exists(y => y.Name == "td"));

                var trs = table.First().Descendants().Where(x => x.Name == "tr").ToArray();


                foreach (var item in trs)
                {
                    string data = "";
                    string descricao = "";
                    string valor = "";

                    var tds = item.Descendants().Where(x => x.Name == "td").ToArray();
                    data = tds[0].InnerText;
                    descricao = tds[1].InnerText;
                    valor = tds[2].InnerText.Replace("&nbsp;"," ");

                    extrato.Add(new ExtratoDados(data, descricao, valor));
                }







                return extrato;

            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public async Task<Image> GetCaptcha()
        {
            try
            {
                SetDefault();
                var teste = await HttpClient.GetAsync(urlCaptcha);
                cookies = teste.Headers.GetValues("Set-Cookie");
                var content = await HttpClient.GetStreamAsync(urlCaptcha);
                return Bitmap.FromStream(content);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
