using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using STTimer.ApiObjects;

namespace STTimer
{
    class ApiWrapper
    {
        private static ApiWrapper instance;
        private Token token;
        RestClient client;
        private ApiWrapper()
        {
            client = new RestClient("http://localhost:8182/");
        }

        public static ApiWrapper Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ApiWrapper();
                }
                return instance;
            }
        }

        public bool Login(string username, string password)
        {
            RestRequest req = new RestRequest("auth/login", Method.POST);
            req.AddJsonBody(new LoginData(username, password));
            req.RootElement = "";
            var resp = client.Execute<Token>(req);
            if (resp.StatusCode != System.Net.HttpStatusCode.OK)
            {
                return false;
            }
            Token token = resp.Data;
            this.token = token;
            client.AddDefaultHeader("X-Token", token.token);
            return true;
        }

        public bool isLoggedIn()
        {
            return this.token != null;
        }

        public List<STTimer.ApiObjects.Task> getTasks()
        {
            if (!isLoggedIn())
            {
                throw new Exception("User needs to log in");
            }

            RestRequest req = new RestRequest("task", Method.GET);
            List<STTimer.ApiObjects.Task> tasks;
            tasks = client.Execute<List<STTimer.ApiObjects.Task>>(req).Data;
            return tasks;
        }

        public User getUser()
        {
            if (!isLoggedIn())
            {
                throw new Exception("User needs to log in");
            }
            return token.user;
        }

        public void saveTaskEffort(STTimer.ApiObjects.Task task, double hours) {
            EffortData effortData = new EffortData(hours);
            RestRequest req = new RestRequest("task/{id}/effort", Method.POST);
            req.AddUrlSegment("id", task.id.ToString());
            req.AddJsonBody(effortData);
            RestResponse resp = (RestResponse) client.Execute(req);
            Console.WriteLine(resp.StatusCode);
            Console.WriteLine(resp.Content);
            Switcher.Switch(new STTimer.Windows.TaskListWindow());
        }
    }
}
