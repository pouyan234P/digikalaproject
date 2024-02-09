using digika_mobileapp.Models;
using digika_mobileapp.Services.IServices;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace digika_mobileapp.Services
{
    public class BaseService : IBaseService
    {
        public ResponseDTO responseDTO { get; set; }
        public IHttpClientFactory httpClient { get; set; }
        public BaseService(IHttpClientFactory httpClient)
        {
            this.responseDTO = new ResponseDTO();
            this.httpClient = httpClient;
        }

        public async Task<T> SendAsync<T>(ApiRequest apiRequest)
        {
            try
            {
                var client = httpClient.CreateClient("MangoApi");
                HttpRequestMessage message = new();
                message.Headers.Add("Accept", "application/json");
                message.RequestUri = new Uri(apiRequest.Url);
                client.DefaultRequestHeaders.Clear();
                if (apiRequest.Data == null)
                {
                    if (apiRequest.ApiType == SD.ApiType.POST || apiRequest.ApiType == SD.ApiType.PUT)
                    {
                        // Check if the request has a file to upload
                        if (apiRequest.file != null)
                        {
                            var formData = new MultipartFormDataContent();
                            formData.Add(new StringContent(JsonConvert.SerializeObject(apiRequest.Data)), "jsonData");

                            var fileContent = new StreamContent(apiRequest.file.OpenReadStream());
                            fileContent.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("form-data")
                            {
                                Name = "mainpicture",
                                FileName = apiRequest.file.FileName
                            };

                            formData.Add(fileContent, "mainpicture", apiRequest.file.FileName);
                            message.Content = formData;
                        }
                        if (apiRequest.Ifile != null)
                        {
                            var formData = new MultipartFormDataContent();
                            foreach (var file in apiRequest.Ifile)
                            {
                                var fileContent1 = new StreamContent(file.OpenReadStream());
                                fileContent1.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("form-data")
                                {
                                    Name = "mypicture", // Use a different name for each file in the list
                                    FileName = file.FileName
                                };

                                formData.Add(fileContent1, "mypicture", file.FileName);
                                message.Content = formData;
                            }
                        }
                            
                            
                        }
                    }
                
                else
                {
                    // Convert other data types to StringContent
                    var stringContent = new StringContent(JsonConvert.SerializeObject(apiRequest.Data), Encoding.UTF8, "application/json");
                    message.Content = stringContent;
                }
                if(!string.IsNullOrEmpty(apiRequest.AccessToken))
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiRequest.AccessToken);
                }
                HttpResponseMessage apiresponse = null;
                switch(apiRequest.ApiType)
                {
                    case SD.ApiType.POST:
                        message.Method = HttpMethod.Post;
                        break;
                    case SD.ApiType.PUT:
                        message.Method = HttpMethod.Put;
                        break;
                    case SD.ApiType.DELETE:
                        message.Method = HttpMethod.Delete;
                        break;
                    default:
                        message.Method = HttpMethod.Get;
                        break;
                }
                apiresponse = await client.SendAsync(message);
                var apicontent = await apiresponse.Content.ReadAsStringAsync();
                var apiresponsedto = JsonConvert.DeserializeObject<T>(apicontent);
                /*var myitem;
                foreach(var item in apiresponse.Headers)
                {
                  myitem=item.  
                }*/
                return apiresponsedto;
            }
            catch(Exception e)
            {
                var dto = new ResponseDTO
                {
                    DisplayMessage = "ERROR",
                    ErrorMessages = new List<string> { Convert.ToString(e.Message) },
                    IsSuccess = false
                };
                var res = JsonConvert.SerializeObject(dto);
                var apiresponsedto = JsonConvert.DeserializeObject<T>(res);
                return apiresponsedto;
            }
        }
        public void Dispose()
        {
            GC.SuppressFinalize(true);
        }
    }
}
