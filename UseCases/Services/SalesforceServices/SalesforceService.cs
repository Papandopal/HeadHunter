using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Domain.Entities;
using Domain.Enums;
using Microsoft.Extensions.Configuration;
using UseCases.Services.AuthServices.Interfaces;
using UseCases.Services.CandidateServices.Interfaces;
using UseCases.Services.RecruterServices.Interfaces;
using UseCases.Services.SalesforceServices.DTOs.Candidate;
using UseCases.Services.SalesforceServices.DTOs.Recruter;
using UseCases.Services.SalesforceServices.Interfaces;

namespace UseCases.Services.SalesforceServices
{
    public class SalesforceService : ISalesforceService
    {
        private readonly IConfiguration configuration;
        private readonly IAuthService authService;
        private readonly ICandidateService candidateService;
        private readonly IRecruterService recruterService;
        public SalesforceService(IAuthService authService, IConfiguration configuration, ICandidateService candidateService,
            IRecruterService recruterService)
        {
            this.authService = authService;
            this.configuration = configuration;
            this.candidateService = candidateService;
            this.recruterService = recruterService;
            ((ISalesforceService)this).LoginAsync();
        }
        async Task ISalesforceService.CreateCandidateAccountAsync(AddCandidateAccountDTO addRecordsDTO)
        {
            var requestBody = CreateCandidateAccountRequestBody(addRecordsDTO);
            var jsonResponse = await QueryPostAsync(requestBody, "composite");

            using var doc = JsonDocument.Parse(jsonResponse);
            var records = doc.RootElement.GetProperty("compositeResponse");

            foreach (var record in records.EnumerateArray())
            {
                string refId = record.GetProperty("referenceId").GetString();
                int status = record.GetProperty("httpStatusCode").GetInt32();

                if (!new int[] { (int)HttpStatusCode.OK, (int)HttpStatusCode.Created, (int)HttpStatusCode.NoContent }.Contains(status))
                {
                    var body = record.GetProperty("body");
                    if (body.TryGetProperty("errors", out var errorsArray))
                    {
                        foreach (var error in errorsArray.EnumerateArray())
                        {
                            string code = error.GetProperty("errorCode").GetString();
                            string msg = error.GetProperty("message").GetString();
                            throw new Exception($"Code: {code}\n" +
                                $"Message: {msg}");
                        }
                    }
                    else throw new Exception($"Code: No code\n" +
                        $"Message: {body.ToString()}");
                }
            }
        }

        async Task ISalesforceService.CreateRecruterAccountAsync(AddRecruterAccountDTO addRecordsDTO)
        {
            var requestBody = CreateRecruterAccountRequestBody(addRecordsDTO);
            var jsonResponse = await QueryPostAsync(requestBody, "composite");

            using var doc = JsonDocument.Parse(jsonResponse);
            var records = doc.RootElement.GetProperty("compositeResponse");

            foreach (var record in records.EnumerateArray())
            {
                string refId = record.GetProperty("referenceId").GetString();
                int status = record.GetProperty("httpStatusCode").GetInt32();

                if (!new int[] { (int)HttpStatusCode.OK, (int)HttpStatusCode.Created, (int)HttpStatusCode.NoContent }.Contains(status))
                {
                    var body = record.GetProperty("body");
                    if (body.TryGetProperty("errors", out var errorsArray))
                    {
                        foreach (var error in errorsArray.EnumerateArray())
                        {
                            string code = error.GetProperty("errorCode").GetString();
                            string msg = error.GetProperty("message").GetString();
                            throw new Exception($"Code: {code}\n" +
                                $"Message: {msg}");
                        }
                    }
                    else throw new Exception($"Code: No code\n" +
                        $"Message: {body.ToString()}");
                }
            }
        }

