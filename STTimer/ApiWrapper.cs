using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using STTimer.ApiObjects;

namespace STTimer
{
    /// <summary>
    /// Provides a singleton interface to the api wrapper
    /// </summary>
    class ApiWrapper
    {
        /// <summary>
        /// The instance to use
        /// </summary>
        private static ApiWrapper instance;

        /// <summary>
        /// The token to do requests with
        /// </summary>
        private Token token;

        /// <summary>
        /// The restclient to do requests with
        /// </summary>
        RestClient client;

        /// <summary>
        /// Private constructor to make sure the singleton class cannot be instantiated elsewhere
        /// </summary>
        private ApiWrapper()
        {
            client = new RestClient("http://localhost:8182/");
        }

        /// <summary>
        /// Provides access to the ApiWrapper instance
        /// </summary>
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

        /// <summary>
        /// Logs in to the api, i.e. fetches a token we can use
        /// </summary>
        /// <param name="username">The username to login with</param>
        /// <param name="password">The password to login with</param>
        /// <returns>True is login was a success, false otherwise</returns>
        public bool Login(string username, string password)
        {
            //Build the request
            RestRequest req = new RestRequest("auth/login", Method.POST);
            req.AddJsonBody(new LoginData(username, password));
            req.RootElement = "";

            //Send the request and deserialize the response as a token object
            var resp = client.Execute<Token>(req);
            if (resp.StatusCode != System.Net.HttpStatusCode.OK)
            {
                return false;
            }
            Token token = resp.Data;
            this.token = token;

            //Set the tauth header
            client.AddDefaultHeader("X-Token", token.token);
            return true;
        }

        /// <summary>
        /// Checks if we are logged in
        /// </summary>
        /// <returns>True if we are, false otherwise</returns>
        public bool isLoggedIn()
        {
            return this.token != null;
        }

        /// <summary>
        /// Gets all tasks this user has access to
        /// </summary>
        /// <returns>The list of tasks</returns>
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

        /// <summary>
        /// Returns the user we are logged in as
        /// </summary>
        /// <returns>The user</returns>
        public User getUser()
        {
            if (!isLoggedIn())
            {
                throw new Exception("User needs to log in");
            }
            return token.user;
        }

        /// <summary>
        /// Saves the effort of a task
        /// </summary>
        /// <param name="task">The task to save</param>
        /// <param name="hours">The amount of effort to add</param>
        public void saveTaskEffort(STTimer.ApiObjects.Task task, double hours) {
            //Create effortData wrapping object
            EffortData effortData = new EffortData(hours);

            //Execute the request
            RestRequest req = new RestRequest("task/{id}/effort", Method.POST);
            req.AddUrlSegment("id", task.id.ToString());
            req.AddJsonBody(effortData);
            RestResponse resp = (RestResponse) client.Execute(req);

            //Switch back to the tasklist window
            Switcher.Switch(new STTimer.Windows.TaskListWindow());
        }
    }
}
