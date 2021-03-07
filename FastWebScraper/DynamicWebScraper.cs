using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace TestProject
{
    public class DynamicWebScraper
    {
        private HttpClient client = new HttpClient();
        public List<User> GetUsers(string url)
        {
            List<User> users = new List<User>();

            var index = 0;
            bool nextNext;
            do
            {
                var res = client.GetAsync(url + "/test?index=" + index).Result.Content.ReadAsStringAsync().Result;
                var resp = JsonConvert.DeserializeObject<UserResponse>(res);
                users.AddRange(resp.Users);

                index++;
                nextNext = resp.HasNext;
            } while (nextNext);

            return users;
        }
    }

    public class UserResponse
    {
        public int Index { get; set; }
        public List<User> Users { get; set; }
        public bool HasPrevious { get; set; }
        public bool HasNext { get; set; }
    }
}