        async Task ISalesforceService.EditCandidateAccountAsync(EditCandidateAccountDTO editRecordsDTO)
        {
            var requestBody = EditCandidateAccountRequestBody(editRecordsDTO);
            var jsonResponse = await QueryPostAsync(requestBody, "composite");

            using var doc = JsonDocument.Parse(jsonResponse);
            var records = doc.RootElement.GetProperty("compositeResponse");

            foreach (var record in records.EnumerateArray())
            {
                string refId = record.GetProperty("referenceId").GetString();
                int status = record.GetProperty("httpStatusCode").GetInt32();
                var goodStatuses = new int[] { (int)HttpStatusCode.OK, (int)HttpStatusCode.Created, (int)HttpStatusCode.NoContent };
                if (!goodStatuses.Contains(status))
                {
                    var body = record.GetProperty("body");
                    if (body.TryGetProperty("errors", out var errorsArray))
                    {
                        foreach (var error in errorsArray.EnumerateArray())
                        {
                            string code = error.GetProperty("errorCode").GetString();
                            string msg = error.GetProperty("message").GetString();
                            throw new Exception($"Code: {code}\n" +
                                $"Message: {msg}");
                        }
                    }
                    else throw new Exception($"Code: No code\n" +
                        $"Message: {body.ToString()}");
                }
            }
        }

        async Task ISalesforceService.EditRecruterAccountAsync(EditRecruterAccountDTO editRecordsDTO)
        {
            var requestBody = EditRecruterAccountRequestBody(editRecordsDTO);
            var jsonResponse = await QueryPostAsync(requestBody, "composite");

            using var doc = JsonDocument.Parse(jsonResponse);
            var records = doc.RootElement.GetProperty("compositeResponse");

            foreach (var record in records.EnumerateArray())
            {
                string refId = record.GetProperty("referenceId").GetString();
                int status = record.GetProperty("httpStatusCode").GetInt32();
                var goodStatuses = new int[] { (int)HttpStatusCode.OK, (int)HttpStatusCode.Created, (int)HttpStatusCode.NoContent };
                if (!goodStatuses.Contains(status))
                {
                    var body = record.GetProperty("body");
                    if (body.TryGetProperty("errors", out var errorsArray))
                    {
                        foreach (var error in errorsArray.EnumerateArray())
                        {
                            string code = error.GetProperty("errorCode").GetString();
                            string msg = error.GetProperty("message").GetString();
                            throw new Exception($"Code: {code}\n" +
                                $"Message: {msg}");
                        }
                    }
                    else throw new Exception($"Code: No code\n" +
                        $"Message: {body.ToString()}");
                }
            }
        }

        async Task ISalesforceService.LoginAsync()
        {
            var jsonResponse = string.Empty;
            using (var client = new HttpClient())
            {
                var request = new FormUrlEncodedContent(new Dictionary<string, string> {
                        { "grant_type", "client_credentials" },
                        { "client_id", configuration["Salesforce:ClientId"] },
                        { "client_secret", configuration["Salesforce:ClientSecret"] }
                    });

                var response = await client.PostAsync(configuration["Salesforce:LoginEndpoint"], request);
                jsonResponse = await response.Content.ReadAsStringAsync();
                var values = JsonSerializer.Deserialize<Dictionary<string, string>>(jsonResponse);
                configuration["Salesforce:AuthToken"] = values["access_token"];
                configuration["Salesforce:InstanceURL"] = values["instance_url"];
            }
        }

        async Task<CandidateAccountDTO?> ISalesforceService.TryGetCandidateAccountAsync()
        {
            var user = authService.User();
            string jsonResponse = await QueryGetAsync($"SELECT Id, Account.Id, Account.Gender__c, Phone " +
                $"FROM Contact " +
                $"WHERE Email = '{user.Email}'");
            var doc = JsonDocument.Parse(jsonResponse);
            if (doc.RootElement.GetProperty("totalSize").GetInt32() == 0) return null;
            var contactRecord = doc.RootElement.GetProperty("records").EnumerateArray().First();
            var accountRecord = contactRecord.GetProperty("Account");
            return new CandidateAccountDTO
            {
                AccountId = accountRecord.GetProperty("Id").GetString(),
                ContactId = contactRecord.GetProperty("Id").GetString(),
                Gender = Enum.Parse<Gender>(accountRecord.GetProperty("Gender__c").GetString()),
                Phone = contactRecord.GetProperty("Phone").GetString()
            };
        }

        async Task<RecruterAccountDTO?> ISalesforceService.TryGetRecruterAccountAsync()
        {
            var user = authService.User();
            string jsonResponse = await QueryGetAsync($"SELECT Id, Account.Id, Account.Gender__c, Account.Birthday__c, Phone " +
                $"FROM Contact " +
                $"WHERE Email = '{user.Email}'");
            var doc = JsonDocument.Parse(jsonResponse);
            if (doc.RootElement.GetProperty("totalSize").GetInt32() == 0) return null;
            var contactRecord = doc.RootElement.GetProperty("records").EnumerateArray().First();
            var accountRecord = contactRecord.GetProperty("Account");
            return new RecruterAccountDTO
            {
                AccountId = accountRecord.GetProperty("Id").GetString(),
                ContactId = contactRecord.GetProperty("Id").GetString(),
                Gender = Enum.Parse<Gender>(accountRecord.GetProperty("Gender__c").GetString()),
                Birthday = DateOnly.Parse(accountRecord.GetProperty("BirthDay__c").GetString()),
                Phone = contactRecord.GetProperty("Phone").GetString()
            };
        }

        private async Task<string> GetResponseAsync(HttpRequestMessage request, HttpClient client)
        {
            var response = await client.SendAsync(request);
            return await response.Content.ReadAsStringAsync();
        }
        private async Task<string> QueryGetAsync(string soqlQuery)
        {
            var result = string.Empty;
            using (var client = new HttpClient())
            {
                string restRequest = $"{configuration["Salesforce:InstanceURL"]}/{configuration["Salesforce:ServiceURL"]}/query?q={soqlQuery}";
                using (var request = GetNewHttpGetRequest(restRequest))
                {
                    result = await GetResponseAsync(request, client);
                }
            }
            return result;
        }

        private async Task<string> QueryPostAsync(string requestBody, string requestService)
        {
            var result = string.Empty;
            using (var client = new HttpClient())
            {
                string restRequest = $"{configuration["Salesforce:InstanceURL"]}/{configuration["Salesforce:ServiceURL"]}/{requestService}";
                using (var request = GetNewHttpPostRequest(restRequest, requestBody))
                {
                    result = await GetResponseAsync(request, client);
                }
            }
            return result;
        }

        private HttpRequestMessage GetNewHttpGetRequest(string url)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Add("Authorization", $"Bearer {configuration["Salesforce:AuthToken"]}");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return request;
        }

        private HttpRequestMessage GetNewHttpPostRequest(string url, string? body)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Headers.Add("Authorization", $"Bearer {configuration["Salesforce:AuthToken"]}");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Content = new StringContent(body ?? string.Empty, Encoding.UTF8, "application/json");
            return request;
        }

        private string NormilazePhone(string phone)
        {
            return phone.StartsWith('+') ? phone : "+" + phone;
        }

        private string CreateCandidateAccountRequestBody(AddCandidateAccountDTO createRecordsDTO)
        {
            var user = authService.User();
            var candidate = candidateService.GetItemOrDefaultByOwnerId(user.Id);

            return $$"""
                 {
                  "allOrNone": true,
                  "compositeRequest": [
                    {
                      "method": "POST",
                      "url": "/{{configuration["Salesforce:ServiceURL"]}}/sobjects/Account",
                      "referenceId": "NewAccount",
                      "body": {
                        "Name": "{{user.Email}}",
                        "BirthDay__c": "{{candidate.Birthday.ToString("yyyy-MM-dd")}}",
                        "Gender__c": "{{createRecordsDTO.Gender}}"
                      }
                    },
                    {
                      "method": "POST",
                      "url": "/{{configuration["Salesforce:ServiceURL"]}}/sobjects/Contact",
                      "referenceId": "NewContact",
                      "body": {
                        "LastName": "{{candidate.LastName}}",
                        "FirstName": "{{candidate.FirstName}}",
                        "Email": "{{user.Email}}",
                        "Phone": "{{NormilazePhone(createRecordsDTO.Phone)}}",
                        "AccountId": "@{NewAccount.id}"
                      }
                    }
                  ]
                }
                """;
        }

        private string CreateRecruterAccountRequestBody(AddRecruterAccountDTO createRecordsDTO)
        {
            var user = authService.User();
            var recruter = recruterService.GetItemOrDefaultByOwnerId(user.Id);
            return $$"""
                 {
                  "allOrNone": true,
                  "compositeRequest": [
                    {
                      "method": "POST",
                      "url": "/{{configuration["Salesforce:ServiceURL"]}}/sobjects/Account",
                      "referenceId": "NewAccount",
                      "body": {
                        "Name": "{{user.Email}}",
                        "BirthDay__c": "{{createRecordsDTO.Birthday.ToString("yyyy-MM-dd")}}",
                        "Gender__c": "{{createRecordsDTO.Gender}}"
                      }
                    },
                    {
                      "method": "POST",
                      "url": "/{{configuration["Salesforce:ServiceURL"]}}/sobjects/Contact",
                      "referenceId": "NewContact",
                      "body": {
                        "LastName": "{{recruter.LastName}}",
                        "FirstName": "{{recruter.FirstName}}",
                        "Email": "{{user.Email}}",
                        "Phone": "{{NormilazePhone(createRecordsDTO.Phone)}}",
                        "AccountId": "@{NewAccount.id}"
                      }
                    }
                  ]
                }
                """;
        }

        private string EditCandidateAccountRequestBody(EditCandidateAccountDTO editRecordsDTO)
        {
            var user = authService.User();
            var candidate = candidateService.GetItemOrDefaultByOwnerId(user.Id);

            return $$"""
                 {
                  "allOrNone": true,
                  "compositeRequest": [
                    {
                      "method": "PATCH",
                      "url": "/{{configuration["Salesforce:ServiceURL"]}}/sobjects/Account/{{editRecordsDTO.AccountId}}",
                      "referenceId": "UpdatedAccount",
                      "body": {
                        "Name": "{{user.Email}}",
                        "BirthDay__c": "{{candidate.Birthday.ToString("yyyy-MM-dd")}}",
                        "Gender__c": "{{editRecordsDTO.Gender}}"
                      }
                    },
                    {
                      "method": "PATCH",
                      "url": "/{{configuration["Salesforce:ServiceURL"]}}/sobjects/Contact/{{editRecordsDTO.ContactId}}",
                      "referenceId": "UpdatedContact",
                      "body": {
                        "LastName": "{{candidate.LastName}}",
                        "FirstName": "{{candidate.FirstName}}",
                        "Email": "{{user.Email}}",
                        "Phone": "{{NormilazePhone(editRecordsDTO.Phone)}}",
                        "AccountId": "{{editRecordsDTO.AccountId}}"
                      }
                    }
                  ]
                }
                """;
        }

        private string EditRecruterAccountRequestBody(EditRecruterAccountDTO editRecordsDTO)
        {
            var user = authService.User();
            var recruter = recruterService.GetItemOrDefaultByOwnerId(user.Id);

            return $$"""
                 {
                  "allOrNone": true,
                  "compositeRequest": [
                    {
                      "method": "PATCH",
                      "url": "/{{configuration["Salesforce:ServiceURL"]}}/sobjects/Account/{{editRecordsDTO.AccountId}}",
                      "referenceId": "UpdatedAccount",
                      "body": {
                        "Name": "{{user.Email}}",
                        "BirthDay__c": "{{editRecordsDTO.Birthday.ToString("yyyy-MM-dd")}}",
                        "Gender__c": "{{editRecordsDTO.Gender}}"
                      }
                    },
                    {
                      "method": "PATCH",
                      "url": "/{{configuration["Salesforce:ServiceURL"]}}/sobjects/Contact/{{editRecordsDTO.ContactId}}",
                      "referenceId": "UpdatedContact",
                      "body": {
                        "LastName": "{{recruter.LastName}}",
                        "FirstName": "{{recruter.FirstName}}",
                        "Email": "{{user.Email}}",
                        "Phone": "{{NormilazePhone(editRecordsDTO.Phone)}}",
                        "AccountId": "{{editRecordsDTO.AccountId}}"
                      }
                    }
                  ]
                }
                """;
        }
    }
}
